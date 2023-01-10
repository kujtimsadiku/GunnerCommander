using UnityEngine;

public static class CreateDamage
{
	public static void DoDamage(Health enemy, float damageArmor, float damageHealth)
	{
		if (enemy == null)
			return ;
		if (enemy.health > 0.0f)
		{
			if (enemy.armor > 0.0f)
			{
				enemy.armor -= damageArmor;
				if (enemy.armor <= 0.0f)
					enemy.health -= damageHealth + enemy.armor * -1.0f;
				else
					enemy.health -= damageHealth;
			}
			else
				enemy.health -= damageHealth;
		}
		if (enemy.health <= 0.0f && enemy.armor > 0.0f)
		{
			enemy.health = enemy.armor / 2.0f;
			enemy.armor = enemy.health;
		}
	}
	public static float HealthDamage(float minDamage, float maxDamage, Health enemy)
	{
		if (enemy == null)
			return (0.0f);
		float damageHit;
		float damageArmor;
		float damageHealth;
		damageHit = Random.Range(minDamage, maxDamage);
		damageArmor = damageHit * enemy.damageReducer * enemy.blockRate / 1.5f;
		if (enemy.armor <= 0.0f)
			damageHealth = damageArmor;
		else
			damageHealth = damageHit * enemy.damageReducer * enemy.blockRate * 0.2f;
		return (damageHealth);
	}
	public static float ArmorDamage(float minDamage, float maxDamage, Health enemy)
	{
		if (enemy == null)
			return (0.0f);
		float damageHit;
		float damageArmor;
		float damageHealth;
		damageHit = Random.Range(minDamage, maxDamage);
		damageArmor = damageHit * enemy.damageReducer * enemy.blockRate / 1.5f;
		if (enemy.armor <= 0.0f)
			damageHealth = damageArmor;
		else
			damageHealth = damageHit * enemy.damageReducer * enemy.blockRate * 0.2f;
		return (damageArmor);
	}
}
