using UnityEngine;

public class MissleEffectDestroy : MonoBehaviour
{
	private void Awake()
	{
		Invoke(nameof(DestroyThisGameObject), 0.5f);
	}
	private void DestroyThisGameObject()
	{
		Destroy(gameObject);	
	}
}
