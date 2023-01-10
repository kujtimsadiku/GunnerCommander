using UnityEngine;
using TMPro;

public class LmgAmmo : MonoBehaviour
{
	public int totalAmmo;
	[SerializeField] private TMP_Text ammo;
	private void Update()
	{
		ammo.text = totalAmmo.ToString();
	}
}
