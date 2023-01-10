using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
	private bool activated = false;
	private bool visited = false;
	private void Update()
	{
		if (!activated)
			return ;
		if (!visited)
			Invoke(nameof(DestroyThisGameobject), 0.40f);
		visited = true;
	}
	private void OnDisable()
	{
		if (!activated)
			return ;
		Destroy(gameObject);
	}
	private void DestroyThisGameobject()
	{
		Destroy(gameObject);
	}
	public void ActivateSystem()
	{
		GetComponent<ParticleSystem>().Play();
		GetComponent<AudioSource>().Play();
		activated = true;
	}
}
