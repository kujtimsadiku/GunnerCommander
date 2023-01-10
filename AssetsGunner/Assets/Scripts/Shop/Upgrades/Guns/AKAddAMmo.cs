using UnityEngine;

public class AKAddAMmo : MonoBehaviour
{
	[SerializeField] private GameObject additionalAmmo;
	[SerializeField] private GameObject textPos;
	[SerializeField] private ReloadAk ak;
	private bool visited = false;
	private void Update()
	{
		if (!visited && AkAdditionalAmmo.additionalAmmo)
		{
			additionalAmmo.SetActive(true);
			Vector3 pos = textPos.transform.position;
			ak.InitMoreAmmo();
			textPos.transform.position = new Vector3(pos.x, pos.y + 0.25f, pos.z);
			visited = true;
		}
	}
}
