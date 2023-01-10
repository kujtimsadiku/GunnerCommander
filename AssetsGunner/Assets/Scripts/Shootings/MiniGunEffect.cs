using UnityEngine;

public class MiniGunEffect : MonoBehaviour
{
	private void Start()
	{
		Invoke(nameof(DestroyThis), 0.05f);
	}
	private void DestroyThis()
	{
		Destroy(gameObject);
	}
}
