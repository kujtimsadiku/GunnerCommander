using UnityEngine;

public class DeathExplosion : MonoBehaviour
{
	[SerializeField] private GameObject explosion;
	private bool quit = false;
	public float expAmmount;

	private void OnApplicationQuit()
	{
		quit = true;
	}
	private void OnDestroy()
	{
		if (quit == false)
		{
			for (int i = 0; i < expAmmount; i++)
				Instantiate(explosion, new Vector3(transform.position.x + Random.Range(-0.4f, 0.45f), transform.position.y + Random.Range(-0.4f, 0.3f), 1.0f), Quaternion.identity);
		}
	}
}

