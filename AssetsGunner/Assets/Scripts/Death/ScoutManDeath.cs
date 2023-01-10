using UnityEngine;

public class ScoutManDeath : MonoBehaviour
{
	[SerializeField] private Health health;
	[SerializeField] private GameObject death;
	private void Update()
	{
		if (health.health <= 0.0f && health.armor <= 0.0f)
		{
			death.SetActive(true);
			death.transform.position = transform.position;
			death.transform.localScale = transform.localScale;
			gameObject.SetActive(false);
		}
	}
}

