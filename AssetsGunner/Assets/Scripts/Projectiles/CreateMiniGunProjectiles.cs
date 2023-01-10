using UnityEngine;

public class CreateMiniGunProjectiles : MonoBehaviour
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
		InvokeRepeating(nameof(CreateProjectile), invokeTime, 0.25f);
		Invoke(nameof(StopInvoke), 3.85f);
	}
	private void CreateProjectile()
	{
		GameObject shot = Instantiate(projectilePrefab, transform.position + position, Quaternion.identity);
		RifleShot dir = shot.GetComponent<RifleShot>();
		dir.direction = GetDirection();
	}
	private void StopInvoke()
	{
		CancelInvoke();
		Invoke(nameof(ResetMoving), canMoveAgain);
	}
	private void ResetMoving()
	{
		troop.projectile = false;
	}
	private Vector2 GetDirection()
	{
		Vector2 ret;
		ret = Quaternion.Euler(0.0f, 0.0f, Random.Range(-70.0f, -110.0f)) * new Vector3(0.0f, -1.0f, 0.0f);
		return (ret.normalized);
	}
}
