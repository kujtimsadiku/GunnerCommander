using UnityEngine;

public static class ShotGunAmmoUp
{
	public static bool up;
}
public class ShotgunUps : MonoBehaviour
{
	private bool one = false, two = false;
	private void Awake()
	{
		ShotGunAmmoUp.up = false;
	}
	public void Buy()
	{
		if (!one)
		{
			one = true;
			GunUp.upIncoming = true;
			GunUp.gun = 2;
			GunUp.minAmount = 22.0f;
			GunUp.maxAmount = 25.0f;
			ShotGunAmmoUp.up = true;
		}
		else if (!two)
		{
			two = true;
			ChargeOwned.yes = true;
		}
	}
}
