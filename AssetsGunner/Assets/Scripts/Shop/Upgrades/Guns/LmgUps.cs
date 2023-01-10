using UnityEngine;

public static class DevilActive
{
	public static bool up;
}
public class LmgUps : MonoBehaviour
{
	[SerializeField] private LmgShoot lmg;
	[SerializeField] private GameObject img;
	[SerializeField] private PickGun owned;
	private bool one = false, two = false, three = false;
	private void Awake()
	{
		DevilActive.up = false;
	}
	public void Buy()
	{
		if (!one)
		{
			Destroy(img);
			owned.ownedGuns[6] = 1;
			one = true;
		}
		else if (!two)
		{
			lmg.minDamage = 45.0f;
			lmg.maxDamage = 55.0f;
			two = true;
		}
		else if (!three)
		{
			DevilActive.up = true;
			three = true;
		}
	}
}
