using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringShop : MonoBehaviour
{
	[HideInInspector] public bool shopUp = false;
	[SerializeField] private GameObject backGround;
	[SerializeField] private GameObject textBoxes;
	[SerializeField] private GameObject info;
	private bool visisted = false;
	private void Update()
	{
		if (shopUp)
		{
			visisted = false;
			backGround.SetActive(true);
			textBoxes.SetActive(true);
			Time.timeScale = 0.3f;
		}
		else if (!visisted)
		{
			visisted = true;
			backGround.SetActive(false);
			textBoxes.SetActive(false);
			info.SetActive(false);
			Time.timeScale = 1.0f;
		}
		if (Input.GetKey(KeyCode.Tab))
			shopUp = true;
		else
			shopUp = false;
	}
}
