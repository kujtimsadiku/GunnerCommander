using UnityEngine;

public class ParticleScript : MonoBehaviour
{
	private ParticleSystem ps;

	private void Awake()
	{
		ps = GetComponent<ParticleSystem>();
		Invoke(nameof(DestroyThis), 35.0f);
		Invoke(nameof(HandleParticles), 0.5f);
	}
	private void HandleParticles()
	{
		ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
		ps.GetParticles(particles);
		for (int i = 0 ; i < ps.particleCount ; i++)
		{
			particles[i].startLifetime = Random.Range(10.0f, 25.0f);
		}
		ps.SetParticles(particles);
	}
	private void DestroyThis()
	{
		Destroy(gameObject);
	}
}
