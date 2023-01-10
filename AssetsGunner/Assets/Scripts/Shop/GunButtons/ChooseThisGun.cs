using UnityEngine;
using UnityEngine.UI;

public class ChooseThisGun : MonoBehaviour
{
	[SerializeField] private PickGun owned;
	[SerializeField] private int gun;
	private Button button;
	private bool visited = false;
	private void Awake()
	{
		button = GetComponent<Button>();
	}
	private void Update()
	{
		if (visited)
			return ;
		if (owned.ownedGuns[gun] == 1)
		{
			button.interactable = true;
			visited = true;
		}
	}
	public void Clicked()
	{
		owned.gunSelected = gun;
	}
}
