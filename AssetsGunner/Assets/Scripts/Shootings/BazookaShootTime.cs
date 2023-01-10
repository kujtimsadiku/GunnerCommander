using UnityEngine;

public class BazookaShootTime : MonoBehaviour
{
	public GameObject shotPrefab;
	private TrooperMoement bools;
	private Animator anim;
	[SerializeField] private Vector2 AttackArea;
	private bool shootDown = false;
	private bool shootUp = false;
	private bool attackingUp = false;
	private GameObject tankToShoot = null;
	private void Awake()
	{
		bools = GetComponent<TrooperMoement>();
		anim = GetComponent<Animator>();
	}
	private void Update()
	{
		if (!shootDown && !shootUp)
		{
			anim.SetBool("Shoot", bools.shoot);
			anim.SetBool("Idle", bools.idle);
		}
		anim.SetBool("ShootUp", shootUp);
		anim.SetBool("ShootDown", shootDown);
		if (shootUp == true && attackingUp == false)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + 0.08f, transform.position.z);
			attackingUp = true;
		}
		if (shootUp == false && attackingUp == true)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y - 0.08f, transform.position.z);
			attackingUp = false;
		}
	}
	private void FixedUpdate()
	{
		tankToShoot = null;
		ShootTheTank();
	}
	private void ShootTheTank()
	{
		// GameObject tankToShoot = null;
		float length = 5000.0f;
		int layerIndex = LayerMask.NameToLayer("Enemy");
		if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
			layerIndex = LayerMask.NameToLayer("Allied");
		Collider2D[] collisions = Physics2D.OverlapBoxAll(transform.position, AttackArea, 0.0f);
		foreach(Collider2D other in collisions)
		{
			if (other.transform.gameObject.layer == layerIndex && other.transform.gameObject.CompareTag("Tank"))
			{
				if (TankDistance(other.transform.position).magnitude < length)
				{
					length = TankDistance(other.transform.position).magnitude;
					tankToShoot = other.transform.gameObject;
				}
			}
		}
		if (tankToShoot != null)
		{
			bools.shooting = true;
			anim.SetBool("Idle", false);
			bools.stop = 0.0f;
			if (tankToShoot.transform.position.y > transform.position.y)
			{
				shootUp = true;
				shootDown = false;
			}
			else if (tankToShoot.transform.position.y < transform.position.y)
			{
				shootDown = true;
				shootUp = false;
			}
			return ;
		}
		bools.shooting = false;
		shootDown = false;
		shootUp = false;
	}
	private void CreateShotEffetForward()
	{
		float xAddition = 0.45f;
		if (transform.localScale.x > 0.0f)
			xAddition = -0.45f;
		GameObject shot = Instantiate(shotPrefab, new Vector3(transform.position.x + xAddition, transform.position.y + 0.2f, transform.position.y), Quaternion.identity);
		shot.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
		if (transform.localScale.x < 0.0f)
			shot.transform.localScale = new Vector3(-0.5f, 0.5f, 1.0f);
	}
	private void CreateShotEffetUp()
	{
		GameObject shot = Instantiate(shotPrefab, new Vector3(transform.position.x, transform.position.y + 0.69f, transform.position.y), Quaternion.identity);
		shot.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
		shot.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
	}
	private void CreateShotEffetDown()
	{
		GameObject shot = Instantiate(shotPrefab, new Vector3(transform.position.x, transform.position.y - 0.55f, transform.position.y), Quaternion.identity);
		shot.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
		shot.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
	}
	private Vector2 TankDistance(Vector2 position)
	{
		return (new Vector2(position.x - transform.position.x, position.y - transform.position.y));
	}
	private void ShootUpDown()
	{
		if (tankToShoot == null)
			return ;
		float multi = 1.0f;
		float extraMulti = 1.0f;
		float minDamage = 75.0f;
		float maxDamage = 100.0f;
		if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			multi = 1.1f;
			extraMulti = 2.2f;
		}
		Health thisEnemy = tankToShoot.GetComponent<Health>();
		CreateDamage.DoDamage(thisEnemy, CreateDamage.ArmorDamage(minDamage * extraMulti, maxDamage * extraMulti, thisEnemy) * multi,
								CreateDamage.HealthDamage(minDamage * extraMulti, maxDamage * extraMulti, thisEnemy) * multi);
	}
}
