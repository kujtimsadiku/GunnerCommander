using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ResForSettings : MonoBehaviour
{
	private Resolution[] res;
	[SerializeField] private TMP_Dropdown dropdownRes;
	private List<string> listStrings = new List<string>();
	private void Start()
	{
		dropdownRes.ClearOptions();
		res = Screen.resolutions;
		int currentResolution = 0;
		for (int i = 0; i < res.Length; i++)
		{
			listStrings.Add(res[i].width + "x" + res[i].height);
			if (res[i].width == Resolutions.res.width && res[i].height == Resolutions.res.height)
				currentResolution = i;
		}
		dropdownRes.AddOptions(listStrings);
		dropdownRes.value = currentResolution;
		dropdownRes.RefreshShownValue();
	}
	public void SetScreen(int index)
	{
		Resolutions.res = res[index];
		Resolutions.resAtQuit = Resolutions.res;
		Screen.SetResolution(res[index].width, res[index].height, Screen.fullScreen);
	}
}
