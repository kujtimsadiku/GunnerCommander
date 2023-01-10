using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartAudio : MonoBehaviour
{
	[SerializeField] private AudioSource sound;
	[SerializeField] private AudioClip[] clips;
	[SerializeField] private AudioSource killSound;
	private void Awake()
	{
		Invoke(nameof(DestroyThis), 1.5f);
	}
	private void Start()
	{
		int clp = Random.Range(0, 4);
		sound.clip = clips[clp];
		sound.Play();
	}
	private void DoDamage()
	{
		bool played = false;
		Collider2D[] others = Physics2D.OverlapCircleAll(transform.position, 0.55f);
		foreach(Collider2D other in others)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
			{
				Health health = other.gameObject.GetComponent<Health>();
				CreateDamage.DoDamage(health, CreateDamage.ArmorDamage(150, 170, health), CreateDamage.HealthDamage(150, 170, health));
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
