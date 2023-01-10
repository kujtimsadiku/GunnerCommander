using UnityEngine;

public static class BurstOn
{
	public static bool burst;
}

public class PistolUps : MonoBehaviour
{
	private int upCount = 0;
	private void Awake()
	{
		BurstOn.burst = false;
	}
	public void UpgradePistol()
	{
		if (upCount == 0)
		{
			GunUp.upIncoming = true;
			GunUp.gun = 0;
			GunUp.minAmount = 14;
			GunUp.maxAmount = 18;
		}
		else
			BurstOn.burst = true;
		upCount++;
	}
}
