using UnityEngine;

public static class GunUp
{
	public static bool upIncoming;
	public static float minAmount;
	public static float maxAmount;
	public static int gun;
}
public class MouseShot : MonoBehaviour
{
	[SerializeField] private MousePosition mouse;
	[SerializeField] private float radius;
	[SerializeField] private float minPistolDamage;
	[SerializeField] private float maxPistolDamage;
	[SerializeField] private float minAkDamage;
	[SerializeField] private float maxAkDamage;
	[SerializeField] private GameObject devilLmg;
	public bool burstOn;
	[SerializeField] private AudioClip[] clips;
	private new AudioSource audio;
	[SerializeField] private AudioSource killClip;
	[HideInInspector] public int hitMaterial = (-1);
	[SerializeField] private GameObject effectPrefab;
	[HideInInspector] public bool clicked;
	[HideInInspector] public PickGun gun;
	[HideInInspector] public ShootRifle AK;
	[HideInInspector] public ShootShotGun shotGun;
	[SerializeField] private ShootRpg RPG;
	[SerializeField] private FireArrow arrow;
	private DesertEagle dessu;
	[SerializeField] private AudioClip pistolSound;
	private bool canDamage = true;
	private BringShop shop;
	public MouseSounds sounds;
	private bool divided = false;
	private void Awake()
	{
		GunUp.upIncoming = false;
		audio = GetComponent<AudioSource>();
		gun = GameObject.FindGameObjectWithTag("Guns").GetComponent<PickGun>();
		AK = GetComponent<ShootRifle>();
		shotGun = GetComponent<ShootShotGun>();
		dessu = GetComponent<DesertEagle>();
		shop = GameObject.FindGameObjectWithTag("Shop").GetComponent<BringShop>();
	}
	private void Update()
	{
		UpGun();
		clicked = false;
		if (shop.shopUp)
			ShopIsUp();
		else
			ShopIsDown();
		SelectGunToFire(gun.gunSelected);
		PickUp();
		if (burstOn && !divided)
			DividePistolDamage();
	}
	private void DividePistolDamage()
	{
		divided = true;
		minPistolDamage /= 1.55f;
		maxPistolDamage /= 1.55f;
	}
	private void UpGun()
	{
		burstOn = BurstOn.burst;
		if (!GunUp.upIncoming)
			return ;
		switch (GunUp.gun)
		{
			case 0:
				minPistolDamage = GunUp.minAmount;
				maxPistolDamage = GunUp.maxAmount;
				break ;
			case 1:
				minAkDamage = GunUp.minAmount;
				maxAkDamage = GunUp.maxAmount;
				break ;
			case 2:
				shotGun.minDamage = GunUp.minAmount;
				shotGun.maxDamage = GunUp.maxAmount;
				break ;
		}
		GunUp.upIncoming = false;
	}
	private void ShopIsUp()
	{
		canDamage = false;
		AK.enabled = false;
		shotGun.enabled = false;
		RPG.enabled = false;
		arrow.enabled = false;
		dessu.enabled = false;
		devilLmg.SetActive(false);
	}
	private void ShopIsDown()
	{
		canDamage = true;
		AK.enabled = true;
		shotGun.enabled = true;
		RPG.enabled = true;
		arrow.enabled = true;
		dessu.enabled = true;
		devilLmg.SetActive(true);
	}
	private void PickUp()
	{
		if (mouse.rightClick == false)
			return ;
		Collider2D[] collisions = Physics2D.OverlapCircleAll(mouse.transform.position, radius);
		foreach (Collider2D enemy in collisions)
		{
			if (enemy.gameObject.layer == LayerMask.NameToLayer("Money"))
			{
				if (enemy.gameObject.CompareTag("Ammo") && (gun.gunSelected == 0 || gun.gunSelected == 4))
					continue ;
				else
				{
					if (gun.gunSelected == 2)
					{
						if (shotGun.holding == false)
							mouse.ChangeScale(1.1f);
					}
					else
						mouse.ChangeScale(1.1f);
					ClickOnMoney(enemy.gameObject);
				}
			}
		}
	}
	private void SelectGunToFire(int ase)
	{
		switch (ase)
		{
			case 0:
				if (canDamage)
					FirePistol();
				break ;
			case 1:
				if (AK.canShoot)
				{
					FireAK();
					mouse.ChangeScale(1.1f);
					AK.canShoot = false;
				}
				break ;
			case 2:
				shotGun.selected = true;
				return ;
		}
		shotGun.selected = false;
	}
	private void FireAK()
	{
		clicked = true;
		CreateEffect(AddDamageToUnit(mouse.transform.position, radius, minAkDamage, maxAkDamage), mouse.transform.position);
	}
	private void FirePistol()
	{
		if (mouse.click)
		{
			mouse.ChangeScale(1.1f);
			CreateEffect(AddDamageToUnit(mouse.transform.position, radius, minPistolDamage, maxPistolDamage), mouse.transform.position);
			if (burstOn)
				Invoke(nameof(ShootSecondTime), 0.08f);
			clicked = true;
		}
	}
	private void ShootSecondTime()
	{
		mouse.ChangeScale(1.1f);
		CreateEffect(AddDamageToUnit(mouse.transform.position, radius, minPistolDamage, maxPistolDamage), mouse.transform.position);
		sounds.PlayerGunSound(pistolSound, 0.12f);
	}
	public void CreateEffect(int sign, Vector3 position)
	{
		if (canDamage == false)
			return ;
		hitMaterial = sign;
		if (sign == (-1))
			return ;
		GameObject effect = Instantiate(effectPrefab, position, Quaternion.identity);
		effect.GetComponent<BulletParticle>().HandleParticlesColor(sign);
	}
	public int AddDamageToUnit(Vector3 position, float rad, float minDamage, float maxDamage)
	{
		if (canDamage == false)
			return (0);
		Health thisEnemy = null;
		int ret = 2;
		Collider2D[] collisions = Physics2D.OverlapCircleAll(position, rad);
		float minMagnitude = Mathf.Infinity;
		foreach (Collider2D enemy in collisions)
		{
			if (enemy.gameObject.layer == LayerMask.NameToLayer("Enemy") || enemy.gameObject.layer == LayerMask.NameToLayer("Projectile"))
			{
				Vector2 distace = enemy.transform.position - transform.position;
				Health enem = enemy.gameObject.GetComponent<Health>();
				if (distace.magnitude < minMagnitude)
				{
					minMagnitude = distace.magnitude;
					thisEnemy = enem;
				}
			}
			else if (enemy.gameObject.layer == LayerMask.NameToLayer("BossBoxes"))
			{
				Vector2 distace = enemy.transform.position - transform.position;
				Health enem = enemy.gameObject.GetComponentInParent<Health>();
				if (distace.magnitude < minMagnitude)
				{
					minMagnitude = distace.magnitude;
					thisEnemy = enem;
				}
			}
		}
		if (thisEnemy != null)
		{
			CreateDamage.DoDamage(thisEnemy, CreateDamage.ArmorDamage(minDamage, maxDamage, thisEnemy), CreateDamage.HealthDamage(minDamage, maxDamage, thisEnemy));
			ret = GetRet(thisEnemy.gameObject.tag);
			if (thisEnemy.health <= 0.0f && thisEnemy.armor <= 0.0f)
			{
				if (!GotKill.active)
					killClip.Play();
				GotKill.killed = true;
				GotKill.maxHealth = thisEnemy.maxArmor + thisEnemy.maxHealth;
				GotKill.equipped = true;
				return (ret);
			}
			audio.clip = clips[0];
			audio.pitch = GetPitch(thisEnemy.maxHealth + thisEnemy.maxArmor, thisEnemy.health + thisEnemy.armor);
			audio.Play();

		}
		return (ret);
	}
	private void ClickOnMoney(GameObject money)
	{
		Destroy(money);
	}
	private int GetRet(string name)
	{
		if (name == "Troop")
			return (0);
		if (name == "Tank")
			return (1);
		return (-1);
	}
	private float GetPitch(float maxHealth, float health)
	{
		float unit = 0.35f / maxHealth;
		return (1.0f + unit * (maxHealth - health));
	}
}
