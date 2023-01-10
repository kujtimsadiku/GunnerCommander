using UnityEngine;

public class BloodDeath : MonoBehaviour
{
	[SerializeField] private GameObject blood;
	
	private Health health;

	private void Awake()
	{
		health = GetComponent<Health>();
	}
	private void OnDestroy()
	{
		if (health.dead)
			Instantiate(blood, transform.position, Quaternion.identity);
	}
}
