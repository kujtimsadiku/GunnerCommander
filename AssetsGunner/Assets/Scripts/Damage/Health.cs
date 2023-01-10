using UnityEngine;

public class Health : MonoBehaviour
{
	public float health;
	public float armor;
	[HideInInspector] public float maxHealth;
	[HideInInspector] public float maxArmor;
	[SerializeField] private float minReduce;
	[SerializeField] private float maxReduce;
	[HideInInspector] public float damageReducer;
	public float blockRate = 1.0f;
	[HideInInspector] public bool dead = false;
	private void Start()
	{
		maxHealth = health;
		maxArmor = armor;
		damageReducer = Random.Range(minReduce, maxReduce);
	}
	private void FixedUpdate()
	{
		if (health <= 0.0f && armor <= 0.0f)
			dead = true;
	}
	private void LateUpdate()
	{
		if (dead)
			Death();
	}
	private void Death()
	{
		if (gameObject.CompareTag("Fence"))
			return ;
		if (gameObject.layer == LayerMask.NameToLayer("Projectile"))
		{
			Destroy(gameObject);
			return ;
		}
		Destroy(transform.parent.gameObject);
	}
}
