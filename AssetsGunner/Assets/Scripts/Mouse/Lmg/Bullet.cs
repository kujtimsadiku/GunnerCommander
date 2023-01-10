using UnityEngine;

public class Bullet : MonoBehaviour
{
	[HideInInspector] public Vector2 target;
	[HideInInspector] public System.Action destroyed;
	private Vector2 direction;
	private Rigidbody2D rb;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	private void Start()
	{
		direction = target - new Vector2(transform.position.x, transform.position.y);
		rb.velocity = direction.normalized * 20.0f;
		if (direction != Vector2.zero) 
		{
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}
	private void FixedUpdate()
	{
		MetTarget();
		rb.velocity /= 1.05f;
		transform.localScale = transform.localScale / 1.12f;
	}
	private void MetTarget()
	{
		Vector2 dist = target - new Vector2(transform.position.x, transform.position.y);
		if (dist.magnitude < 0.2f)
		{
			destroyed.Invoke();
			Destroy(gameObject);
		}
	}
}
