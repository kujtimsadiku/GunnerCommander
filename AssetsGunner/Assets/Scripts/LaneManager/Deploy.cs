using UnityEngine;
using System.Collections;
using TMPro;

public static class PlayerMoney
{
	public static int money;
	public static int normalMoney;
	public static bool bought;
	public static bool moneyComing;
}
public class Deploy : MonoBehaviour
{
	public int money;
	private LaneUnits laneUnits;
	public LaneMoving lane;
	public TrooperSelector troop;
	public GameObject[] units;
	public GameObject[] boxes;
	public GameObject progresBar;
	private int index = 0;
	private Queue troopQueue = new Queue();
	private float progresBarTimer;
	private bool selectTime = true;
	private UpStats stats;
	[HideInInspector] public float[] deplopySpeeds = {0.035f, 0.05f, 0.02f, 0.03f, 0.013f, 0.02f, 0.05f};
	[SerializeField] private TMP_Text moneyToScreen;
	private void Awake()
	{
		PlayerMoney.moneyComing = false;
		PlayerMoney.bought = false;
		stats = GetComponent<UpStats>();
		laneUnits = GameObject.FindGameObjectWithTag("LaneManager").GetComponent<LaneUnits>();
	}
	private void Update()
	{
		if (LeveleActive.leveleActive == false)
		{
			troopQueue.Clear();
			progresBar.transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
		}
		if (PlayerMoney.moneyComing)
		{
			money = PlayerMoney.normalMoney;
			PlayerMoney.moneyComing = false;
		}
		else
			PlayerMoney.normalMoney = money;
		if (Input.GetKeyDown(KeyCode.Space) && LeveleActive.leveleActive)
			DeployTroop();
		LightBoxes();
		moneyToScreen.text = money.ToString();
		moneyToScreen.text += "$";
	}
	private void LightBoxes()
	{
		int i = 0;
		while (i < troopQueue.Count)
		{
			boxes[i].gameObject.SetActive(true);
			i++;
		}
		while (i < 5)
		{
			boxes[i].gameObject.SetActive(false);
			i++;
		}
	}
	private void FixedUpdate()
	{
		HandleTroopQueue();
		UpdateProgresBar();
	}
	private void UpdateProgresBar()
	{
		if (troopQueue.Count == 0)
			return ;
		if (progresBar.transform.localScale.x == 0.0f && selectTime)
		{
			progresBarTimer = SelectProgresTime();
			selectTime = false;
		}
		progresBar.transform.localScale = new Vector3(progresBar.transform.localScale.x + progresBarTimer, 1.0f, 1.0f);
	}
	private float SelectProgresTime()
	{
		Vector2 troop = (Vector2)troopQueue.Peek();
		switch (troop.x)
		{
			case 0:
				return (deplopySpeeds[0]);
			case 1:
				return (deplopySpeeds[1]);
			case 2:
				return (deplopySpeeds[2]);
			case 3:
				return (deplopySpeeds[3]);
			case 4:
				return (deplopySpeeds[4]);
			case 5:
				return (deplopySpeeds[5]);
			case 6:
				return (deplopySpeeds[6]);
		}
		return (0.05f);
	}
	private void HandleTroopQueue()
	{
		if (troopQueue.Count == 0)
			return ;
		else if (progresBar.transform.localScale.x >= 2.0f)
		{
			LaunchToBattlefield();
			progresBar.transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
			selectTime = true;
			return ;
		}
	}
	private void LaunchToBattlefield()
	{
		Vector2 troop = (Vector2)troopQueue.Dequeue();
		GameObject rifle = Instantiate(units[(int)troop.x], LanePosition((int)troop.x, (int)troop.y), Quaternion.identity);
		rifle.transform.transform.tag = "Allied";
		LaneUnitClass laneDeploy = new LaneUnitClass();
		if ((int)troop.x == 6)
			laneDeploy.unit = 8;
		else
			laneDeploy.unit = (int)troop.x;
		laneDeploy.checkpoint = 0;
		laneDeploy.index = index;
		laneDeploy.troop = rifle;
		TrooperMoement current = rifle.GetComponentInChildren<TrooperMoement>();
		stats.DeployTroop(rifle, (int)troop.x, current);
		current.ownLink = laneDeploy;
		current.index = index;
		current.ownLane = (int)troop.y;
		current.Init();
		laneUnits.lanes[(int)troop.y].Add(laneDeploy);
		index++;
	}
	private void DeployTroop()
	{
		if (!(troopQueue.Count < 5))
			return ;
		switch (troop.unitSelected)
		{
			case 0:
				if (MoneyCheck(TroopCosts.rifle))
				{
					money -= (int)TroopCosts.rifle;
					troopQueue.Enqueue(new Vector2(0, lane.currentLane));
				}
				return ;
			case 1:
				if (MoneyCheck(TroopCosts.scout))
				{
					money -= (int)TroopCosts.scout;
					troopQueue.Enqueue(new Vector2(1, lane.currentLane));
				}
				return ;
			case 2:
				if (MoneyCheck(TroopCosts.bazooka))
				{
					money -= (int)TroopCosts.bazooka;
					troopQueue.Enqueue(new Vector2(2, lane.currentLane));
				}
				return ;
			case 3:
				if (MoneyCheck(TroopCosts.riot))
				{
					money -= (int)TroopCosts.riot;
					troopQueue.Enqueue(new Vector2(3, lane.currentLane));
				}
				return ;
			case 4:
				if (MoneyCheck(TroopCosts.tank) && lane.currentLane != 0 && lane.currentLane != 6)
				{
					money -= (int)TroopCosts.tank;
					troopQueue.Enqueue(new Vector2(4, lane.currentLane));
				}
				return ;
			case 5:
				if (MoneyCheck(TroopCosts.miniGun - 50.0f))
				{
					money -= ((int)TroopCosts.miniGun - 50);
					troopQueue.Enqueue(new Vector2(5, lane.currentLane));
				}
				return ;
			case 6:
				if (MoneyCheck(TroopCosts.sub))
				{
					money -= (int)TroopCosts.sub;
					troopQueue.Enqueue(new Vector2(6, lane.currentLane));
				}
				return ;
		}
	}
	private Vector3 LanePosition(int troop, int currentLane)
	{
		if (troop == 0)
			return (new Vector3(lane.transform.position.x - 9.8f, 2.0f - 0.75f * currentLane, lane.transform.position.z));
		if (troop == 3 || troop == 4)
			return (new Vector3(lane.transform.position.x - 9.8f, 1.95f - 0.75f * currentLane, lane.transform.position.z));
		return (new Vector3(lane.transform.position.x - 9.8f, 2.025f - 0.75f * currentLane, lane.transform.position.z));
	}
	private bool MoneyCheck(float moneyAmount)
	{
		if ((int)moneyAmount <= money)
			return (true);
		return (false);
	}
}
