using UnityEngine;

public static class AkAdditionalAmmo
{
	public static bool additionalAmmo;
}
public class AKUps : MonoBehaviour
{
	private bool one = false;
	private bool two = false;
	private void Awake()
	{
		AkAdditionalAmmo.additionalAmmo = false;
	}
	public void Buy()
	{
		if (one && two)
			return ;
		if (!one)
		{
			GunUp.upIncoming = true;
			GunUp.gun = 1;
			GunUp.minAmount = 20;
			GunUp.maxAmount = 26;
			one = true;
		}
		else if (!two)
		{
			AkAdditionalAmmo.additionalAmmo = true;
			two = true;
		}
	}
}
