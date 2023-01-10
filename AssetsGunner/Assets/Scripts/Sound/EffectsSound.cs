using UnityEngine;

public class EffectsSound : MonoBehaviour
{
	[SerializeField] private AudioClip[] shootClip;
	[SerializeField] private AudioClip[] deathClips;
	private AudioSource audioClip;
	private void Awake() {
		audioClip = GetComponent<AudioSource>();
	}
	private void OnEnable()
	{
		if (deathClips.Length == 0)
			return ;
		audioClip.clip = deathClips[Random.Range(0, deathClips.Length)];
		audioClip.Play();
	}
	private void ShootClip()
	{
		audioClip.clip = shootClip[0];
		audioClip.Play();
	}
}
