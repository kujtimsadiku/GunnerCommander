using UnityEngine;
using UnityEngine.UI;

public class HealActive : MonoBehaviour
{
	private InteractButton cost;
	private Button button;
	private void Awake()
	{
		cost = GetComponent<InteractButton>();
		button = GetComponent<Button>();
	}
	private void Update()
	{
		if (cost.cost == 0)
			button.interactable = false;
		else
			button.interactable = true;
	}
}
