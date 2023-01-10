using UnityEngine;
using System.Collections;

public class DestroyerAnimation : MonoBehaviour
{
	[SerializeField] private GameObject[] missles;
	[SerializeField] private DamageMake dmg;
	[SerializeField] private GameObject death;
	private Health health;
	private Animator anim;
	private TrooperMoement movement;
	private Rigidbody2D rb;
	private int startShooting = 9;
	private bool moveShoot = true;
	private void Awake()
	{
		anim = GetComponent<Animator>();
		movement = GetComponent<TrooperMoement>();
		rb = GetComponent<Rigidbody2D>();
		health = GetComponent<Health>();
	}
	private void Start()
	{
		StartCoroutine(MoveShoot());
	}
	private void Update()
	{
		if (health.health <= 0.0f && health.armor <= 0.0f)
		{
			gameObject.SetActive(false);
			death.transform.position = gameObject.transform.position;
			death.SetActive(true);
		}
	}
	private IEnumerator MoveShoot()
	{
		while (moveShoot)
		{
			while (rb.position.x <= (float)startShooting)
			{
				anim.SetBool("Shoot", moveShoot);
				yield return new WaitForSeconds(2.7f);
				anim.SetBool("Shoot", false);
				yield return new WaitForSeconds(3.0f);
			}
			yield return null;
		}
		yield return null;
	}
	private void ShootMissles()
	{
		Instantiate(missles[0], new Vector3(transform.position.x - 0.65f, transform.position.y + 0.05f, transform.position.z), Quaternion.identity);
		Instantiate(missles[0], new Vector3(transform.position.x - 0.3f, transform.position.y + 0.03f, transform.position.z), Quaternion.identity);
		Instantiate(missles[1], new Vector3(transform.position.x - 0.4f, transform.position.y - 0.2f, transform.position.z), Quaternion.identity);
		Instantiate(missles[1], new Vector3(transform.position.x - 0.4f, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
	}
	private void CheckIfEnemyClose()
	{
		if (movement.targetEnemy == null)
			return ;
		if (movement.targetEnemy)
		{
			float multi = 1.1f;
			Health enemy = movement.targetEnemy.GetComponent<Health>();
			CreateDamage.DoDamage(enemy, CreateDamage.ArmorDamage(dmg.minDamage, dmg.maxDamage, enemy) * multi, CreateDamage.HealthDamage(dmg.minDamage, dmg.maxDamage, enemy) * multi);
		}
	}
}
