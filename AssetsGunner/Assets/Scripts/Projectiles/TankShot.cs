using UnityEngine;

public class TankShot : MonoBehaviour
{
	private Rigidbody2D rb;
	[SerializeField] private float minDamage;
	[SerializeField] private float maxDamage;
	[HideInInspector] public Vector2 direction = Vector2.zero;
	private bool byTouch = true;
	private bool died = false;
	private Health health;
	private float move = 0.0f;
	private void Awake()
	{
		if (direction == Vector2.zero)
			direction = new Vector2(-1.0f, 0.0f);
		rb = GetComponent<Rigidbody2D>();
		health = GetComponent<Health>();
	}
	private void Update()
	{
		if (health.health <= 0.0f && health.armor <= 0.0f)
			Die();
	}
	private void FixedUpdate()
	{
		rb.velocity = direction * 0.8f;
		float newPos = 0.0f;
		newPos = 0.005f * Mathf.Sin(move * 20.0f);
		move += 0.01f;
		transform.position = new Vector3(transform.position.x, transform.position.y + newPos, 1.0f);
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == 3)
		{
			Health troop = other.gameObject.GetComponent<Health>();
			health.health -= (troop.health / 2.0f + troop.armor / 2.0f);
			CreateDamage.DoDamage(troop, CreateDamage.ArmorDamage(minDamage, maxDamage, troop), CreateDamage.HealthDamage(minDamage, maxDamage, troop));
			float compareArmor = troop.armor;
			float compareHealth = troop.health;
			if (troop.armor < 0.0f)
				compareArmor = 0.0f;
			if (troop.health < 0.0f)
				compareHealth = 0.0f;
			if (health.health <= 0.0f || (compareArmor + compareHealth) > 0.0f)
			{
				byTouch = false;
				Destroy(gameObject);
			}
			return ;
		}
		if (other.gameObject.CompareTag("Fence"))
		{
			byTouch = false;
			Destroy(gameObject);
		}
		if (other.gameObject.layer == LayerMask.NameToLayer("Boundary"))
		{
			byTouch = false;
			Destroy(gameObject);
		}
	}
	private void Die()
	{
		if (byTouch && transform.localScale.x == 1.0f && !died)
		{
			CreateTankShot(-90.0f, 0.0f);
			CreateTankShot(-80.0f, -0.1f);
			CreateTankShot(-100.0f, 0.1f);
			died = true;
		}
	}
	private void CreateTankShot(float angle, float yAdd)
	{
		float xAdd = 0.0f;
		if (yAdd != 0.0f)
			xAdd = 0.08f;
		GameObject shot = Instantiate(this.gameObject, transform.position + new Vector3(xAdd, yAdd, 0.0f), Quaternion.identity);
		shot.transform.localScale = new Vector3(0.6f, 0.6f, 1.0f);
		TankShot damage = shot.gameObject.GetComponent<TankShot>();
		damage.direction = NewDirection(angle);
		damage.minDamage = 60.0f;
		damage.maxDamage = 60.0f;
		Health first = shot.gameObject.GetComponent<Health>();
		first.health = 25.0f;
		first.maxHealth = 25.0f;
	}
	private Vector2 NewDirection(float angle)
	{
		Vector2 ret;
		ret = Quaternion.Euler(0.0f, 0.0f, angle) * new Vector3(0.0f, -1.0f, 0.0f);
		return (ret.normalized);
	}
}
