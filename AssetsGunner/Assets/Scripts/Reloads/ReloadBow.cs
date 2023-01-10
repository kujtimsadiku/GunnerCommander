using TMPro;
using UnityEngine;

public class ReloadBow : MonoBehaviour
{
	public GameObject bow_Ammo;
	public GameObject empty;
	[HideInInspector] public bool reload = false;
	[HideInInspector] public int ammoInMag = 1;
	public int totalAmmo = 24;
	[SerializeField] private GameObject bowReloadBar;
	[SerializeField] private TMP_Text ammoToScreen;
	private void Update()
	{
		ammoToScreen.text = totalAmmo.ToString();
		ammoToScreen.text += "/24";
		if (ammoInMag == 0)
		{
			bow_Ammo.SetActive(false);
			empty.SetActive(true);
		}
		else
			empty.SetActive(false);
	}
	public void ManageReloadBar()
	{
		if (!reload)
			return ;
		Vector3 scale = bowReloadBar.transform.localScale;
		if (scale.x >= 2.0f)
		{
			reload = false;
			bowReloadBar.transform.localScale = new Vector3(0.0f, scale.y, scale.z);
			AddAmmoBackToMag();
			return ;
		}
		bowReloadBar.transform.localScale = new Vector3(scale.x + 0.12f, scale.y, scale.z);
	}
	public void AddAmmoBackToMag()
	{
		bow_Ammo.SetActive(true);
		ammoInMag = 1;
	}
	public void Reload()
	{
		if (ammoInMag == 0 && !reload && totalAmmo != 0)
		{
			reload = true;
		}
	}
}
