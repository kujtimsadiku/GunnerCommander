using UnityEngine;

public class EndLineDamage : MonoBehaviour
{
	[SerializeField] private LaneUnits enemies;
	[SerializeField] private GameObject effect;
	private AudioSource sound;
	private void Awake()
	{
		sound = GetComponent<AudioSource>();
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Allied"))
		{
			AddDamageToEnemies(other.gameObject);
			Destroy(other.transform.parent.gameObject);
		}
	}
	private void AddDamageToEnemies(GameObject other)
	{
		Health health = other.GetComponent<Health>();
		int enemyAmount = GetEnemyAmount();
		bool visited = false;
		if (enemyAmount == 0)
			return ;
		float addedDamage = ((health.health + health.armor) / 1.5f) / (float)enemyAmount;
		for (int i = 0; i < 7; i++)
		{
			for (int j = 0; j < enemies.lanes[i].Count; j++)
			{
				Health enem = enemies.lanes[i][j].troop.GetComponentInChildren<Health>();
				if (enem == null)
					continue ;
				CreateDamage.DoDamage(enem, CreateDamage.ArmorDamage(addedDamage, addedDamage, enem), CreateDamage.HealthDamage(addedDamage, addedDamage, enem));
				GameObject part = Instantiate(effect, enemies.lanes[i][j].troop.transform.GetChild(0).position, Quaternion.identity);
				if (enemies.lanes[i][j].troop.CompareTag("Tank"))
					part.GetComponent<BulletParticle>().HandleParticlesColor(1);
				else
					part.GetComponent<BulletParticle>().HandleParticlesColor(0);
				if (enem.health <= 0.0f && enem.armor <= 0.0f && visited == false)
				{
					sound.Play();
					visited = true;
				}
				
			}
		}
	}
	private int GetEnemyAmount()
	{
		int count = 0;
		for (int i = 0; i < 7; i++)
		{
			for (int j = 0; j < enemies.lanes[i].Count; j++)
				count++;
		}
		return (count);
	}
}
