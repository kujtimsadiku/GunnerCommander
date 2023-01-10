using UnityEngine;

public class ReloadRPG : MonoBehaviour
{
	public GameObject RPG_Ammo;
	[HideInInspector] public bool reload = false;
	[HideInInspector] public int ammoInMag = 1;
	[SerializeField] private GameObject RpgReloadBar;
	public int totalAmmo;
	[SerializeField] private AudioClip reloadRPG;
	private void Update()
	{
		if (ammoInMag == 0)
			RPG_Ammo.SetActive(false);
	}
	public void ManageReloadBar()
	{
		if (!reload)
			return ;
		Vector3 scale = RpgReloadBar.transform.localScale;
		if (scale.x >= 2.0f)
		{
			reload = false;
			RpgReloadBar.transform.localScale = new Vector3(0.0f, scale.y, scale.z);
			AddAmmoBackToMag();
			return ;
		}
		RpgReloadBar.transform.localScale = new Vector3(scale.x + 0.035f, scale.y, scale.z);
	}
	public void AddAmmoBackToMag()
	{
		RPG_Ammo.SetActive(true);
		ammoInMag = 1;
	}
	public void Reload()
	{
		if (totalAmmo != 0 && ammoInMag != totalAmmo && ammoInMag != 1)
		{
			if ((Input.GetKey(KeyCode.R) && !reload) || (ammoInMag == 0 && !reload))
			{
				reload = true;
				AudioSource.PlayClipAtPoint(reloadRPG, new Vector3(0.29f, 0.0f, -10.0f), 0.45f);
			}
		}
	}
}
