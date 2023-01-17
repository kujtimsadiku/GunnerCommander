using UnityEngine;
using TMPro;

public static class AmmoDrop
{
	public static bool coming;
	public static int selected;
	public static int owned;
}
public class PickGun : MonoBehaviour
{
	public int[] ownedGuns = {1, 0, 0, 0, 0, 0, 0};
	private bool somethingOwned = false;
	[HideInInspector] public int gunSelected = 0;
	[SerializeField] private GameObject[] guns;
	[SerializeField] private AudioClip sound;
	[SerializeField] private TMP_Text ammoToScreen;
	[SerializeField] private TMP_Text shotGunAmmoToScreen;
	[SerializeField] private TMP_Text RpgAmmoToScreen;
	[HideInInspector] public bool AmmoComing = false;
	[HideInInspector] public bool arrowComing = false;
	[HideInInspector] public ReloadAk reloadAk;
	[HideInInspector] public ShotGunReload reloadShotGun;
	[HideInInspector] public ReloadRPG relaodRPG;
	[HideInInspector] public ReloadBow reloadBow;
	[SerializeField] private BringShop shopUp;
	[SerializeField] private LevelChange currentLevel;
	[HideInInspector] public LmgAmmo lmgAmmo;
	private void Awake()
	{
		reloadAk = GetComponent<ReloadAk>();
		reloadShotGun = GetComponent<ShotGunReload>();
		relaodRPG = GetComponent<ReloadRPG>();
		reloadBow = GetComponent<ReloadBow>();
		lmgAmmo = GetComponent<LmgAmmo>();
	}
	private void Start()
	{
		reloadAk.AddAmmoBackToMag();
		reloadShotGun.AddAmmoBackToMag();
		relaodRPG.AddAmmoBackToMag();
		reloadBow.AddAmmoBackToMag();
	}
	private void FixedUpdate()
	{
		for (int i = 0 ; i < guns.Length ; i++)
		{
			if (i == gunSelected)
				guns[i].SetActive(true);
			else
				guns[i].SetActive(false);
		}
		ammoToScreen.text = reloadAk.totalAmmo.ToString();
		shotGunAmmoToScreen.text = reloadShotGun.totalAmmo.ToString();
		RpgAmmoToScreen.text = relaodRPG.totalAmmo.ToString();
		reloadAk.ManageReloadBar();
		reloadShotGun.ManageReloadBar();
		relaodRPG.ManageReloadBar();
		reloadBow.ManageReloadBar();
		if (!somethingOwned)
			CheckIfSomethingOwned();
	}
	private void CheckIfSomethingOwned()
	{
		for (int i = 0 ; i < ownedGuns.Length ; i++)
		{
			if (ownedGuns[i] == 1 && i != 4 && i != 0)
			{
				somethingOwned = true;
				AmmoDrop.owned = 1;
				return ;
			}
		}
	}
	private void Update()
	{
		ChooseGun();
		if (gunSelected == 1)
			reloadAk.Reload();
		else if (gunSelected == 2)
			reloadShotGun.Reload();
		else if (gunSelected == 3)
			relaodRPG.Reload();
		else if (gunSelected == 5)
			reloadBow.Reload();
		if (AmmoComing)
		{
			AmmoComing = false;
			if (gunSelected == 1)
			{
				if (AkAdditionalAmmo.additionalAmmo)
					reloadAk.totalAmmo += 120;
				else
					reloadAk.totalAmmo += 90;
			}
			else if (gunSelected == 2)
				reloadShotGun.totalAmmo += 18;
			else if (gunSelected == 3)
			{
				if (RpgDrops.dropDrop)
					relaodRPG.totalAmmo += 2;
				else
					relaodRPG.totalAmmo += 1;
			}
			else if (gunSelected == 5)
			{
				reloadBow.totalAmmo += 12;
				if (reloadBow.totalAmmo > 24)
					reloadBow.totalAmmo = 24;
			}
			else if (gunSelected == 6)
				lmgAmmo.totalAmmo += 90;
		}
		if (AmmoDrop.coming)
		{
			AmmoDrop.coming = false;
			if (AmmoDrop.selected == 1)
			{
				if (AkAdditionalAmmo.additionalAmmo)
					reloadAk.totalAmmo += 120;
				else
					reloadAk.totalAmmo += 90;
			}
			else if (AmmoDrop.selected == 2)
				reloadShotGun.totalAmmo += 18;
			else if (AmmoDrop.selected == 3)
			{
				if (RpgDrops.shopDrop)
					relaodRPG.totalAmmo += 3;
				else
					relaodRPG.totalAmmo += 2;
			}
			else if (AmmoDrop.selected == 5)
			{
				reloadBow.totalAmmo += 12;
				if (reloadBow.totalAmmo > 24)
					reloadBow.totalAmmo = 24;
			}
			else if (AmmoDrop.selected == 6)
				lmgAmmo.totalAmmo += 120;
		}
		if (arrowComing)
		{
			if (reloadBow.totalAmmo < 24)
				reloadBow.totalAmmo += 1;
			arrowComing = false;
		}
	}
	private void ChooseGun()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
			gunSelected = 0;
		else if (Input.GetKeyDown(KeyCode.Alpha2) && ownedGuns[1] == 1)
			gunSelected = 1;
		else if (Input.GetKeyDown(KeyCode.Alpha3) && ownedGuns[2] == 1)
			gunSelected = 2;
		else if (Input.GetKeyDown(KeyCode.Alpha4) && ownedGuns[3] == 1)
			gunSelected = 3;
		else if (Input.GetKeyDown(KeyCode.Alpha5) && ownedGuns[4] == 1)
			gunSelected = 4;
		else if (Input.GetKeyDown(KeyCode.Alpha6) && ownedGuns[5] == 1)
			gunSelected = 5;
		else if (Input.GetKeyDown(KeyCode.Alpha7) && ownedGuns[6] == 1)
			gunSelected = 6;
		if (Input.GetAxis("Wheel") < 0f && !shopUp.shopUp) // forward
		{
			if (gunSelected < guns.Length - 1)
				gunSelected++;
			else
				gunSelected = 0;
			if (ownedGuns[gunSelected] == 0)
			{
				int i = gunSelected;
				while(ownedGuns[i] == 0)
				{
					if (i >= (ownedGuns.Length - 1) && ownedGuns[i] == 0)
					{
						gunSelected = 0;
						return ;
					}
					i++;
				}
				gunSelected = i;
			}
		}
		else if (Input.GetAxis("Wheel") > 0f && !shopUp.shopUp) // backwards
		{
			if (gunSelected > 0)
				gunSelected--;
			else
				gunSelected = guns.Length - 1;
			if (ownedGuns[gunSelected] == 0)
			{
				int i = gunSelected;
				while(ownedGuns[i] == 0)
				{
					if (i < 0)
					{
						int j = ownedGuns.Length - 1;
						while (ownedGuns[j] == 0)
							j--;
						gunSelected = j;
						return ;
					}
					i--;
				}
				gunSelected = i;
			}
		}
	}
}
