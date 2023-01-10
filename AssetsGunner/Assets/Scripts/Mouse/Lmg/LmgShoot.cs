using UnityEngine;

public static class GotKill
{
	public static bool equipped;
	public static bool killed;
	public static float maxHealth;
	public static bool active;
}
public class LmgShoot : MonoBehaviour
{
	[SerializeField] private new AudioSource audio;
	[SerializeField] private MousePosition click;
	[SerializeField] private MouseShot shoot;
	private float buildUp = 0.0f;
	[HideInInspector] public bool build {get ; private set ;} = false;
	private float fireRate = 0.5f;
	private bool fire = false;
	private int shotsInARow = 0;
	private bool shot = false;
	public float minDamage;
	public float maxDamage;
	[SerializeField] private GameObject ground;
	[HideInInspector] public SpriteRenderer groundAlpha;
	[SerializeField] private PickGun selected;
	private Color ogColor;
	private void Awake()
	{
		GotKill.killed = false;
		GotKill.equipped = false;
		GotKill.active = false;
		groundAlpha = ground.GetComponent<SpriteRenderer>();
		ogColor = new Color(groundAlpha.color.r, groundAlpha.color.g, groundAlpha.color.b, 0.0f);
	}
	private void Update()
	{
		if (selected.gunSelected != 6 || selected.lmgAmmo.totalAmmo <= 0)
		{
			fire = false;
			fireRate = 0.5f;
			shotsInARow = 0;
			buildUp = 0.0f;
			build = false;
			groundAlpha.color = ogColor;
			return ;
		}
		if (click.click)
			fire = true;
		if (!click.holdClick && fire)
		{
			fire = false;
			fireRate = 0.5f;
			shotsInARow = 0;
			buildUp = 0.0f;
			build = false;
			groundAlpha.color = ogColor;
		}
		if (fire && !build)
		{
			if (buildUp > 50.0f)
				ManageGroundAlpha();
			if (!shot)
				ShootLmg();
		}
	}
	private void ManageGroundAlpha()
	{
		float num = buildUp - 50.0f;
		float unit = 0.196f / 50.0f;
		groundAlpha.color = new Color(groundAlpha.color.r, groundAlpha.color.g, groundAlpha.color.b, unit * num);
	}
	private void ShootLmg()
	{
		shot = true;
		selected.lmgAmmo.totalAmmo--;
		audio.Play();
		click.ChangeScale(1.2f);
		Vector3 pos = GetRecoil();
		shoot.CreateEffect(shoot.AddDamageToUnit(pos, 0.05f, minDamage, maxDamage), pos);
		if (GotKill.killed && GotKill.equipped)
		{
			GotKill.killed = false;
			buildUp += GotKill.maxHealth / 25.0f;
		}
		shotsInARow++;
		if (shotsInARow < 30)
			buildUp += 0.02f * (float)shotsInARow;
		else
			buildUp += 0.6f;
		if (buildUp >= 100.0f)
			build = true;
		if (!DevilActive.up)
		{
			buildUp = 0.0f;
			build = false;
		}
		Invoke(nameof(ResetShot), fireRate);
	}
	private Vector3 GetRecoil()
	{
		Vector3 ret = click.mousePosition;
		float addition = 0.03f;
		if (shotsInARow < 12)
			addition *= (float)shotsInARow;
		else
			addition *= 12.0f;
		ret.x += Random.Range(-addition, addition);
		ret.y += Random.Range(-addition, addition);
		return (ret);
	}
	private void OnDisable()
	{
		shotsInARow = 0;
		buildUp = 0.0f;
		build = false;
		groundAlpha.color = ogColor;
	}
	private void OnEnable()
	{
		groundAlpha.color = ogColor;
	}
	private void ResetShot()
	{
		if (fireRate > 0.1f)
			fireRate -= (fireRate / 7.5f);
		shot = false;
	}
}
