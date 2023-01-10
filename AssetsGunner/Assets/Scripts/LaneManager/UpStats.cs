using UnityEngine;

public class UpStats : MonoBehaviour
{
	[SerializeField] private GameObject[] prefabs;
	[HideInInspector] public Upgrades[] stats = new Upgrades[7];
	private Deploy deploySpeed;
	private void Awake()
	{
		deploySpeed = GetComponent<Deploy>();
		SignRifle();
		SignScout();
		SignBazooka();
		SignRiot();
		SignTank();
		SignMiniGun();
		SignSub();
	}
	private void SignRifle()
	{
		DamageMake damage = prefabs[0].GetComponentInChildren<DamageMake>();
		stats[0].damageMin = damage.minDamage;
		stats[0].damageMax = damage.maxDamage;
		Health health = prefabs[0].GetComponentInChildren<Health>();
		stats[0].health = health.health;
		stats[0].armor = health.armor;
		stats[0].speed = prefabs[0].GetComponentInChildren<TrooperMoement>().speed;
		stats[0].deploySpeed = deploySpeed.deplopySpeeds[0];
	}
	private void SignScout()
	{
		ScoutDamage damage = prefabs[1].GetComponentInChildren<ScoutDamage>();
		stats[1].damageMin = damage.minDamage;
		stats[1].damageMax = damage.maxDamage;
		Health health = prefabs[1].GetComponentInChildren<Health>();
		stats[1].health = health.health;
		stats[1].armor = health.armor;
		stats[1].speed = prefabs[1].GetComponentInChildren<TrooperMoement>().speed;
		stats[1].deploySpeed = deploySpeed.deplopySpeeds[1];
	}
	private void SignBazooka()
	{
		BazookaDamage damage = prefabs[2].GetComponentInChildren<BazookaDamage>();
		stats[2].damageMin = damage.minDamage;
		stats[2].damageMax = damage.maxDamage;
		Health health = prefabs[2].GetComponentInChildren<Health>();
		stats[2].health = health.health;
		stats[2].armor = health.armor;
		stats[2].speed = prefabs[2].GetComponentInChildren<TrooperMoement>().speed;
		stats[2].deploySpeed = deploySpeed.deplopySpeeds[2];
	}
	private void SignRiot()
	{
		DamageMake damage = prefabs[3].GetComponentInChildren<DamageMake>();
		stats[3].damageMin = damage.minDamage;
		stats[3].damageMax = damage.maxDamage;
		Health health = prefabs[3].GetComponentInChildren<Health>();
		stats[3].health = health.health;
		stats[3].armor = health.armor;
		stats[3].speed = prefabs[3].GetComponentInChildren<TrooperMoement>().speed;
		stats[3].deploySpeed = deploySpeed.deplopySpeeds[3];
	}
	private void SignTank()
	{
		DamageMake damage = prefabs[4].GetComponentInChildren<DamageMake>();
		stats[4].damageMin = damage.minDamage;
		stats[4].damageMax = damage.maxDamage;
		Health health = prefabs[4].GetComponentInChildren<Health>();
		stats[4].health = health.health;
		stats[4].armor = health.armor;
		stats[4].speed = prefabs[4].GetComponentInChildren<TrooperMoement>().speed;
		stats[4].deploySpeed = deploySpeed.deplopySpeeds[4];
	}
	private void SignMiniGun()
	{
		DamageMake damage = prefabs[5].GetComponentInChildren<DamageMake>();
		stats[5].damageMin = (damage.minDamage);
		stats[5].damageMax = (damage.maxDamage);
		Health health = prefabs[5].GetComponentInChildren<Health>();
		stats[5].health = health.health;
		stats[5].armor = health.armor;
		stats[5].speed = prefabs[5].GetComponentInChildren<TrooperMoement>().speed;
		stats[5].deploySpeed = deploySpeed.deplopySpeeds[5];
	}
	private void SignSub()
	{
		DamageMake damage = prefabs[6].GetComponentInChildren<DamageMake>();
		stats[6].damageMin = (damage.minDamage);
		stats[6].damageMax = (damage.maxDamage);
		Health health = prefabs[6].GetComponentInChildren<Health>();
		stats[6].health = health.health;
		stats[6].armor = health.armor;
		stats[6].speed = prefabs[6].GetComponentInChildren<TrooperMoement>().speed;
		stats[6].deploySpeed = deploySpeed.deplopySpeeds[6];
	}
	public void DeployTroop(GameObject unit, int sign, TrooperMoement speed)
	{
		if (sign == 0)
			Rifle(unit, speed);
		else if (sign == 1)
			Scout(unit, speed);
		else if (sign == 2)
			Bazooka(unit, speed);
		else if (sign == 3)
			Riot(unit, speed);
		else if (sign == 4)
			Tank(unit, speed);
		else if (sign == 5)
			MiniGun(unit, speed);
		else
			Sub(unit, speed);
	}
	private void Rifle(GameObject unit, TrooperMoement speed)
	{
		speed.speed = stats[0].speed;
		Health health = unit.GetComponentInChildren<Health>();
		health.health = stats[0].health;
		health.armor = stats[0].armor;
		DamageMake damage = unit.GetComponentInChildren<DamageMake>();
		damage.minDamage = stats[0].damageMin;
		damage.maxDamage = stats[0].damageMax;
	}
	private void Scout(GameObject unit, TrooperMoement speed)
	{
		speed.speed = stats[1].speed;
		Health health = unit.GetComponentInChildren<Health>();
		health.health = stats[1].health;
		health.armor = stats[1].armor;
		ScoutDamage damage = unit.GetComponentInChildren<ScoutDamage>();
		damage.minDamage = stats[1].damageMin;
		damage.maxDamage = stats[1].damageMax;
	}
	private void Bazooka(GameObject unit, TrooperMoement speed)
	{
		speed.speed = stats[2].speed;
		Health health = unit.GetComponentInChildren<Health>();
		health.health = stats[2].health;
		health.armor = stats[2].armor;
		BazookaDamage damage = unit.GetComponentInChildren<BazookaDamage>();
		damage.minDamage = stats[2].damageMin;
		damage.maxDamage = stats[2].damageMax;
	}
	private void Riot(GameObject unit, TrooperMoement speed)
	{
		speed.speed = stats[3].speed;
		Health health = unit.GetComponentInChildren<Health>();
		health.health = stats[3].health;
		health.armor = stats[3].armor;
		DamageMake damage = unit.GetComponentInChildren<DamageMake>();
		damage.minDamage = stats[3].damageMin;
		damage.maxDamage = stats[3].damageMax;
	}
	private void Tank(GameObject unit, TrooperMoement speed)
	{
		speed.speed = stats[4].speed;
		Health health = unit.GetComponentInChildren<Health>();
		health.health = stats[4].health;
		health.armor = stats[4].armor;
		DamageMake damage = unit.GetComponentInChildren<DamageMake>();
		damage.minDamage = stats[4].damageMin;
		damage.maxDamage = stats[4].damageMax;
	}
	private void MiniGun(GameObject unit, TrooperMoement speed)
	{
		speed.speed = stats[5].speed;
		Health health = unit.GetComponentInChildren<Health>();
		health.health = stats[5].health;
		health.armor = stats[5].armor;
		DamageMake damage = unit.GetComponentInChildren<DamageMake>();
		damage.minDamage = stats[5].damageMin;
		damage.maxDamage = stats[5].damageMax;
	}
	private void Sub(GameObject unit, TrooperMoement speed)
	{
		speed.speed = stats[6].speed;
		Health health = unit.GetComponentInChildren<Health>();
		health.health = stats[6].health;
		health.armor = stats[6].armor;
		DamageMake damage = unit.GetComponentInChildren<DamageMake>();
		damage.minDamage = stats[6].damageMin;
		damage.maxDamage = stats[6].damageMax;
	}
}
