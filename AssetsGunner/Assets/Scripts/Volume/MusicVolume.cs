using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class MusicVolume : MonoBehaviour
{
	[SerializeField] private AudioMixer musicMixer;

	private void Awake()
	{
		GetComponent<Slider>().value = Remember.music;
	}
	public void MusicsVolume()
	{
		Remember.music = GetComponent<Slider>().value;
		musicMixer.SetFloat("Music", Remember.music);
		Volumes.musicVol = Mathf.Pow(10.0f, Remember.music / 20.0f);
	}
}
