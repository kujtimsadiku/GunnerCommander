	using UnityEngine;

public class SubManDeath : MonoBehaviour
{
	[SerializeField] private Health health;
	[SerializeField] private GameObject death;

	private void Update()
	{
		if (health.health <= 0.0f && health.armor <= 0.0f)
		{
			death.SetActive(true);
			death.transform.position = transform.position;
			if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
				death.transform.localScale = new Vector3(-1.0f, transform.localScale.y, 1.0f);
			gameObject.SetActive(false);
		}
	}
}
