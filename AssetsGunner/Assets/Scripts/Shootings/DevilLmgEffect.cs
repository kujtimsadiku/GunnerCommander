using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilLmgEffect : MonoBehaviour
{
	[SerializeField] private AudioSource sound;
	[SerializeField] private AudioSource killSound;
	private void Awake()
	{
		Invoke(nameof(EnableBack), 0.3f);
		gameObject.SetActive(false);
	}
	private void EnableBack()
	{
		gameObject.SetActive(true);
		sound.Play();
		Invoke(nameof(DoDamage), 0.15f);
		Invoke(nameof(DestroyThis), 0.5f);
	}
	private void DoDamage()
	{
		bool played = false;
		Collider2D[] others = Physics2D.OverlapCircleAll(transform.position, 0.23f);
		foreach(Collider2D other in others)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
			{
				Health health = other.gameObject.GetComponent<Health>();
				CreateDamage.DoDamage(health, CreateDamage.ArmorDamage(20, 30, health), CreateDamage.HealthDamage(20, 30, health));
				if (health.armor <= 0.0f && health.health <= 0.0f && !played)
				{
					played = true;
					killSound.Play();
				}
			}
		}
	}
	private void DestroyThis()
	{
		Destroy(gameObject);
	}
}
