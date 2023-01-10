using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;



public class EffectVolume : MonoBehaviour
{
	[SerializeField] private AudioMixer effectMixer;

	private void Awake()
	{
		GetComponent<Slider>().value = Remember.effect;
	}
	public void EffectsVolume()
	{
		// the value we have set for the slider -40 to 0
		Remember.effect = GetComponent<Slider>().value;
		// set the slider value for the mixer. [MAX -40] [MIN 0] dB
		effectMixer.SetFloat("Effects", Remember.effect);
		// this value is for the audioclips that will play without using audiosource
		Volumes.effectVol = Mathf.Pow(10.0f, Remember.effect / 20.0f);
	}
}
