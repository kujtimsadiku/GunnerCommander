using UnityEngine;

public static class MoneyStack
{
	public const int rifleMoney = 20;
	public const int scoutMoney = 10;
	public const int bazookaMoney = 40;
	public const int riotMoney = 30;
	public const int tankMoney = 75;
	public const int miniGunMoney = 50;
	public const int mechMoney = 120;
	public const int bugMoney = 20;
	public static readonly int[] moneyAmounts = {rifleMoney, scoutMoney, bazookaMoney, riotMoney, tankMoney, miniGunMoney, mechMoney, bugMoney};
}
public class MoneyMoving : MonoBehaviour
{
	private float move;
	[SerializeField] private AudioClip sound;
	[HideInInspector] public int moneyAmount;
	private UpgadeMoney upMoney;
	private bool ByTime = false;
	private void Awake()
	{
		upMoney = GameObject.FindGameObjectWithTag("Deployer").GetComponent<UpgadeMoney>();
	}
	private void Start()
	{
		gameObject.SetActive(false);
		Invoke(nameof(Activate), 0.4f);
		Invoke(nameof(Fade), 3.5f);
	}
	private void Activate()
	{
		gameObject.SetActive(true);
	}
	private void FixedUpdate()
	{
		float newPos = 0.0f;
		newPos = 0.005f * Mathf.Sin(move * 5.0f);
		move += 0.01f;
		transform.position = new Vector3(transform.position.x, transform.position.y + newPos, 1.0f);
	}
	private void Fade()
	{
		gameObject.GetComponent<Animator>().SetTrigger("Fade");
		Invoke(nameof(DestroyThis), 2.0f);
	}
	private void OnApplicationQuit()
	{
		ByTime = true;
	}
	private void OnDestroy()
	{
		if (!ByTime)
		{
			upMoney.upMoney += moneyAmount;
			AudioSource.PlayClipAtPoint(sound, new Vector3(0.29f, 0.0f, -10.0f), 0.5f * Volumes.effectVol);
		}
	}
	private void DestroyThis()
	{
		ByTime = true;
		Destroy(gameObject);
	}
}
