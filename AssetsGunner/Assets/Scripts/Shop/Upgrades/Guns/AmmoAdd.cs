using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoAdd : MonoBehaviour
{
	[SerializeField] private int selectedGun;
	[SerializeField] private PickGun owned;
	private InteractButton button;
	private bool active = false;
	private void Awake()
	{
		button = GetComponent<InteractButton>();
	}
	private void Update()
	{
		if (active)
			return ;
		if (owned.ownedGuns[selectedGun] == 1)
		{
			button.unlocked = true;
			active = true;
		}
	}
	public void Buy()
	{
		AmmoDrop.coming = true;
		AmmoDrop.selected = selectedGun;
	}
}
