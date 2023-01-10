using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadAk : MonoBehaviour
{
	public GameObject[] AkAmmo;
	[HideInInspector] public bool reload = false;
	[HideInInspector] public int ammoInMag = 30;
	[SerializeField] private GameObject akReloadBar;
	public GameObject[] additionalAmmo;
	private bool moreAmmo = false;
	private int maxAmmoInMag = 30;
	public int totalAmmo;
	[SerializeField] private AudioClip reloadAk;
	public void ManageReloadBar()
	{
		if (!reload)
			return ;
		Vector3 scale = akReloadBar.transform.localScale;
		if (scale.x >= 2.0f)
		{
			reload = false;
			akReloadBar.transform.localScale = new Vector3(0.0f, scale.y, scale.z);
			AddAmmoBackToMag();
			return ;
		}
		akReloadBar.transform.localScale = new Vector3(scale.x + 0.05f, scale.y, scale.z);
	}
	public void AddAmmoBackToMag()
	{
		if (moreAmmo)
		{
			MoreAmmoToMag();
			return ;
		}
		DefaultAdd();
	}
	private int DefaultAdd()
	{
		int ammoToAdd = 30;
		if (totalAmmo < 30)
		{
			ammoToAdd = totalAmmo;
			ammoInMag = ammoToAdd;
			int j = 29;
			for (int i = 0 ; i < ammoToAdd ; i++)
			{
				AkAmmo[j].SetActive(true);
				j--;
			}
			return (1);
		}
		ammoInMag = ammoToAdd;
		for (int i = 0 ; i < ammoToAdd ; i++)
			AkAmmo[i].SetActive(true);
		return (0);
	}
	private void MoreAmmoToMag()
	{
		if (ammoInMag < 30)
		{
			if (DefaultAdd() == 1)
				return ;
		}
		int ammoToAdd = 15;
		if (totalAmmo < 45)
		{
			ammoToAdd = totalAmmo - 30;
			ammoInMag = 30 + ammoToAdd;
			int j = 14;
			for (int i = 0; i < ammoToAdd; i++)
			{
				additionalAmmo[j].SetActive(true);
				j--;
			}
			return ;
		}
		ammoInMag = 45;
		for (int i = 0; i < 15; i++)
			additionalAmmo[i].SetActive(true);
	}
	public void Reload()
	{
		if (totalAmmo != 0 && ammoInMag != totalAmmo && ammoInMag != maxAmmoInMag)
		{
			if ((Input.GetKey(KeyCode.R) && !reload) || (ammoInMag == 0 && !reload))
			{
				reload = true;
				AudioSource.PlayClipAtPoint(reloadAk, new Vector3(0.29f, 0.0f, -10.0f), 0.25f * Volumes.effectVol);
			}
		}
	}
	public void InitMoreAmmo()
	{
		if (totalAmmo < 150)
			totalAmmo = 150;
		if (ammoInMag != 30)
			AddAmmoBackToMag();
		ammoInMag = 45;
		maxAmmoInMag = 45;
		moreAmmo = true;
	}
}
