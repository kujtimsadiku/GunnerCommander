using UnityEngine;

public class FireArrow : MonoBehaviour
{
	[SerializeField] private MousePosition click;
	[SerializeField] private MouseShot shoot;
	[SerializeField] private ReloadBow reloadBow;
	[HideInInspector] public bool canShoot;
	[SerializeField] private GameObject arrowPrefab;
	private LineRenderer points;
	private Arrow currentArrow = null;
	private bool arrowCharge = false;
	private bool release = false;
	private float addition = 0.01f;
	private float totalAdditon = 0.0f;
	private int chargeCount = 0;
	private new AudioSource audio;
	[SerializeField] private AudioClip[] clips;
	[SerializeField] private AnimationCurve volume;
	[SerializeField] private AnimationCurve rectScale;
	private Vector3 landingPoint = new Vector3();
	private void Awake() 
	{
		audio = GetComponent<AudioSource>();
		points = GetComponent<LineRenderer>();
	}
	private void Update()
	{
		if (shoot.gun.gunSelected != 5)
			return ;
		if (reloadBow.ammoInMag == 1)
			canShoot = true;
		else
			canShoot = false;
		if (click.click && arrowCharge != true && release == false && canShoot)
		{
			audio.volume = 0.2f;
			arrowCharge = true;
			audio.clip = clips[0];
			audio.Play();
			CreateArrow();
		}
		if (arrowCharge && !click.holdClick)
		{
			arrowCharge = false;
			release = true;
		}
		if (release)
		{
			if (currentArrow != null)
			{
				currentArrow.released = true;
				currentArrow.landingPoint = new Vector2(click.mousePosition.x, landingPoint.y);
				currentArrow.chargeCount = chargeCount;
				currentArrow = null;
			}
			audio.volume = volume.Evaluate(chargeCount);
			audio.clip = clips[1];
			audio.Play();
			click.ChangeScale(rectScale.Evaluate(chargeCount));
			landingPoint = Vector3.zero;
			addition = 0.05f;
			chargeCount = 0;
			reloadBow.ammoInMag = 0;
			reloadBow.totalAmmo -= 1;
			totalAdditon = 0.0f;
			points.positionCount = 0;
			release = false;
		}
	}
	private void ChargeArrow()
	{
		if (currentArrow == null)
			return ;
		if (currentArrow.speed >= 1500.0f || release == true || arrowCharge == false)
		{
			release = true;
			arrowCharge = false;
			return ;
		}
		Vector2 og = OriginPos();
		points.positionCount = 0;
		points.positionCount = 50;
		currentArrow.transform.position = new Vector3(og.x, og.y, 1.0f);
		Vector2 direction = new Vector2(click.mousePosition.x, click.mousePosition.y) - og;
		currentArrow.target = new Vector2(click.mousePosition.x, click.mousePosition.y);
		int maxSteps = (int)(5.0f / 0.1f);
		float vel = currentArrow.speed / currentArrow.rb.mass * Time.fixedDeltaTime;
		for (int i = 0 ; i < maxSteps ; i++)
		{
			Vector2 calculatedPosition = og + (direction.normalized * vel * i * 0.015f);
			calculatedPosition.y += Physics2D.gravity.y / 2.0f * Mathf.Pow(i * 0.015f, 2);
			points.SetPosition(i, new Vector3(calculatedPosition.x, calculatedPosition.y, 10.0f));
			Vector3 point = points.GetPosition(i);
			landingPoint = (Vector3)points.GetPosition(i);
			if (point.x > click.mousePosition.x)
			{
				points.positionCount = i;
				break ;
			}
		}
		chargeCount++;
		currentArrow.speed += 400.0f * Time.deltaTime;
		currentArrow.rb.mass /= (1.0f + (1.0f * Time.deltaTime));
	}
	private void FixedUpdate()
	{
		if (shoot.gun.gunSelected != 5)
			return ;
		if (currentArrow == null || arrowCharge == false || release == true)
			return ;
		ChargeArrow();
		totalAdditon += addition;
		addition /= 1.045f;
	}
	private void CreateArrow()
	{
		GameObject arrow = Instantiate(arrowPrefab, OriginPos(), Quaternion.identity);
		currentArrow = arrow.GetComponent<Arrow>();
		if (UpBow.arrows)
			currentArrow.dropActive = true;
		else
			currentArrow.dropActive = false;
	}
	private Vector2 OriginPos()
	{
		Vector2 ret = new Vector2();
		ret.x = click.mousePosition.x - 7.5f - totalAdditon;
		ret.y = click.mousePosition.y;
		return (ret);
	}
}
