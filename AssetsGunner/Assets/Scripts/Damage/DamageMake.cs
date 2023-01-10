using UnityEngine;

public class DamageMake : MonoBehaviour
{
	private TrooperMoement troops;
	public float minDamage;
	public float maxDamage;
	private void Awake()
	{
		troops = GetComponent<TrooperMoement>();
	}
	private void InflictDamage()
	{
		if (troops.targetEnemy == null)
			return ;
		float multi = 1.0f;
		if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
			multi = 1.1f;
		Health thisEnemy = troops.targetEnemy.GetComponent<Health>();
		CreateDamage.DoDamage(thisEnemy, CreateDamage.ArmorDamage(minDamage, maxDamage, thisEnemy) * multi, CreateDamage.HealthDamage(minDamage, maxDamage, thisEnemy) * multi);
	}
}