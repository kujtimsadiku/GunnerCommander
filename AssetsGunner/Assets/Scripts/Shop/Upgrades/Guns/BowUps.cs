using UnityEngine;

public static class UpBow
{
	public static bool arrows;
	public static bool armor;
}
public class BowUps : MonoBehaviour
{
	[SerializeField] private GameObject img;
	private bool one = false, two = false, three = false;
	[SerializeField] private PickGun owned;
	private void Awake()
	{
		UpBow.armor = false;
		UpBow.arrows = false;
	}
	public void Buy()
	{
		if (!one)
		{
			one = true;
			Destroy(img);
			owned.ownedGuns[5] = 1;
		}
		else if (!two)
		{
			two = true;
			UpBow.arrows = true;
		}
		else if (!three)
		{
			three = true;
			UpBow.armor = true;
		}
	}
}
