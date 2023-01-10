using UnityEngine;

public static class RpgDrops
{
	public static bool shopDrop;
	public static bool dropDrop;
}
public class RpgUps : MonoBehaviour
{
	[SerializeField] private GameObject img;
	[SerializeField] private PickGun owned;
	private bool one = false, two = false, three = false;
	private void Awake()
	{
		RpgDrops.shopDrop = false;
		RpgDrops.dropDrop = false;
	}
	public void Buy()
	{
		if (!one)
		{
			one = true;
			owned.ownedGuns[3] = 1;
			Destroy(img);
		}
		else if (!two)
		{
			two = true;
			RpgDrops.shopDrop = true;
		}
		else if (!three)
		{
			three = true;
			RpgDrops.dropDrop = true;
		}
	}
}
