using UnityEngine;

public class ShootRifle : MonoBehaviour
{
	private MouseShot shooting;
	[SerializeField] private MousePosition click;
	[SerializeField] private float fireRate;
	[HideInInspector] public bool canShoot = false;
	private bool startFire = false;
	private void Awake()
	{
		shooting = GetComponent<MouseShot>();
	}
	private void Update()
	{
		if (shooting.gun.gunSelected != 1 || !(shooting.gun.reloadAk.totalAmmo > 0) || shooting.gun.reloadAk.reload)
		{
			canShoot = false;
			startFire = false;
			CancelInvoke();
			return ;
		}
		if (click.holdClick)
		{
			if (!startFire)
			{
				canShoot = true;
				ManageMag();
				InvokeRepeating(nameof(Shooting), fireRate, fireRate);
			}
			startFire = true;
		}
		else
		{
			CancelInvoke();
			startFire = false;
		}
	}
	private void Shooting()
	{
		if (ManageMag() == 0)
			canShoot = true;
	}
	private int ManageMag()
	{
		TakeAmmo();
		shooting.gun.reloadAk.totalAmmo--;
		shooting.gun.reloadAk.ammoInMag--;
		return (0);
	}
	private void TakeAmmo()
	{
		if (AkAdditionalAmmo.additionalAmmo == false)
		{
			TakeDefaultAmmo();
			return ;
		}
		if (shooting.gun.reloadAk.ammoInMag > 30)
			TakeAdditionalAmmo();
		else
			TakeDefaultAmmo();
	}
	private void TakeDefaultAmmo()
	{
		int i = 0;
		while (i < shooting.gun.reloadAk.AkAmmo.Length)
		{
			if (shooting.gun.reloadAk.AkAmmo[i].activeSelf == true)
			{
				shooting.gun.reloadAk.AkAmmo[i].SetActive(false);
				break ;
			}
			i++;
		}
	}
	private void TakeAdditionalAmmo()
	{
		int i = 0;
		while (i < shooting.gun.reloadAk.additionalAmmo.Length)
		{
			if (shooting.gun.reloadAk.additionalAmmo[i].activeSelf == true)
			{
				shooting.gun.reloadAk.additionalAmmo[i].SetActive(false);
				return ;
			}
			i++;
		}
	}
}
