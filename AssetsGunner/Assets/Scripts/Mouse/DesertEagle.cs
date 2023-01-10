using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertEagle : MonoBehaviour
{
	[SerializeField] private AudioSource gunSound;
	[SerializeField] private MousePosition click;
	private bool canShoot = true;
	public float minDamage;
	public float maxDamage;
	public float fireRate;
	private MouseShot shoot;
	private void Awake()
	{
		shoot = GetComponent<MouseShot>();
	}
	private void Update()
	{
		if (shoot.gun.gunSelected != 4)
			return ;
		if (click.click && canShoot)
			ShootDessu();
	}
	private void ShootDessu()
	{
		canShoot = false;
		gunSound.Play();
		Invoke(nameof(ResetCanShoot), fireRate);
		click.ChangeScale(1.25f);
		shoot.CreateEffect(shoot.AddDamageToUnit(click.mousePosition, 0.05f, minDamage, maxDamage), click.mousePosition);
	}
	private void ResetCanShoot()
	{
		canShoot = true;
	}
}
