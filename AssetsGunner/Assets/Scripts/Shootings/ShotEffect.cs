using UnityEngine;

public class ShotEffect : MonoBehaviour
{
	public void DestroyThisGameObject()
	{
		Destroy(transform.parent.gameObject);
	}
}
