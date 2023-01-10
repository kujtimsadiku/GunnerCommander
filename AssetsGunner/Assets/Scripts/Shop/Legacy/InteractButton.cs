using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractButton : MonoBehaviour
{
	private Button active;
	public bool unlocked;
	public int cost;
	public bool showPriceOnLocked = false;
	[SerializeField] public bool reUseable = false;
	[SerializeField] private InteractButton[] next;
	private bool bought = false;
	[HideInInspector] public bool maxed = false;
	[SerializeField] private bool disableObject = false;
	[SerializeField] private TMP_Text price;
	[SerializeField] private bool max;
	public bool showPrice = true;
	private void Awake()
	{
		active = GetComponent<Button>();
	}
	private void Update()
	{
		if ((bought || maxed) && !reUseable)
		{
			if (next != null)
			{
				foreach (InteractButton newNext in next)
					newNext.unlocked = true;
			}
			price.color = new Color(0.0f, 0.0f, 0.0f);
			if (max)
				price.text = "MAX";
			else
				price.text = "UNLOCKED";
			active.interactable = false;
			if (disableObject)
			{
				gameObject.SetActive(false);
				price.gameObject.SetActive(false);
			}
			return ;
		}
		else if (bought && reUseable)
			bought = false;
		if (showPrice)
		{
			price.text = cost.ToString();
			price.text += "$";
		}
		if (cost > PlayerMoney.money || !unlocked)
			active.interactable = false;
		else
			active.interactable = true;
		if (unlocked)
		{
			price.enabled = true;
			if (cost > PlayerMoney.money)
				price.color = new Color(0.482f, 0.01f, 0.0f); //Red
			else
				price.color = new Color(0.0f, 0.0f, 0.0f); //Green
		}
		else if (!showPriceOnLocked)
			price.enabled = false;
	}
	public void Bought()
	{
		bought = true;
		PlayerMoney.bought = true;
		PlayerMoney.money -= cost;
	}
}
