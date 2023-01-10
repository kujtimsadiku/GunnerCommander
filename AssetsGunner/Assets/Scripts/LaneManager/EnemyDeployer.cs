using UnityEngine;
using System.Collections.Generic;
public static class TroopCosts
{
	public const float rifle = 50.0f;
	public const float scout = 20.0f;
	public const float bazooka = 150.0f;
	public const float riot = 100.0f;
	public const float tank = 500.0f;
	public const float miniGun = 300.0f;
	public const float mech = 1000.0f;
	public const float fly = 80.0f;
	public const float sub = 75.0f;
	public static readonly float[] costs = {50.0f, 20.0f, 150.0f, 100.0f, 500.0f, 300.0f, 1000.0f, 80.0f, 75.0f};
}
public class UnitDeploy
{
	public int lane;
	public int unit;
}
public class Stat
{
	public float strength;
	public float defence;
}
public class EnemyDeployer : MonoBehaviour
{
	private int index = 0;
	public float money;
	private LaneUnits lanes;
	private LaneUnits enemyLanes;
	[SerializeField] private LevelChange level;
	private Deploy playerMoney;
	private float deployTimer = 1.0f;
	private List<UnitDeploy> deployQueue = new List<UnitDeploy>();
	private bool canDeploy;
	private int[] lanePriotiry = new int[7];
	public GameObject[] units;
	private Stat[] unitStats = new Stat[9];
	[SerializeField] private AnimationCurve moneyDeployEffect;
	[SerializeField] private AnimationCurve moneyTankEffect;
	private void Awake()
	{
		lanes = GameObject.FindGameObjectWithTag("LaneManager").GetComponent<LaneUnits>();
		enemyLanes = GameObject.FindGameObjectWithTag("EnemyLaneManager").GetComponent<LaneUnits>();
		playerMoney = GameObject.FindGameObjectWithTag("Deployer").GetComponent<Deploy>();
		for (int i = 0 ; i < 9 ; i++)
			unitStats[i] = new Stat();
		unitStats[0].strength = UnitStats.rifleStrength;
		unitStats[0].defence = UnitStats.rifleDefence;
		unitStats[1].strength = UnitStats.scoutStrength;
		unitStats[1].defence = UnitStats.scoutDefence;
		unitStats[2].strength = UnitStats.bazookaStrength;
		unitStats[2].defence = UnitStats.bazookaDefence;
		unitStats[3].strength = UnitStats.riotStrength;
		unitStats[3].defence = UnitStats.rifleDefence;
		unitStats[4].strength = UnitStats.tankStrength;
		unitStats[4].defence = UnitStats.tankDefence;
		unitStats[5].strength = UnitStats.miniGunStrength;
		unitStats[5].defence = UnitStats.miniGunDefence;
		unitStats[6].strength = UnitStats.mechStrength;
		unitStats[6].defence = UnitStats.mechDefence;
		unitStats[7].strength = UnitStats.flyStrength;
		unitStats[7].defence = UnitStats.flyDefence;
	}
	private void FixedUpdate()
	{
		ManageDeployTimer();
		if (canDeploy)
		{
			MangageLanePriority();
			ChooseLaneToEngage();
			DeployOnTheLane();
		}
	}
	private void DeployOnTheLane()
	{
		if (deployQueue.Count == 0)
		{
			deployTimer = 1.0f;
			return ;
		}
		GameObject troop = Instantiate(units[deployQueue[0].unit], LanePosition(deployQueue[0].lane, deployQueue[0].unit), Quaternion.identity);
		TrooperMoement move = troop.gameObject.GetComponentInChildren<TrooperMoement>();
		LaneUnitClass unit = new LaneUnitClass();
		unit.unit = deployQueue[0].unit;
		unit.index = index;
		unit.checkpoint = 0;
		unit.troop = troop;
		enemyLanes.lanes[deployQueue[0].lane].Add(unit);
		move.ownLink = unit;
		move.index = index;
		move.ownLane = deployQueue[0].lane;
		move.transform.gameObject.layer = 6;
		move.transform.localScale = Vector3.one;
		move.Init();
		if (deployQueue[0].unit == 4)
			move.transform.localScale = new Vector3(1.0f, 0.6f, 1.0f);
		move.direction = (-1.0f);
		AddDeployTime(deployQueue[0].unit);
		TakeMoney(deployQueue[0].unit);
		deployQueue.RemoveAt(0);
		index++;
	}
	private void AddDeployTime(int unit)
	{
		switch (unit)
		{
			case 0:
				deployTimer = 1.0f;
				return ;
			case 1:
				deployTimer = 0.5f;
				return ;
			case 2:
				deployTimer = 1.5f;
				return ;
			case 3:
				deployTimer = 1.5f;
				return ;
			case 4:
				deployTimer = 2.5f;
				return ;
			case 5:
				deployTimer = 2.0f;
				return ;
			case 6:
				deployTimer = 2.5f;
				return ;
			case 7:
				deployTimer = 0.5f;
				return ;
		}
	}
	private void TakeMoney(int unit)
	{
		switch (unit)
		{
			case 0:
				money -= TroopCosts.rifle;
				return ;
			case 1:
				money -= TroopCosts.scout;
				return ;
			case 2:
				money -= TroopCosts.bazooka;
				return ;
			case 3:
				money -= TroopCosts.riot;
				return ;
			case 4:
				money -= TroopCosts.tank;
				return ;
			case 5:
				money -= TroopCosts.miniGun;
				return ;
			case 6:
				money -= TroopCosts.mech;
				return ;
			case 7:
				money -= TroopCosts.fly;
				return ;
		}
	}
	private Vector3 LanePosition(int currentLane, int unit)
	{
		if (unit == 7)
			return (new Vector3(transform.position.x + 11.0f, 2.2f - 0.75f * currentLane, transform.position.z));
		if (unit == 6)
			return (new Vector3(transform.position.x + 11.0f, 2.15f - 0.75f * currentLane, transform.position.z));
		if (unit == 4)
			return (new Vector3(transform.position.x + 11.0f, 1.95f - 0.75f * currentLane, transform.position.z));
		return (new Vector3(transform.position.x + 11.0f, 2.025f - 0.75f * currentLane, transform.position.z));
	}
	private void ChooseLaneToEngage()
	{
		if (deployQueue.Count != 0 || money < TroopCosts.scout)
			return ;
		EngageOnTheLane(FindMaxValue());
	}
	private bool EngageOnTheLane(int lane)
	{
		float[] chances = new float[units.Length + 1];
		for (int i = 0 ; i < units.Length + 1 ; i++)
			chances[i] = GetChanceForUnit(lane, i);
		return (AddUnitToQueue(lane, chances));
	}
	private float GetChanceForUnit(int lane, int unit)
	{
		float deploy = 0.0f;
		if (money < 20.0f)
		{
			if (unit == units.Length + 1)
				deploy += 100.0f;
			return (deploy);
		}
		if (lanes.strength[lane] > 0.0f)
		{
			if (unit == 3 && enemyLanes.defence[lane] <= 4.0f)
				deploy += AddDeploy(unit, 8.0f);
			if (lanes.strength[lane] > enemyLanes.strength[lane])
			{
				if (enemyLanes.defence[lane] <= 4.0f && unitStats[unit].defence > 5.0f)
				{
					if (unit == 4)
					{
						if (money > 1000.0f && money < 2000.0f)
							deploy += AddDeploy(unit, 1.5f + moneyTankEffect.Evaluate(money));
						else if (money > 2000.0f)
							deploy += AddDeploy(unit, 2.6f);
					}
					else
						deploy += AddDeploy(unit, 3.0f);
				}
				if (unitStats[unit].strength > 1.0f)
				{
					if (unit == 4 && money > 1000 && money < 2000.0f)
						deploy += AddDeploy(unit, moneyTankEffect.Evaluate(money));
					else if (unit == 4)
						deploy += AddDeploy(unit, 0.6f);
					else if (unitStats[unit].strength > 9.0f)
						deploy += AddDeploy(unit, 5.0f);
					else
						deploy += AddDeploy(unit, 2.0f);
				}
			}
		}
		if (lanes.strength[lane] == 0.0f)
		{
			if (unitStats[unit].strength >= 1.0f)
				deploy += AddDeploy(unit, 4.0f);
			if (unit == 1 && lanes.defence[lane] == 0.0f)
				deploy += AddDeploy(unit, 8.0f);
			if (lanes.defence[lane] > 0.0f && unitStats[unit].strength > 2.0f)
				deploy += AddDeploy(unit, 5.0f);
			if (unitStats[unit].defence > 7.0f)
				deploy += AddDeploy(unit, 3.0f);
			else if (unitStats[unit].defence > 4.0f)
				deploy += AddDeploy(unit, 1.5f);
			if (enemyLanes.defence[lane] <= 3.0f && unit == 3)
				deploy += AddDeploy(unit, 10.0f);
			if (unit == 4 && money > 1000.0f && money < 2000.0f)
				deploy += AddDeploy(unit, moneyTankEffect.Evaluate(money));
			else if (unit == 4 && money > 2000.0f)
				deploy += AddDeploy(unit, 1.5f);
		}
		if (lanePriotiry[lane] <= 2)
		{
			if (money < playerMoney.money && unit == units.Length)
				deploy += 45.0f;
			if (unit == units.Length)
				deploy += 25.5f;
			else if (deploy != 0.0f)
				deploy = deploy / 10.0f;
		}
		if (money < 550.0f && unit == units.Length)
		{
			if (lanePriotiry[lane] <= 2)
				deploy += 10.0f;
			deploy += moneyDeployEffect.Evaluate(money);
		}
		if (money > 5000.0f)
		{
			deploy += AddDeploy(unit, 0.25f);
			if (money > playerMoney.money)
				deploy += AddDeploy(unit, 0.5f);
			if (money > 6000.0f)
				deploy += AddDeploy(unit, 0.75f);
			if (money > 8000.0f)
				deploy += AddDeploy(unit, 0.5f);
		}
		else if (unit == 6 && deploy != 0.0f)
			deploy /= 1.65f;
		if (unit == 6)
		{
			for (int i = 0 ; i < 7 ; i++)
			{
				float amount;
				if ((amount = FindUnitInLane(i, unit)) > 0 && deploy != 0.0f)
					deploy /= (amount + 1.0f);
			}
		}
		if (unit == 7)
		{
			if (enemyLanes.strength[lane] < 2.3f && enemyLanes.defence[lane] < 2.3f)
			{
				deploy += AddDeploy(unit, 0.5f);
				if (FindUnitInAlliedLane(lane, 4) == 0 && lanes.strength[lane] != 0.0f && FindUnitInLane(lane, unit) < 2)
					deploy += AddDeploy(unit, 2.0f);
			}
			else
				deploy += AddDeploy(unit, 0.25f);
			if (FindUnitInAlliedLane(lane, 4) == 0 && lanes.strength[lane] != 0.0f && FindUnitInLane(lane, unit) < 2)
				deploy += AddDeploy(unit, 1.0f);
		}
		if (unit == 0 && FindUnitInLane(lane, unit) < 5)
			deploy += AddDeploy(unit, 0.75f);
		if (unit == 1 && enemyLanes.defence[lane] > 1.0f)
			deploy = 0.0f;
		if (enemyLanes.defence[lane] > 10.0f && (unit != 4 && unitStats[unit].strength > 9.0f) && FindUnitInLane(lane, unit) < 1)
			deploy += AddDeploy(unit, 2.5f);
		if (enemyLanes.defence[lane] > 5.0f && unit == 0 && FindUnitInLane(lane, unit) < 4)
			deploy += AddDeploy(unit, 0.5f);
		if (FindUnitInLane(lane, unit) >= 3 && deploy != 0.0f)
			deploy = deploy / 3.0f;
		if (FindUnitInLane(lane, unit) >= 2 && unit == 5 && deploy != 0.0f)
			deploy /= 1.6f;
		if (unit == 3 && FindUnitInLane(lane, unit) != 0)
			deploy = 0.0f;
		if (GiveCost(unit) * 2.8f > money && unit != 1 && unit != 0 && unit != units.Length && deploy != 0.0f)
			deploy = deploy / 9.0f;
		if (money < 1350.0f && unit == 5)
			deploy = 0.0f;
		if (unit == 6 && money < 2750.0f)
			deploy = 0.0f;
		if ((unit == 4 || unit == 6) && (lane == 0 || lane == 6))
			deploy = 0.0f;
		return (deploy);
	}
	private int FindUnitInLane(int lane, int unit)
	{
		int amount = 0;
		for (int i = 0 ; i < enemyLanes.lanes[lane].Count ; i++)
		{
			if (enemyLanes.lanes[lane][i].unit == unit)
				amount++;
		}
		return (amount);
	}
	private int FindUnitInAlliedLane(int lane, int unit)
	{
		int amount = 0;
		for (int i = 0 ; i < lanes.lanes[lane].Count ; i++)
		{
			if (lanes.lanes[lane][i].unit == unit)
				amount++;
		}
		return (amount);
	}
	private float AddDeploy(int unit, float amount)
	{
		switch (unit)
		{
			case 0:
				if (money < TroopCosts.rifle)
					return (0.0f);
				break ;
			case 1:
				if (money < TroopCosts.scout)
					return (0.0f);
				break ;
			case 2:
				if (money < TroopCosts.bazooka)
					return (0.0f);
				break ;
			case 3:
				if (money < TroopCosts.riot)
					return (0.0f);
				break ;
			case 4:
				if (money * 0.45f < TroopCosts.tank)
					return (0.0f);
				break ;
			case 5:
				if (money * 0.8f < TroopCosts.miniGun)
					return (0.0f);
				break ;
			case 6:
				if (money * 0.5f < TroopCosts.mech || level.level < 11)
					return (0.0f);
				break ;
			case 7:
				if (money * 0.5f < TroopCosts.fly || level.level < 7)
					return (0.0f);
				break ;
		}
		return (amount);
	}
	private bool AddUnitToQueue(int lane, float[] chances)
	{
		UnitDeploy unit = new UnitDeploy();
		unit.lane = lane;
		float rand = Random.Range(0.0f, GetChanceValue(chances, units.Length + 1));
		if (rand > 0.0f && rand < chances[0])
		{
			unit.unit = 0;
			deployQueue.Add(unit);
			return (true);
		}
		if (rand >= chances[0] && rand < GetChanceValue(chances, 2))
		{
			unit.unit = 1;
			deployQueue.Add(unit);
			return (true);
		}
		if (rand >= GetChanceValue(chances, 2) && rand < GetChanceValue(chances, 3))
		{
			unit.unit = 2;
			deployQueue.Add(unit);
			return (true);
		}
		if (rand >= GetChanceValue(chances, 3) && rand < GetChanceValue(chances, 4))
		{
			unit.unit = 3;
			deployQueue.Add(unit);
			return (true);
		}
		if (rand >= GetChanceValue(chances, 4) && rand < GetChanceValue(chances, 5))
		{
			unit.unit = 4;
			deployQueue.Add(unit);
			return (true);
		}
		if (rand >= GetChanceValue(chances, 5) && rand < GetChanceValue(chances, 6))
		{
			unit.unit = 5;
			deployQueue.Add(unit);
			return (true);
		}
		if (rand >= GetChanceValue(chances, 6) && rand < GetChanceValue(chances, 7))
		{
			unit.unit = 6;
			deployQueue.Add(unit);
			return (true);
		}
		if (rand >= GetChanceValue(chances, 7) && rand < GetChanceValue(chances, 8))
		{
			unit.unit = 7;
			deployQueue.Add(unit);
			return (true);
		}
		return (false);
	}
	private float GetChanceValue(float[] chances, int max)
	{
		float add = 0.0f;
		int i = 0;
		while (i < max)
		{
			add += chances[i];
			i++;
		}
		return (add);
	}
	private int FindMaxValue()
	{
		int i = 0;
		int max = 0;
		int prioSave = -100000;
		while (i < 7)
		{
			if (prioSave <= lanePriotiry[i])
			{
				if (max == (-1) || prioSave < lanePriotiry[i])
				{
					max = i;
					prioSave = lanePriotiry[i];
				}
				else if ((int)Random.Range(0, 5) == 1)
				{
					max = i;
					prioSave = lanePriotiry[i];
				}
			}
			i++;
		}
		return (max);
	}
	private void MangageLanePriority()
	{
		int i = 0;
		while (i < 7)
		{
			lanePriotiry[i] = CheckCurrentLanePritority(i);
			i++;
		}
	}
	private void ManageSamePriorities()
	{
		int prioSave = (-1000);
		int count = 0;
		int[] sames = new int[7];
		for (int i = 0 ; i < 7 ; i++)
		{
			sames[i] = (-1);
			if (prioSave < lanePriotiry[i])
				prioSave = lanePriotiry[i];
		}
		for (int i = 0 ; i < 7 ; i++)
		{
			if (lanePriotiry[i] == prioSave)
			{
				count++;
				sames[i] = 1;
			}
		}
		if (count == 1 || prioSave < 9)
			return ;
		float maxDiff = 0.0f;
		int saveLane = (-1);
		for (int i = 0 ; i < 7 ; i++)
		{
			if (sames[i] == 1)
			{
				if ((lanes.strength[i] + lanes.defence[i] - enemyLanes.strength[i] + enemyLanes.defence[i]) > maxDiff)
				{
					maxDiff = (lanes.strength[i] + lanes.defence[i] - enemyLanes.strength[i] + enemyLanes.defence[i]);
					saveLane = i;
				}
			}
		}
		lanePriotiry[saveLane] += 3;
	}
	private int CheckCurrentLanePritority(int lane)
	{
		int priority = 0;
		if (lanes.defence[lane] > enemyLanes.defence[lane])
			priority += Random.Range(1, 3);
		if (lanes.strength[lane] > enemyLanes.strength[lane])
		{
			priority += Random.Range(3, 9);
			if (money > playerMoney.money)
				priority += Random.Range(0, 3);;
		}
		if (lanes.strength[lane] == 0.0f && lanes.defence[lane] == 0.0f)
		{
			priority += Random.Range(2, 6);
			if (money > playerMoney.money)
				priority += Random.Range(3, 6);
		}
		if (lanes.defence[lane] < enemyLanes.strength[lane] && lanes.defence[lane] != 0.0f)
		{
			if (playerMoney.money > money)
				priority -= 3;
			else
				priority += Random.Range(-1, 4);
			if (lanes.defence[lane] > enemyLanes.strength[lane] * 0.9f)
				priority += 1;
			else
				priority -= Random.Range(-1, 3);
		}
		if (lanes.strength[lane] < enemyLanes.strength[lane] && lanes.strength[lane] != 0.0f)
		{
			if (playerMoney.money > money)
				priority -= 3;
			else
				priority += Random.Range(-1, 5);
			if (lanes.strength[lane] > enemyLanes.strength[lane] * 0.9f)
				priority += 1;
			else
				priority -= Random.Range(-1, 3);
		}
		priority += lanes.urgency[lane];
		if (lanes.strength[lane] > 0.0f && enemyLanes.strength[lane] == 0.0f && lanes.urgency[lane] == 3)
		{
			priority += 2;
			if (money > playerMoney.money)
				priority += Random.Range(2, 13);
		}
		return (priority);
	}
	private float GiveCost(int unit)
	{
		switch (unit)
		{
			case 0:
				return (TroopCosts.rifle);
			case 1:
				return (TroopCosts.scout);
			case 2:
				return (TroopCosts.bazooka);
			case 3:
				return (TroopCosts.riot);
			case 4:
				return (TroopCosts.tank);
			case 5:
				return (TroopCosts.miniGun);
			case 7:
				return (TroopCosts.fly);
		}
		return (0.0f);
	}
	private void ManageDeployTimer()
	{
		if (deployTimer > 0.0f)
			deployTimer -= 0.02f;
		if (deployTimer <= 0.0f)
		{
			canDeploy = true;
			return ;
		}
		canDeploy = false;
	}
}

