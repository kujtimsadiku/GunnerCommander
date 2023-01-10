using UnityEngine;

public class DestroyChild : MonoBehaviour
{
	[SerializeField] private GameObject child;
	private void OnEnable()
	{
		Destroy(child);
	}
}
