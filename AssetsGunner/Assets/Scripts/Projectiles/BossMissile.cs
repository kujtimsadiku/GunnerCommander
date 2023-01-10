using UnityEngine;
using System.Collections.Generic;

public class BossMissile : MonoBehaviour
{
	private Health health;
	[SerializeField] private float minDamage;
	[SerializeField] private float maxDamage;
	[SerializeField] private GameObject missileExp;
	private Rigidbody2D rb;
	private Vector2 fencePosition = new Vector2(-5.604f, -0.222f);
	private List<Vector2> midPoints = new List<Vector2>();
	private Vector2 moveDirection = new Vector2(1.0f, 0.0f);
	private Vector2 finalPoint;
	private bool quit = false;
	[SerializeField] private float speed;
	// y arvot 2.5 - -3.0
	private void Awake()
	{
		health = GetComponent<Health>();
		rb = GetComponent<Rigidbody2D>();
	}
	private void Start()
	{
		GetMidPoints();
		Invoke(nameof(DestroyThis), 10.0f);
	}
	private void FixedUpdate()
	{
		GetMoveDirection();
		if (moveDirection != Vector2.zero) 
		{
			float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
		rb.velocity = -moveDirection.normalized * speed;
	}
	private void GetMoveDirection()
	{
		Vector2 target = GetTargetPoint();
		Vector2 targetDirection = new Vector2(transform.position.x, transform.position.y) - target;
		moveDirection = moveDirection.normalized + (targetDirection.normalized * 0.25f);
	}
	private Vector2 GetTargetPoint()
	{
		if (midPoints.Count == 0)
			return (finalPoint);
		if (MetThePoint(midPoints[0]))
			midPoints.RemoveAt(0);
		if (midPoints.Count == 0)
			return (finalPoint);
		return (midPoints[0]);
	}
	private bool MetThePoint(Vector2 point)
	{
		float len = (new Vector2(transform.position.x, transform.position.y) - point).magnitude;
		if (len < 0.59f)
			return (true);
		return (false);
	}
	private void GetMidPoints()
	{
		float distanceX = transform.position.x - fencePosition.x;
		int count = (int)(distanceX * 0.85f);
		float diffDistance = distanceX / (float)count;
		for (int i = 0 ; i < count ; i++)
			midPoints.Add(new Vector2(fencePosition.x + (diffDistance * (count - i)), Random.Range(-2.5f, 2.0f)));
		finalPoint = new Vector2(fencePosition.x, Random.Range(-1.5f, 0.5f));
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == 3)
		{
			Health troop = other.gameObject.GetComponent<Health>();
			health.health -= (troop.health / 4.0f + troop.armor / 3.0f);
			CreateDamage.DoDamage(troop, CreateDamage.ArmorDamage(minDamage, maxDamage, troop), CreateDamage.HealthDamage(minDamage, maxDamage, troop));
			float compareArmor = troop.armor;
			float compareHealth = troop.health;
			if (troop.armor < 0.0f)
				compareArmor = 0.0f;
			if (troop.health < 0.0f)
				compareHealth = 0.0f;
			if (health.health <= 0.0f || (compareArmor + compareHealth) > 0.0f)
				Destroy(gameObject);
			return ;
		}
		if (other.gameObject.CompareTag("Fence") || other.gameObject.layer == LayerMask.NameToLayer("Boundary"))
			Destroy(gameObject);
	}
	private void DestroyThis()
	{
		Destroy(gameObject);
	}
	private void OnApplicationQuit()
	{
		quit = true;
	}
	private void OnDestroy()
	{
		if (!quit)
			Instantiate(missileExp, transform.position, Quaternion.identity);	
	}
}
