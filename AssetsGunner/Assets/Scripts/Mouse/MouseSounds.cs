using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSounds : MonoBehaviour
{
	[SerializeField] private AudioSource effect;
	[SerializeField] private AudioSource gunSound;
	[SerializeField] private MouseShot material;
	[SerializeField] private AudioClip[] materialAudio;
	[SerializeField] private AudioClip[] gunSounds;
	private void LateUpdate()
	{
		if (material.clicked)
		{
			ChooseRighSound();
			PlayerGunSound(null, 0.12f);
			ChooseRightMaterialSound();
		}
	}
	private void ChooseRighSound()
	{
		switch (material.gun.gunSelected)
		{
			case 0:
				gunSound.clip = gunSounds[0];
				break ;
			case 1:
				gunSound.clip = gunSounds[1];
				break ;
		}
	}
	public void PlayerGunSound(AudioClip sound, float volume)
	{
		if (sound == null)
		{
			gunSound.volume = volume;
			gunSound.Play();
		}
		else
		{
			gunSound.volume = volume;
			gunSound.clip = sound;
			gunSound.Play();
		}
	}
	private void ChooseRightMaterialSound()
	{
		if (material.hitMaterial == (-1))
			effect.clip = materialAudio[0];
		else if (material.hitMaterial == 0)
			effect.clip = materialAudio[1];
		else if (material.hitMaterial == 1)
			effect.clip = materialAudio[2];
		else
			effect.clip = materialAudio[3];
		effect.Play();
	}
}
