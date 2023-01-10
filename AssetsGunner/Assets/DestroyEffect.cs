using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
	private void Awake()
	{
		Invoke(nameof(DestroyThis), 2.5f);
	}
	private void DestroyThis()
	{
		Destroy(gameObject);
	}
}
