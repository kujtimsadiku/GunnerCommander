using UnityEngine;

public class KillDestroyer : MonoBehaviour
{
	private void DestroyerDeath()
	{
		Destroy(transform.gameObject);
	}
}
