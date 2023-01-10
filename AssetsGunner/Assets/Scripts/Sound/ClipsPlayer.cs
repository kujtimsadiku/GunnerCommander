using UnityEngine;

public class ClipsPlayer : MonoBehaviour
{
	[SerializeField] private AudioClip[] clips;
	[SerializeField] private Health health;
	private AudioSource audioSource;
	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}
	private void Update()
	{

	}
	private void PlayClip()
	{
		int i = 0;
		foreach (AudioClip clip in clips)
			i++;
		audioSource.clip = clips[Random.Range(0, i + 1)];
		audioSource.Play();
	}
}
