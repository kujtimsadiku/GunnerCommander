using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
	private int projectilesShot = 0;
	[SerializeField] private GameObject projectilePrefab;
	[SerializeField] private Vector3 position;
	private BoxCollider2D area;
	private Animator anim;
	private TrooperMoement troop;
	[SerializeField] private float invokeTime;
	[SerializeField] private float canMoveAgain;
	private void Awake()
	{
		area = GetComponent<BoxCollider2D>();
		anim = GetComponent<Animator>();
		troop = GetComponent<TrooperMoement>();
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("EnemyLaneManager"))
		{
			if (area.IsTouching(other.gameObject.GetComponent<BoxCollider2D>()) && projectilesShot == 0 && troop.shoot == false)
				StartProjectileCreation();
			else if (area.IsTouching(other.gameObject.GetComponent<CapsuleCollider2D>()) && projectilesShot == 1 && troop.shoot == false)
				StartProjectileCreation();
		}
	}
	private void StartProjectileCreation()
	{
		troop.projectile = true;
		projectilesShot++;
		anim.SetTrigger("Projectile");
		Invoke(nameof(CreateProjectile), invokeTime);
	}
	private void CreateProjectile()
	{
		Invoke(nameof(ResetMoving), canMoveAgain);
		Instantiate(projectilePrefab, transform.position + position, Quaternion.identity);
	}
	private void ResetMoving()
	{
		troop.projectile = false;
	}
}
