using UnityEngine;
using TMPro;

public class TrooperSelector : MonoBehaviour
{
	[HideInInspector] public int unitSelected = 0;
	private int scroller = 0;
	[HideInInspector] public int unitsAmount;
	[SerializeField] private Deploy money;
	[SerializeField] private TMP_Text troopCostToScreen;
	[SerializeField] private GameObject miniGun;
	[SerializeField] private GameObject sub;
	private bool subFirst = false;
	private int added = 0;
	//3.0f, 3.9f;
	private void Awake()
	{
		unitsAmount = 4;
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			if (scroller > 0)
			{
				transform.position = new Vector3(transform.position.x - 0.9f, transform.position.y, transform.position.z);
				scroller--;
			}
			else
			{
				transform.position = new Vector3(transform.position.x + (0.9f * unitsAmount), transform.position.y, transform.position.z);
				scroller = unitsAmount;
			}
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			if (scroller < unitsAmount)
			{
				transform.position = new Vector3(transform.position.x + 0.9f, transform.position.y, transform.position.z);
				scroller++;
			}
			else
			{
				transform.position = new Vector3(transform.position.x - (0.9f * unitsAmount), transform.position.y, transform.position.z);
				scroller = 0;
			}
		}
		if (subFirst && scroller == 5)
			unitSelected = 6;
		else if (subFirst && scroller == 6)
			unitSelected = 5;
		else
			unitSelected = scroller;
		PutCostTextToScreen();
	}
	public void MiniGunAdded()
	{
		miniGun.SetActive(true);
		unitsAmount++;
		if (added == 1)
			miniGun.transform.position = new Vector3(miniGun.transform.position.x + 0.9f, miniGun.transform.position.y, 1.0f);
		added++;
	}
	public void SubAdded()
	{
		sub.SetActive(true);
		unitsAmount++;
		if (added == 0)
		{
			sub.transform.position = new Vector3(sub.transform.position.x - 0.9f, sub.transform.position.y, 1.0f);
			subFirst = true;
		}
		added++;
	}
	private void PutCostTextToScreen()
	{
		int unitCost = UnitCost(unitSelected);
		troopCostToScreen.text = unitCost.ToString();
		troopCostToScreen.text += "$";
		if (unitCost > money.money)
			troopCostToScreen.color = new Color(0.482f, 0.01f, 0.0f); //Red
		else
			troopCostToScreen.color = new Color(0.0f, 0.415f, 0.0003f); //Green
	}
	private int UnitCost(int unit)
	{
		switch (unit)
		{
			case 0:
				return ((int)TroopCosts.rifle);
			case 1:
				return ((int)TroopCosts.scout);
			case 2:
				return ((int)TroopCosts.bazooka);
			case 3:
				return ((int)TroopCosts.riot);
			case 4:
				return ((int)TroopCosts.tank);
			case 5:
				return ((int)TroopCosts.miniGun - 50);
			case 6:
				return ((int)TroopCosts.sub);
		}
		return (0);
	}
}
