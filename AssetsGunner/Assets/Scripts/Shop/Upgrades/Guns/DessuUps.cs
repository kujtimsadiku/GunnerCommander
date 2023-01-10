using UnityEngine;

public class DessuUps : MonoBehaviour
{
	[SerializeField] private GameObject img;
	[SerializeField] private DesertEagle dessu;
	[SerializeField] private PickGun owned;
	private bool one = false, two = false, three = false;
	public void Buy()
	{
		if (!one)
		{
			one = true;
			owned.ownedGuns[4] = 1;
			Destroy(img);
		}
		else if (!two)
		{
			dessu.minDamage = 34.0f;
			dessu.maxDamage = 38.0f;
			two = true;
		}
		else if (!three)
		{
			dessu.fireRate = 0.175f;
			three = true;
		}
	}
}
