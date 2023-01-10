using UnityEngine;

public class DestroyerDeathAnimation : MonoBehaviour
{
	[SerializeField] private GameObject[] explosions;
	private int timer = 0;
	private void Start()
	{
		Invoke(nameof(Death), 2.0f);
	}
	private void FixedUpdate()
	{
		if (timer % 12 == 0)
		{
			CreateExp((int)Random.Range(0, 3), new Vector3(transform.position.x + Random.Range(-0.9f, 0.7f), transform.position.y + Random.Range(-0.6f, 0.1f), 1.0f));
			CreateExp((int)Random.Range(0, 3), new Vector3(transform.position.x + Random.Range(-0.9f, 0.7f), transform.position.y + Random.Range(-0.6f, 0.1f), 1.0f));
			CreateExp((int)Random.Range(0, 3), new Vector3(transform.position.x + Random.Range(-0.9f, 0.7f), transform.position.y + Random.Range(-0.6f, 0.1f), 1.0f));
		}
		timer++;
	}
	private void CreateExp(int index, Vector3 pos)
	{
		Instantiate(explosions[index], pos, Quaternion.identity);
	}
	private void Death()
	{
		Destroy(transform.parent.gameObject);
	}
}
