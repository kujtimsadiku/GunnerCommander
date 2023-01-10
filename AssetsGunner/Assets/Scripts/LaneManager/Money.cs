using UnityEngine;

public static class DamageToEndLine
{
	public static bool metEnd;
	public static bool money;
}

public class Money : MonoBehaviour
{
	private Health health;
	private TrooperMoement troop;
	private Deploy ally;
	private EnemyDeployer enemy;
	private UpgadeMoney upMoney;
	[SerializeField] private GameObject moneyPrefab;
	[SerializeField] private GameObject ammoBox;
	private int[] upMoneyAmount = new int[6];
	private bool died = false;
	private bool quit = false;
	private bool gave = false;
	private void Awake()
	{
		health = GetComponent<Health>();
		troop = GetComponent<TrooperMoement>();
		ally = GameObject.FindGameObjectWithTag("Deployer").GetComponent<Deploy>();
		enemy = GameObject.FindGameObjectWithTag("EnemyDeployer").GetComponent<EnemyDeployer>();
		upMoney = ally.gameObject.GetComponent<UpgadeMoney>();
	}
	private void OnApplicationQuit()
	{
		quit = true;
	}
	private void SpawnCashStack()
	{
		if ((int)Random.Range(0, 4) == 1)
		{
			GameObject cash = Instantiate(moneyPrefab, new Vector3(transform.position.x, transform.position.y - 0.1f, 1.0f), Quaternion.identity);
			cash.gameObject.GetComponent<MoneyMoving>().moneyAmount = MoneyStack.moneyAmounts[troop.unit];
		}
		else if (DropAmmo(troop.unit) && AmmoDrop.owned != 0)
			Instantiate(ammoBox, new Vector3(transform.position.x, transform.position.y - 0.1f, 1.0f), Quaternion.identity);
	}
	private bool DropAmmo(int unit)
	{
		switch (unit)
		{
			case 0:
				if ((int)Random.Range(0, 8) == 1)
					return (true);
				break ;
			case 1:
				if ((int)Random.Range(0, 14) == 1)
					return (true);
				break ;
			case 2:
				if ((int)Random.Range(0, 7) == 1)
					return (true);
				break ;
			case 3:
				if ((int)Random.Range(0, 8) == 1)
					return (true);
				break ;
			case 4:
				if ((int)Random.Range(0, 3) == 1)
					return (true);
				break ;
			case 5:
				if ((int)Random.Range(0, 5) == 1)
					return (true);
				break ;
			case 6:
				if ((int)Random.Range(0, 3) == 1)
					return (true);
				break ;
			case 7:
				if ((int)Random.Range(0, 10) == 1)
					return (true);
				break ;
		}
		return (false);
	}
	private void AddCurrency()
	{
		if (gameObject.layer == LayerMask.NameToLayer("Allied"))
			AddMoneyToEnemy(troop.unit);
		else
		{
			AddMoneyToAlly(troop.unit);
			float cash = TroopCosts.costs[troop.unit] / 6.5f;
			upMoney.upMoney += (int)Random.Range((int)(cash / 3.0f), (int)(cash * 2.0f));
		}
	}
	private void OnDisable()
	{
		if (quit == false && health.armor <= 0.0f && health.health <= 0.0f && !died && !gave && LeveleActive.leveleActive)
		{
			AddCurrency();
			if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
				SpawnCashStack();
			died = true;
			gave = true;
		}
	}
	private void OnDestroy()
	{
		if (troop.endLine && gameObject.layer == LayerMask.NameToLayer("Allied"))
		{
			ally.money += (int)TroopCosts.costs[troop.unit];
			float amount;
			if (troop.unit == 4)
				amount = 300.0f;
			else if (troop.unit == 1)
				amount = 60.0f;
			else
				amount = TroopCosts.costs[troop.unit] * 2.0f;
			float cashAmount;
			if (enemy.money > amount)
				cashAmount = amount;
			else
				cashAmount = enemy.money;
			enemy.money -= amount;
			if (enemy.money < 0.0f)
				enemy.money = 0.0f;
			else
				DamageToEndLine.money = true;
			float cash = cashAmount / 6.5f;
			upMoney.upMoney += (int)Random.Range((int)(cash / 1.5f), (int)(cash * 2.5f));
			DamageToEndLine.metEnd = true;
		}
	}
	private int AddUpgradeMoneyToAlly(int unit)
	{
		switch (unit)
		{
			case 0:
				return ((int)(TroopCosts.rifle));
			case 1:
				return ((int)(TroopCosts.scout));
			case 2:
				return ((int)(TroopCosts.bazooka));
			case 3:
				return ((int)(TroopCosts.riot));
			case 4:
				return ((int)(TroopCosts.tank));
			case 5:
				return ((int)(TroopCosts.miniGun));
		}
		return (0);
	}
	private void AddMoneyToAlly(int unit)
	{
		switch (unit)
		{
			case 0:
				ally.money += (int)(TroopCosts.rifle / 2.0f);
				return ;
			case 1:
				ally.money += (int)(TroopCosts.scout / 2.0f);
				return ;
			case 2:
				ally.money += (int)(TroopCosts.bazooka / 2.0f);
				return ;
			case 3:
				ally.money += (int)(TroopCosts.riot / 2.0f);
				return ;
			case 4:
				ally.money += (int)(TroopCosts.tank / 2.0f);
				return ;
			case 5:
				ally.money += (int)(TroopCosts.miniGun / 3.0f);
				return ;
			case 6:
				ally.money += (int)(TroopCosts.mech / 2.0f);
				return ;
			case 7:
				ally.money += (int)(TroopCosts.fly / 2.0f);
				return ;
		}
	}
	private void AddMoneyToEnemy(int unit)
	{
		switch (unit)
		{
			case 0:
				enemy.money += TroopCosts.rifle * 1.5f;
				return ;
			case 1:
				enemy.money += TroopCosts.scout * 1.5f;
				return ;
			case 2:
				enemy.money += TroopCosts.bazooka * 1.25f;
				return ;
			case 3:
				enemy.money += TroopCosts.riot * 1.5f;
				return ;
			case 4:
				enemy.money += TroopCosts.tank * 1.25f;
				return ;
			case 5:
				enemy.money += TroopCosts.miniGun * 1.1f;
				return ;
			case 8:
				enemy.money += TroopCosts.sub;
				return ;
		}
	}
}
