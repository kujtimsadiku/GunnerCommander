using UnityEngine;

public class BulletParticle : MonoBehaviour
{
	private ParticleSystem ps;
	public float hSliderValueR = 0.0F;
	public float hSliderValueG = 0.0F;
	public float hSliderValueB = 0.0F;
	private void Awake()
	{
		ps = GetComponent<ParticleSystem>();
		Invoke(nameof(DestroyThis), 0.31f);
	}
	public void HandleParticlesColor(int hitType)
	{
		ParticleSystem.MainModule Mo = ps.main;
		switch (hitType)
		{
			case 0:
				Mo.startColor = new Color(0.556f, 0.0f, 0.0f); // red
				break ;
			case 1:
				Mo.startColor = new Color(0.737f, 0.694f, 0.694f); // grey
				break ;
			case 2:
				Mo.startColor = new Color(0.494f, 0.337f, 0.184f); // mud
				break ;
		}
		ps.Play();
	}
	private void DestroyThis()
	{
		Destroy(gameObject);
	}
}
