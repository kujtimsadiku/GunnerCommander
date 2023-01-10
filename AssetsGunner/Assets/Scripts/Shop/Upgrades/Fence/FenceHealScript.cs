using UnityEngine;
using UnityEngine.UI;

public class FenceHealScript : MonoBehaviour
{
	[SerializeField] private InteractButton cost;
	[SerializeField] private Health fenceHealth;
	[SerializeField] private FenceHealth totalHealth;
	[SerializeField] private GameObject buyText;
	private Button button;
	private int increase = 0;
	private int increaseIncrease = 1;
	private int visitCount = 0;
	private int amountHealed = 0;
	private int tics = 0;
	private void Start()
	{
		button = cost.gameObject.GetComponent<Button>();
	}
	private void Update()
	{
		if ((cost.cost = GetCost()) == 0)
			buyText.SetActive(false);
		else
			buyText.SetActive(true);
	}
	private int GetCost()
	{
		int healthToHeal = (int)(fenceHealth.maxHealth - fenceHealth.health);
		if (healthToHeal == 0 || PlayerMoney.money == 0)
		{
			button.interactable = false;
			tics = 0;
			return (0);
		}
		int costCount = (healthToHeal / 10) + 1;
		int costOfFullHeal = (5 + increase) * costCount;
		if (costOfFullHeal > PlayerMoney.money)
		{
			if (PlayerMoney.money < (5 + increase))
			{
				tics = 0;
				return (0);
			}
			int buyNumber = PlayerMoney.money / (5 + increase);
			tics = buyNumber;
			return (buyNumber * (5 + increase));
		}
		tics = (-1);
		return (costOfFullHeal);
	}
	public void Heal()
	{
		if (tics == 0)
			return ;
		if (tics == (-1))
		{
			amountHealed += (int)(fenceHealth.maxHealth - fenceHealth.health);
			fenceHealth.health = fenceHealth.maxHealth;
		}
		else
		{
			amountHealed += 10 * tics;
			fenceHealth.health += 10 * tics;
			if (fenceHealth.health > fenceHealth.maxHealth)
				fenceHealth.health = fenceHealth.maxHealth;
		}
	}
	public void IncreaseCost()
	{
		if (amountHealed > 380)
		{
			amountHealed = 0;
			increase += increaseIncrease;
			visitCount++;
			if (visitCount % 3 == 0)
				increaseIncrease++;
		}
	}
	public void IncreaseHealth()
	{
		totalHealth.totalHealth += 200;
		fenceHealth.maxHealth += 200;
		fenceHealth.health += 200;
	}
}
