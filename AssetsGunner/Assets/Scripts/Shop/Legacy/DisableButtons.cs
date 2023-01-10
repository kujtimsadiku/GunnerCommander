using UnityEngine;

public class DisableButtons : MonoBehaviour
{
	[SerializeField] private GameObject[] buttons;
	private void OnDisable()
	{
		foreach(GameObject button in buttons)
		{
			button.SetActive(false);
		}
	}
}
