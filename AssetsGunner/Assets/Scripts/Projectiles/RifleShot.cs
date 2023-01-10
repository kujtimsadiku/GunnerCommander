
using UnityEngine;

public class RifleShot : MonoBehaviour
{
	private Rigidbody2D rb;
	[SerializeField] private float minDamage;
	[SerializeField] private float maxDamage;
	[HideInInspector] public Vector2 direction = Vector2.zero;
	private float move;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		if (direction == Vector2.zero)
			direction = new Vector2(-1.0f, 0.0f);
	}
	private void FixedUpdate()
	{
		rb.velocity = direction * 1.5f;
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
			CreateDamage.DoDamage(troop, CreateDamage.ArmorDamage(minDamage, maxDamage, troop), CreateDamage.HealthDamage(minDamage, maxDamage, troop));
			Destroy(gameObject);
			return ;
		}
		if (other.gameObject.CompareTag("Fence") || other.gameObject.layer == LayerMask.NameToLayer("Boundary"))
			Destroy(gameObject);
	}
}
