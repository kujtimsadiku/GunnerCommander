using UnityEngine;
using TMPro;

public class BowAmmoBuy : MonoBehaviour
{
	private InteractButton button;
	[SerializeField] private PickGun owned;
	[SerializeField] private TMP_Text cost;
	private bool visited = false;
	private void Awake()
	{
		button = GetComponent<InteractButton>();
	}
	private void Update()
	{
		if (owned.ownedGuns[5] == 1 && !visited)
		{
			visited = true;
			cost.enabled = true;
			button.showPriceOnLocked = true;
		}
		if (owned.ownedGuns[5] == 1 && owned.reloadBow.totalAmmo == 24)
			button.unlocked = false;
		else if (owned.ownedGuns[5] == 1)
			button.unlocked = true;
	}
}
