using UnityEngine;

public class BazookaDamage : MonoBehaviour
{
	private TrooperMoement troops;
	public float minDamage;
	public float maxDamage;
	private void Awake()
	{
		troops = GetComponent<TrooperMoement>();
	}
	private void BazookaDmg()
	{
		float tankMulit = 1.0f;
		if (troops.targetEnemy == null)
			return ;
		float multi = 1.0f;
		if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
			multi = 1.1f;
		if (!(troops.targetEnemy.CompareTag("Fence")))
		{
			if (troops.targetEnemy.CompareTag("Tank") || troops.targetEnemy.gameObject.GetComponent<TrooperMoement>().unit == 6)
				tankMulit = Random.Range(1.3f, 1.9f);
		}
		Health thisEnemy = troops.targetEnemy.GetComponent<Health>();
		CreateDamage.DoDamage(thisEnemy, CreateDamage.ArmorDamage(minDamage, maxDamage, thisEnemy) * multi * tankMulit, CreateDamage.HealthDamage(minDamage, maxDamage, thisEnemy) * multi * tankMulit);
	}
}
