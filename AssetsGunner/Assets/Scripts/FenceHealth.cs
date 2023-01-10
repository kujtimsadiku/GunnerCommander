using UnityEngine;

public class FenceHealth : MonoBehaviour
{
	[SerializeField] private Health health;
	[SerializeField] private GameObject bar;
	[HideInInspector] public float totalHealth;
	private float currentTotalHealth = 0.0f;
	private float unit;
	private void Start()
	{
		totalHealth = health.health;
	}
	private void Update()
	{
		if (health.health < 0.0f)
			health.health = 0.0f;
		if (currentTotalHealth != totalHealth)
		{
			currentTotalHealth = totalHealth;
			unit = 2.0f / totalHealth;
		}
		bar.transform.localScale = new Vector3(unit * health.health, 0.5f, 1.0f);
	}
	private void UpgradeHealth(float amount)
	{
		health.health += amount;
		totalHealth += amount;
	}
}
