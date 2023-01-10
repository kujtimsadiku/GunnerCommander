using UnityEngine;
using TMPro;

public class UpgadeMoney : MonoBehaviour
{
	public int upMoney;
	[SerializeField] private TMP_Text moneyToScreen;
	private new AudioSource audio;
	private int currentUpMoney;
	private void Awake()
	{
		audio = GetComponent<AudioSource>();
	}
	private void Start()
	{
		currentUpMoney = upMoney;
	}
	private void Update()
	{
		if (PlayerMoney.bought)
		{
			upMoney = PlayerMoney.money;
			currentUpMoney = upMoney;
			PlayerMoney.bought = false;
		}
		else
			PlayerMoney.money = upMoney;
		moneyToScreen.text = upMoney.ToString();
		moneyToScreen.text += "$";
		if (currentUpMoney != upMoney)
		{
			if (DamageToEndLine.money)
			{
				audio.Play();
				DamageToEndLine.money = false;
			}
			currentUpMoney = upMoney;
		}
	}
}
