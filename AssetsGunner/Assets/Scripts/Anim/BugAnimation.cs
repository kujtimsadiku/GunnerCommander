using UnityEngine;

public class BugAnimation : MonoBehaviour
{
	private TrooperMoement bools;
	private Animator anim;
	[SerializeField] private GameObject splash;
	private DamageMake dmg;
	[SerializeField] private float minDamage;
	[SerializeField] private float maxDamage;
	[SerializeField] private Health dead;
	private float tempMinDmg;
	private float tempMaxDmg;
	private void Awake()
	{
		bools = GetComponent<TrooperMoement>();
		anim = GetComponent<Animator>();
		dmg = GetComponent<DamageMake>();
		tempMaxDmg = maxDamage;
		tempMinDmg = minDamage;
	}
	private void Update()
	{
		if (bools.shoot)
			anim.SetBool("Shoot", true);
	}
	private void Splash()
	{
		if (gameObject == null)
			return ;
		Instantiate(splash, new Vector3(transform.position.x - 0.13f, transform.position.y - 0.16f, 1.0f), Quaternion.identity);
	}
	private void InflictDamage()
	{
		if (bools.targetEnemy == null)
			return ;
		float multi = 1.0f;
		if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
			multi = 1.1f;
		minDamage = tempMinDmg;
		maxDamage = tempMaxDmg;
		if (!(bools.targetEnemy.CompareTag("Fence")))
		{
			if (bools.targetEnemy.GetComponent<TrooperMoement>().unit == 4)
			{
				minDamage /= 10;
				maxDamage /= 10;
			}
			else if (bools.targetEnemy.GetComponent<TrooperMoement>().unit == 3)
			{
				minDamage /= 6;
				maxDamage /= 6;
			}
		}
		else
		{
			minDamage /= 9;
			maxDamage /= 9;
		}
		Health thisEnemy = bools.targetEnemy.GetComponent<Health>();
		CreateDamage.DoDamage(thisEnemy, CreateDamage.ArmorDamage(minDamage, maxDamage, thisEnemy) * multi, CreateDamage.HealthDamage(minDamage, maxDamage, thisEnemy) * multi);
	}
}
