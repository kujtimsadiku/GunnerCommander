using UnityEngine;

public static class ChargeOwned
{
	public static bool yes;
}
public class ShootShotGun : MonoBehaviour
{
	private MouseShot shoot;
	[SerializeField] private MousePosition click;
	[SerializeField] private float shotDelay;
	private bool canShoot = true;
	[HideInInspector] public bool holding = false;
	[SerializeField] private int pelletCount;
	[HideInInspector] public bool selected = false;
	[SerializeField] public float minDamage;
	[SerializeField] public float maxDamage;
	[SerializeField] private GameObject mouseCursor;
	private int startHold = 0;
	private bool initialHold = false;
	private float transformUnit => 1.25f / 120.0f;
	private int chargeTimer = 0;
	[SerializeField] private AudioClip shotGunSound;
	[SerializeField] private AudioClip bigGunSound;
	[SerializeField] private GameObject shotEffect;
	[SerializeField] private AudioSource charge;
	private void Awake()
	{
		ChargeOwned.yes = false;
		shoot = GetComponent<MouseShot>();
	}
	private void FixedUpdate()
	{
		if (initialHold)
			startHold++;
		if (!selected || !canShoot)
		{
			mouseCursor.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, mouseCursor.transform.rotation.w);
			return ;
		}
		if (holding)
		{
			if (chargeTimer == 0)
			{
				mouseCursor.transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);
				mouseCursor.transform.Rotate(new Vector3(0.0f, 0.0f, 90.0f));
			}
			else
			{
				if (mouseCursor.transform.localScale.x > 0.75f)
				{
					mouseCursor.transform.localScale = new Vector3(mouseCursor.transform.localScale.x - transformUnit, mouseCursor.transform.localScale.y - transformUnit, 1.0f);
					mouseCursor.transform.Rotate(new Vector3(0.0f, 0.0f, -4.0f));
				}
				else
				{
					mouseCursor.transform.localScale = new Vector3(0.75f, 0.75f, 1.0f);
					mouseCursor.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, mouseCursor.transform.rotation.w);
				}
			}
			chargeTimer++;
		}
	}
	private void Update()
	{
		if (selected == false)
			return ;
		if (click.click)
			CreateNormalShot();
		else if (click.holdClick && startHold > 15 && ChargeOwned.yes)
			StartCreateChargedShot();
		else if (click.holdClick && ChargeOwned.yes)
			ClearHold();
		else
		{
			initialHold = false;
			mouseCursor.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, mouseCursor.transform.rotation.w);
		}
		if (holding == true && click.holdClick == false)
			CreateChargedShot();

	}
	private void CreateNormalShot()
	{
		if (!canShoot || shoot.gun.reloadShotGun.reload || shoot.gun.reloadShotGun.totalAmmo == 0)
			return ;
		shoot.CreateEffect(shoot.AddDamageToUnit(click.transform.position, 0.25f, minDamage, maxDamage), click.transform.position);
		for (int i = 0 ; i < pelletCount ; i++)
		{
			Vector2 vec = Random.insideUnitCircle;
			Vector3 position = new Vector3(click.transform.position.x + vec.x, click.transform.position.y + vec.y, click.transform.position.z);
			shoot.CreateEffect(shoot.AddDamageToUnit(position, 0.1f, minDamage, maxDamage), position);
		}
		canShoot = false;
		click.ChangeScale(1.25f);
		chargeTimer = 0;
		startHold = 0;
		ManageMag();
		shoot.sounds.PlayerGunSound(shotGunSound, 0.19f);
		Invoke(nameof(ResetCanShoot), shotDelay);
	}
	private void CreateChargedShot()
	{
		charge.Stop();
		if (chargeTimer < 25 || shoot.gun.reloadShotGun.reload || shoot.gun.reloadShotGun.totalAmmo == 0)
		{
			holding = false;
			CreateNormalShot();
			return ;
		}
		shoot.CreateEffect(shoot.AddDamageToUnit(click.transform.position, 0.6f, minDamage, maxDamage), click.transform.position);
		GameObject effect = Instantiate(shotEffect, click.transform.position, Quaternion.identity);
		effect.transform.localScale = new Vector3(0.75f, 0.75f, 1.0f);
		for (int i = 0 ; i < ((pelletCount * 2) + 5) ; i++)
		{
			Vector2 vec = Random.insideUnitCircle * 1.25f;
			Vector3 position = new Vector3(click.transform.position.x + vec.x, click.transform.position.y + vec.y, click.transform.position.z);
			shoot.CreateEffect(shoot.AddDamageToUnit(position, 0.1f, minDamage * 2.3f, maxDamage * 2.3f), position);
			effect = Instantiate(shotEffect, position, Quaternion.identity);
			effect.transform.localScale = new Vector3(0.75f, 0.75f, 1.0f);
		}
		mouseCursor.transform.localScale = new Vector3(0.75f, 0.75f, 1.0f);
		mouseCursor.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, mouseCursor.transform.rotation.w);
		canShoot = false;
		click.ChangeScale(1.5f);
		chargeTimer = 0;
		startHold = 0;
		holding = false;
		ManageChargeMag();
		shoot.sounds.PlayerGunSound(bigGunSound, 0.24f);
		Invoke(nameof(ResetCanShoot), shotDelay);
	}
	private void ClearHold()
	{
		if (shoot.gun.reloadShotGun.reload || shoot.gun.reloadShotGun.totalAmmo == 0)
			return ;
		initialHold = true;
	}
	private void StartCreateChargedShot()
	{
		if (!canShoot || shoot.gun.reloadShotGun.reload || shoot.gun.reloadShotGun.totalAmmo == 0)
			return ;
		if (!holding)
			chargeTimer = 0;
		if (holding)
			return ;
		charge.Play();
		initialHold = false;
		holding = true;
	}
	private void ResetCanShoot()
	{
		canShoot = true;
	}
	private int ManageChargeMag()
	{
		int i = 0;
		bool one = false;
		if (ShotGunAmmoUp.up && shoot.gun.reloadShotGun.ammoInMag == 7)
		{
			shoot.gun.reloadShotGun.additionalShell.SetActive(false);
			one = true;
		}
		while (i < shoot.gun.reloadShotGun.shotGunAmmo.Length)
		{
			if (shoot.gun.reloadShotGun.shotGunAmmo[i].activeSelf == true)
			{
				shoot.gun.reloadShotGun.shotGunAmmo[i].SetActive(false);
				if (one || shoot.gun.reloadShotGun.ammoInMag == 1)
					break ;
				one = true;
			}
			i++;
		}
		if (shoot.gun.reloadShotGun.ammoInMag == 1)
		{
			shoot.gun.reloadShotGun.totalAmmo -= 1;
			shoot.gun.reloadShotGun.ammoInMag -= 1;
			return (0);
		}
		shoot.gun.reloadShotGun.totalAmmo -= 2;
		shoot.gun.reloadShotGun.ammoInMag -= 2;
		return (0);
	}
	private int ManageMag()
	{
		int i = 0;
		if (ShotGunAmmoUp.up && shoot.gun.reloadShotGun.ammoInMag == 7)
			shoot.gun.reloadShotGun.additionalShell.SetActive(false);
		else
		{
			while (i < shoot.gun.reloadShotGun.shotGunAmmo.Length)
			{
				if (shoot.gun.reloadShotGun.shotGunAmmo[i].activeSelf == true)
				{
					shoot.gun.reloadShotGun.shotGunAmmo[i].SetActive(false);
					break ;
				}
				i++;
			}
		}
		shoot.gun.reloadShotGun.totalAmmo--;
		shoot.gun.reloadShotGun.ammoInMag--;
		return (0);
	}
}
