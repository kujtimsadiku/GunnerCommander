using UnityEngine;

public class HandleMoreShotgunAmmo : MonoBehaviour
{
	[SerializeField] private GameObject moreAmmo;
	[SerializeField] private ShotGunReload shotGun;
	private bool visited = false;
	private void Update()
	{
		if (!visited && ShotGunAmmoUp.up)
		{
			moreAmmo.SetActive(true);
			shotGun.InitMoreAmmo();
			visited = true;
		}
	}
}
