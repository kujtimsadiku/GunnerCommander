using UnityEngine;
using System.Collections;

public class ToxicBall : MonoBehaviour
{
	private GameObject enemyToDamage;
	private void Toxic()
	{
		Collider2D[] collisions = Physics2D.OverlapCircleAll(new Vector2(transform.position.x - 0.05f, transform.position.y), 0.2f);
		foreach (Collider2D enemy in collisions)
		{
			if (enemy.gameObject.layer == LayerMask.NameToLayer("Allied"))
			{
				enemyToDamage = enemy.gameObject;
				for (int i = 0; i < 3; i++)
				{
					if (enemy == null)
						return ;
					Invoke("Bleed", 0.5f);
				}
			}
			else
				return ;
		}
	}
	private void Bleed()
	{
		enemyToDamage.GetComponent<Health>().health -= 10.0f;
	}
}
