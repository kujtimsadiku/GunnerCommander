using System.Collections.Generic;
using UnityEngine;

public class JavelinTravel : MonoBehaviour
{
	[HideInInspector] public List<Vector2> points;
	[SerializeField] private float damageMin;
	[SerializeField] private float damageMax;
	[SerializeField] private float attackRadius;
	[HideInInspector] public System.Action destroyed;
	[SerializeField] private AudioSource hitSounds;
	[SerializeField] private AudioClip[] hits;
	[SerializeField] private AudioSource metalHitSounds;
	[SerializeField] private AudioClip[] meatals;
	[SerializeField] private AudioSource flySounds;
	[SerializeField] private AudioClip[] flys;
	[SerializeField] private AudioSource killSound;
	private SpriteRenderer sprite;
	private int travelled = 0;
	private void Awake()
	{
		sprite = GetComponent<SpriteRenderer>();
	}
	private void Start()
	{
		GetStartingDirection();
	}
	private void FixedUpdate()
	{
		if ((points.Count - 2) < (travelled + 1))
		{
			destroyed.Invoke();
			this.enabled = false;
			sprite.enabled = false;
			Invoke(nameof(DestroyThis), 1.8f);
		}
		MoveToPosition();
		Vector2 dir = GetDirection();
		DamageEnemies(dir);
		travelled++;
	}
	private void DamageEnemies(Vector2 dir)
	{
		bool killed = false;
		Vector2 attackPoint = new Vector2(transform.position.x, transform.position.y) + (dir * 0.5f);
		Collider2D[] collisions = Physics2D.OverlapCircleAll(attackPoint, attackRadius);
		foreach(Collider2D other in collisions)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
			{
				Health health = other.gameObject.GetComponent<Health>();
				CreateDamage.DoDamage(health, CreateDamage.ArmorDamage(damageMin, damageMax, health), CreateDamage.HealthDamage(damageMin, damageMax, health));
				if (health.health <= 0.0f && health.armor <= 0.0f)
					killed = true;
			}
		}
		if (killed)
			killSound.Play();
	}
	private Vector2 GetAttackPosition()
	{
		return (Vector2.zero);
	}
	private void MoveToPosition()
	{
		transform.position = new Vector3(points[travelled + 1].x, points[travelled + 1].y, 1.0f);
	}
	private void GetStartingDirection()
	{
		if (points.Count < 2)
			return ;
		Vector2 dir = points[1] - points[0];
		if (dir != Vector2.zero) 
		{
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}
	private Vector2 GetDirection()
	{
		Vector2 dir = points[travelled + 1] - points[travelled];
		if (dir != Vector2.zero) 
		{
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
		return (dir.normalized);
	}
	private void DestroyThis()
	{
		Destroy(gameObject);
	}
}
