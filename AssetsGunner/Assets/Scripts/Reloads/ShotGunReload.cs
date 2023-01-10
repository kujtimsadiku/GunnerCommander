using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunReload : MonoBehaviour
{
	public GameObject[] shotGunAmmo;
	public GameObject additionalShell;
	[HideInInspector] public bool reload = false;
	[HideInInspector] public int ammoInMag = 30;
	[SerializeField] private GameObject shotGunReloadBar;
	public int totalAmmo;
	private int maxAmmoInMag = 6;
	private bool moreAmmo = false;
	[SerializeField] private AudioClip reloadShotGun;
	public void ManageReloadBar()
	{
		if (!reload)
			return ;
		Vector3 scale = shotGunReloadBar.transform.localScale;
		if (scale.x >= 2.0f)
		{
			reload = false;
			shotGunReloadBar.transform.localScale = new Vector3(0.0f, scale.y, scale.z);
			AddAmmoBackToMag();
			return ;
		}
		shotGunReloadBar.transform.localScale = new Vector3(scale.x + 0.03f, scale.y, scale.z);
	}
	public void AddAmmoBackToMag()
	{
		if (!moreAmmo)
		{
			DefaultAdd();
			return ;
		}
		if (ammoInMag < 6)
		{
			if (DefaultAdd() == 1)
				return ;
		}
		if (totalAmmo > 6)
		{
			additionalShell.SetActive(true);
			ammoInMag = 7;
		}
	}
	private int DefaultAdd()
	{
		int ammoToAdd = 6;
		if (totalAmmo < 6)
		{
			ammoToAdd = totalAmmo;
			ammoInMag = ammoToAdd;
			int j = 5;
			for (int i = 0 ; i < ammoToAdd ; i++)
			{
				shotGunAmmo[j].SetActive(true);
				j--;
			}
			return (1);
		}
		ammoInMag = ammoToAdd;
		for (int i = 0 ; i < ammoToAdd ; i++)
			shotGunAmmo[i].SetActive(true);
		return (0);
	}
	public void Reload()
	{
		if (totalAmmo != 0 && ammoInMag != totalAmmo && ammoInMag != maxAmmoInMag)
		{
			if ((Input.GetKey(KeyCode.R) && !reload) || (ammoInMag == 0 && !reload))
			{
				reload = true;
				AudioSource.PlayClipAtPoint(reloadShotGun, new Vector3(0.29f, 0.0f, -10.0f), 0.25f * Volumes.effectVol);
			}
		}
	}
	public void InitMoreAmmo()
	{
		maxAmmoInMag += 1;
		if (totalAmmo < 36)
			totalAmmo = 36;
		if (ammoInMag < 6)
			AddAmmoBackToMag();
		moreAmmo = true;
		ammoInMag = 7;
	}
}
