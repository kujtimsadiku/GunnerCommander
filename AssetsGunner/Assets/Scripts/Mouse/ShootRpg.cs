using UnityEngine;
using System.Collections;

public class ShootRpg : MonoBehaviour
{
	[SerializeField] private MousePosition click;
	[SerializeField] private AudioSource shotSound;
	[SerializeField] private AudioSource killSound;
	[SerializeField] private GameObject rocket;
	[SerializeField] private AnimationCurve damageRange;
	[SerializeField] private ReloadRPG ammo;
	public bool infiniteAmmo;
	private Queue targetPositions = new Queue();
	private MouseShot shoot;
	private void Awake()
	{
		shoot = GetComponent<MouseShot>();
	}
	private void Update()
	{
		if (shoot.gun.gunSelected != 3)
			return ;
		if (click.click && ammo.ammoInMag == 1)
			ShootRocket();
	}
	private void ShootRocket()
	{
		if (!infiniteAmmo)
		{
			ammo.ammoInMag -= 1;
			ammo.totalAmmo -= 1;
		}
		shotSound.Play();
		GameObject rpg = Instantiate(rocket, GetRocketPosition(), Quaternion.identity);
		RpgRocket active = rpg.GetComponent<RpgRocket>();
		active.target = new Vector2(click.mousePosition.x, click.mousePosition.y);
		targetPositions.Enqueue(new Vector2(active.target.x, active.target.y));
		active.destroyed += CreateDamageArea;
	}
	private void CreateDamageArea()
	{
		if (targetPositions.Count == 0)
			return ;
		Vector2 point = (Vector2)targetPositions.Dequeue();
		Collider2D[] others = Physics2D.OverlapCircleAll(point, 2.0f);
		foreach(Collider2D other in others)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
			{
				Health health = other.gameObject.GetComponent<Health>();
				if (health == null)
					continue ;
				CreateDamageToEnemy(health, point);
				if (health.health <= 0.0f && health.armor <= 0.0f)
					killSound.Play();
				else
				{
					int sign = 0;
					if (other.gameObject.CompareTag("Tank"))
						sign = 1;
					else if (other.gameObject.CompareTag("Rocket"))
						sign = (-1);
					shoot.CreateEffect(sign, other.transform.position);
				}
			}
		}
	}
	private void CreateDamageToEnemy(Health enemy, Vector2 point)
	{
		Vector2 distance = new Vector2(enemy.transform.position.x, enemy.transform.position.y) - (Vector2)point;
		float dmg = damageRange.Evaluate(distance.magnitude);
		if (enemy.gameObject.CompareTag("Tank") && distance.magnitude < 1.5f)
			dmg = dmg * 1.75f;
		CreateDamage.DoDamage(enemy, CreateDamage.ArmorDamage(dmg, dmg, enemy), CreateDamage.HealthDamage(dmg, dmg, enemy));
	}
	private Vector3 GetRocketPosition()
	{
		Vector3 ret = new Vector3();
		if (click.mousePosition.x > 1.5f)
			ret.x = (-10.0f);
		else
			ret.x = 10.0f;
		if (click.mousePosition.y < 0.0f)
			ret.y = Random.Range(1.0f, 5.5f);
		else
			ret.y = Random.Range((-1.0f), (-5.5f));
		ret.z = 1.0f;
		return (ret);
	}
}
