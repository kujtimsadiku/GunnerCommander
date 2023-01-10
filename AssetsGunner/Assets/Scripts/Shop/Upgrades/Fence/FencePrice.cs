using UnityEngine;
using UnityEngine.UI;

public class FencePrice : MonoBehaviour
{
	[SerializeField] private InteractButton cost;
	private Button button;
	[SerializeField] private GameObject deact;
	private TextBubble text;
	private int exhaust = 0;
	private int[] costs = {500, 600, 700, 800, 1000};
	private string[] textBubbel = {"500 to 700", "700 to 900", "900 to 1100", "1100 to 1300", "1300 to 1500"};
	private void Awake()
	{
		text = GetComponent<TextBubble>();
	}
	private void Start()
	{
		button = cost.gameObject.GetComponent<Button>();
	}
	private void Update()
	{
		if (exhaust == 5)
		{
			deact.SetActive(false);
			button.interactable = false;
			return ;
		}
		cost.cost = costs[exhaust];
		text.text = "Fence Health Goes From ";
		text.text += textBubbel[exhaust];
	}
	public void Bought()
	{
		exhaust++;
	}
}
