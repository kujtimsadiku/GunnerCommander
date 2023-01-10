using UnityEngine;

public class Rocket : MonoBehaviour
{
	private Rigidbody2D rb;
	[SerializeField] private GameObject rocketExp;
	[SerializeField] private float minDamage;
	[SerializeField] private float maxDamage;
	private float yMove = 0.005f;
	private bool quit = false;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	private void FixedUpdate()
	{
		rb.velocity += new Vector2(-0.1f, 0.0f);
		transform.position = new Vector3(transform.position.x, transform.position.y - yMove, 1.0f);
		yMove /= 1.02f;
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
	private void OnApplicationQuit()
	{
		quit = true;
	}
	private void OnDestroy()
	{
		if (!quit)
			Instantiate(rocketExp, transform.position, Quaternion.identity);
	}
}
