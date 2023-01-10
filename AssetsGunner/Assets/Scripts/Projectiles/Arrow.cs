using UnityEngine;

public class Arrow : MonoBehaviour
{
	[HideInInspector] public Rigidbody2D rb;
	public Vector2 target;
	public float speed = 9.0f;
	[HideInInspector] public bool released = false;
	private bool speedGiven = false;
	[HideInInspector] public int chargeCount;
	[HideInInspector] public Vector2 landingPoint;
	[SerializeField] private AnimationCurve size;
	[SerializeField] private AudioClip killSound;
	[SerializeField] private AudioClip hitmark;
	[SerializeField] private AudioClip hit;
	[SerializeField] private AudioClip metalHit;
	[SerializeField] private AudioClip metalHitUp;
	[SerializeField] private AudioClip rorCrit;
	[SerializeField] private AnimationCurve damageRange;
	[SerializeField] private float Damage;
	[SerializeField] private GameObject effect;
	[SerializeField] private GameObject arrowDrop;
	public bool dropActive;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	private void Update()
	{
		if (!released)
		{
			rb.velocity = Vector2.zero;
			return ;
		}
		if (!speedGiven)
			GiveArrowSpeed();
		Vector2 moving = rb.velocity;
		if (moving != Vector2.zero) 
		{
			float angle = Mathf.Atan2(moving.y, moving.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
		ChangeScale();
		if (rb.velocity.x > 0.0f && transform.position.x >= target.x)
			CreateDamageAndDestroy();
		else if (rb.velocity.x < 0.0f && transform.position.x <= target.x)
			CreateDamageAndDestroy();
	}
	private void CreateDamageAndDestroy()
	{
		Collider2D[] others = Physics2D.OverlapCircleAll(landingPoint, 0.1f);
		float len = Mathf.Infinity;
		GameObject enem = null;
		foreach(Collider2D other in others)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
			{
				float temp = new Vector2(other.transform.position.x - landingPoint.x, other.transform.position.y - landingPoint.y).magnitude;
				if (temp < len)
				{
					len = temp;
					enem = other.gameObject;
				}
			}
		}
		if (enem != null)
		{
			int isTank = 0;
			if (enem.CompareTag("Tank"))
				isTank = 1;
			Health health = enem.GetComponent<Health>();
			float damageMulti;
			if (chargeCount > 1100)
				damageMulti = 1.0f;
			else
				damageMulti = damageRange.Evaluate(chargeCount);
			float additionalMulti = 1.0f;
			if (isTank == 1)
			{
				if (UpBow.armor)
					additionalMulti = 1.0f;
				else
					additionalMulti = 0.25f;
			}
			float dmg = Damage * damageMulti * additionalMulti;
			CreateDamage.DoDamage(health, CreateDamage.ArmorDamage(dmg, dmg, health), CreateDamage.HealthDamage(dmg, dmg, health));
			if (health.health <= 0.0f && health.armor <= 0.0f)
			{
				if (health.gameObject.CompareTag("Rocket"))
					AudioSource.PlayClipAtPoint(killSound, new Vector3(0.29f, 0.0f, -10.0f), 0.25f);
				else if (isTank == 0)
					AudioSource.PlayClipAtPoint(hit, new Vector3(0.29f, 0.0f, -10.0f), 0.25f);
				else
					AudioSource.PlayClipAtPoint(killSound, new Vector3(0.29f, 0.0f, -10.0f), 0.25f);
			}
			else
			{
				if (isTank == 1)
				{
					if (UpBow.armor)
						AudioSource.PlayClipAtPoint(metalHitUp, new Vector3(0.29f, 0.0f, -10.0f), 0.175f);
					else
						AudioSource.PlayClipAtPoint(metalHit, new Vector3(0.29f, 0.0f, -10.0f), 0.26f);
				}
				else if (health.gameObject.CompareTag("Rocket"))
					AudioSource.PlayClipAtPoint(rorCrit, new Vector3(0.29f, 0.0f, -10.0f), 0.15f);
				AudioSource.PlayClipAtPoint(hitmark, new Vector3(0.29f, 0.0f, -10.0f), 0.25f);
				GameObject part = Instantiate(effect, health.transform.position, Quaternion.identity);
				part.GetComponent<BulletParticle>().HandleParticlesColor(isTank);
			}
		}
		if (dropActive)
			Instantiate(arrowDrop, new Vector3(transform.position.x - 0.6f, transform.position.y, 1.0f), Quaternion.identity);
		Destroy(gameObject);
	}
	private void ChangeScale()
	{
		float xDist = transform.position.x - target.x;
		transform.localScale = new Vector3(-0.6f, 1.0f, 1.0f) * size.Evaluate(Mathf.Abs(xDist));
	}
	private void GiveArrowSpeed()
	{
		speedGiven = true;
		Vector2 direction = new Vector2(transform.position.x, transform.position.y) - target;
		rb.AddForce(-direction.normalized * speed);
	}
}
