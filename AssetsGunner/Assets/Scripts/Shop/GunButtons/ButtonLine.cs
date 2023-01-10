using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonLine : MonoBehaviour
{
	private InteractButton button;
	public int[] costs;
	[SerializeField] private GameObject[] stars;
	[SerializeField] private GameObject[] texts;
	[SerializeField] private GameObject lockText;
	private int timesBought = 0;
	[SerializeField] private bool changeTextToUpgrade = false;
	[SerializeField] private TMP_Text text;
	private Color upStar = new Color(1.0f, 0.729f, 0.0f);
	private void Awake()
	{
		button = GetComponent<InteractButton>();
		button.cost = costs[0];
	}
	public void BoughtItem()
	{
		ActivateStar();
		button.Bought();
		if (timesBought == 0 && changeTextToUpgrade)
			text.text = "upgrade";
		timesBought++;
		if (timesBought < costs.Length)
			button.cost = costs[timesBought];
		else
		{
			button.reUseable = false;
			button.maxed = true;
		}
	}
	private void ActivateStar()
	{
		stars[timesBought].transform.parent.gameObject.GetComponent<Image>().color = upStar;
		stars[timesBought].GetComponent<ParticleEffect>().ActivateSystem();
	}
	public void ShowTextBubble()
	{
		if (button.showPrice == false)
			lockText.SetActive(true);
		else if (timesBought < texts.Length)
			texts[timesBought].SetActive(true);
	}
	public void DeactivateTextBubble()
	{
		if (button.showPrice == false)
		{
			lockText.SetActive(false);
			return ;
		}
		int i = 0;
		while (i < texts.Length)
		{
			texts[i].SetActive(false);
			i++;
		}
	}
	public void ChangeText()
	{
		text.text = "upgrade";
	}
}
