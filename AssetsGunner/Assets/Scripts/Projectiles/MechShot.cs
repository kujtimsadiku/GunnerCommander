using UnityEngine;

public class MechShot : MonoBehaviour
{
private Rigidbody2D rb;
	[SerializeField] private float minDamage;
	[SerializeField] private float maxDamage;
	[HideInInspector] public Vector2 direction = Vector2.zero;
	[HideInInspector] public int sinDir;
	private float move = 0.8f;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		if (direction == Vector2.zero)
			direction = new Vector2(-1.0f, 0.0f);
		sinDir = 0;
	}
	private void FixedUpdate()
	{
		rb.velocity = direction * 1.2f;
		float newPos = 0.0f;
		if (sinDir == 1)
			newPos = 0.1f * Mathf.Sin(move * 10.0f);
		else
			newPos = -0.1f * Mathf.Sin(move * 10.0f);
		move += 0.01f;
		transform.position = new Vector3(transform.position.x, transform.position.y + newPos, 1.0f);
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == 3)
		{
			Health troop = other.gameObject.GetComponent<Health>();
			CreateDamage.DoDamage(troop, CreateDamage.ArmorDamage(minDamage, maxDamage, troop), CreateDamage.HealthDamage(minDamage, maxDamage, troop));
			Destroy(gameObject);
			return ;
		}
		if (other.gameObject.CompareTag("Fence") || other.gameObject.layer == LayerMask.NameToLayer("Boundary"))
			Destroy(gameObject);
	}
}
