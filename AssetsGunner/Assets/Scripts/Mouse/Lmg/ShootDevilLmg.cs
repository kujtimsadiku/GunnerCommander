using UnityEngine;
using System.Collections;

public class ShootDevilLmg : MonoBehaviour
{
	[SerializeField] private MousePosition click;
	[SerializeField] private MouseShot shoot;
	[SerializeField] private GameObject[] shotParticle;
	[SerializeField] private GameObject effect;
	[SerializeField] private GameObject explosionParticle;
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private AudioSource normalShot;
	[SerializeField] private AudioSource kill;
	[SerializeField] private Health fenceHealth;
	[SerializeField] private SpriteRenderer fenceHealthOutLine;
	[SerializeField] private SpriteRenderer fenceHealthBar;
	[SerializeField] private Sprite[] outlines;
	[SerializeField] private Sprite defaultOutline;
	private Color defaultColor = new Color();
	[SerializeField] private float Damage;
	private float currentDamage;
	[HideInInspector] public int shotsHit {get ; private set ;}
	private Queue positions = new Queue();
	private bool shot = false;
	private bool explosionShot = false;
	[HideInInspector] public bool start = true;
	[HideInInspector] public bool acive = false;
	public AudioSource rapid;
	private bool invoked = false;
	private int shotCount = 0;
	private bool visited = false;
	[SerializeField] private AnimationCurve healthDrain;
	private void Start()
	{
		defaultColor = fenceHealthBar.color;
	}
	private void FixedUpdate()
	{
		if (!acive)
		{
			if (!visited)
			{
				fenceHealthOutLine.sprite = defaultOutline;
				fenceHealthBar.color = defaultColor;
				currentDamage = Damage;
				shotCount = 0;
				shotsHit = 0;
				rapid.volume = 0.2f;
				rapid.pitch = 0.9f;
				visited = true;
			}
			return ;
		}
		visited = false;
		if (!shot)
			CreateBullet();
	}
	private bool ExplosionShot()
	{
		if (start)
		{
			start = false;
			invoked = true;
			Invoke(nameof(CreateExplosion), 1.5f);
			return(false);
		}
		if (explosionShot)
		{
			explosionShot = false;
			ExplosionDamage();
			return (true);
		}
		else if (!invoked)
		{
			invoked = true;
			Invoke(nameof(CreateExplosion), Random.Range(0.5f, 1.7f));
		}
		return (false);
	}
	private void ExplosionDamage()
	{
		Vector3 pos = new Vector3();
		if (positions.Count != 0)
			pos = GetRecoilFromPosition((Vector3)positions.Dequeue());
		else
			pos = GetRecoil();
		Instantiate(explosionParticle, pos, Quaternion.identity);
	}
	private void CreateExplosion()
	{
		invoked = false;
		explosionShot = true;
	}
	private void CreateBullet()
	{
		shot = true;
		fenceHealthOutLine.sprite = outlines[Random.Range(0, 3)];
		fenceHealthBar.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
		fenceHealth.health -= healthDrain.Evaluate(shotCount);
		shotCount++;
		normalShot.Play();
		click.ChangeScale(1.4f);
		positions.Enqueue(new Vector3(click.mousePosition.x, click.mousePosition.y, click.mousePosition.z));
		GameObject bullet = Instantiate(bulletPrefab, BulletStartPos(), Quaternion.identity);
		Bullet target = bullet.GetComponent<Bullet>();
		target.target = GetRecoil();
		target.destroyed += CreateShotEffects;
		Invoke(nameof(ResetShot), 0.065f);
	}
	private void CreateShotEffects()
	{
		if (!ExplosionShot())
				Shoot();
		currentDamage = Damage * (1.0f + healthDrain.Evaluate((float)shotsHit * 1.5f));
		float pitchUnit = 0.25f / 550.0f;
		float volumeUnit = 0.35f / 550.0f;
		if (shotsHit < 550)
		{
			rapid.volume = 0.2f + volumeUnit * shotsHit;
			rapid.pitch = 0.9f + pitchUnit * shotsHit;
		}
		else
		{
			rapid.volume = 0.55f;
			rapid.pitch = 1.15f;
		}
		shotsHit++;
	}
	private Vector3 BulletStartPos()
	{
		Vector2 temp = new Vector2(click.mousePosition.x, click.mousePosition.y);
		Vector2 rand = new Vector2();
		rand.y = -4.3f;
		rand.x = Random.Range(-1.1f, 1.1f);
		temp += rand;
		return (new Vector3(temp.x, temp.y, 1.0f));
	}
	private void Shoot()
	{
		bool notQueue = false;
		Vector3 used = new Vector3();
		Vector3 pos = new Vector3();
		if (positions.Count != 0)
			pos = (Vector3)positions.Dequeue();
		else
			notQueue = true;
		for (int i = 0; i < 3; i++)
		{
			if (notQueue)
				used = GetRecoil();
			else
				used = GetRecoilFromPosition(pos);
			shoot.AddDamageToUnit(used, 0.1f, currentDamage, currentDamage);
			Instantiate(shotParticle[0], used, Quaternion.identity);
			Instantiate(shotParticle[1], used, Quaternion.identity);
			Instantiate(shotParticle[2], used, Quaternion.identity);
			Instantiate(effect, used, Quaternion.identity);
		}
		if (GotKill.killed && GotKill.equipped)
		{
			GotKill.killed = false;
			kill.Play();
		}
	}
	private Vector3 GetRecoil()
	{
		Vector2 ret = click.mousePosition;
		ret += Random.insideUnitCircle * 0.75f;
		return (new Vector3(ret.x, ret.y, 1.0f));
	}
	private Vector3 GetRecoilFromPosition(Vector3 pos)
	{
		Vector2 ret = pos;
		ret += Random.insideUnitCircle * 0.75f;
		return (new Vector3(ret.x, ret.y, 1.0f));
	}
	private void ResetShot()
	{
		shot = false;
	}
	public void PlayAudio()
	{
		rapid.Play();
	}
}
