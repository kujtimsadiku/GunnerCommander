using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class Resolutions
{
	// what resolution was before the quit
	public static Resolution resAtQuit;
	public static Resolution res;
	public static bool beenHere = false;
}

public class ResolutionMainMenu : MonoBehaviour
{
	private Resolution[] res;
	[SerializeField] private TMP_Dropdown dropdownRes;
	private List<string> listString = new List<string>();

	private void Start()
	{
		dropdownRes.ClearOptions();
		res = Screen.resolutions;
		int currentResolution = 0;
		for (int i = 0; i < res.Length; i++)
		{
			listString.Add(res[i].width + "x" + res[i].height);
			if (newGame.newgame && !Resolutions.beenHere)
			{
				if (Screen.currentResolution.width == res[i].width && Screen.currentResolution.height == res[i].height)
				{
					currentResolution = i;
					newGame.newgame = false;
					Resolutions.beenHere = true;
				}
			}
			else if (Resolutions.res.width == res[i].width && Resolutions.res.height == res[i].height)
				currentResolution = i;
		}
		dropdownRes.AddOptions(listString);
		dropdownRes.value = currentResolution;
		dropdownRes.RefreshShownValue();
		FS.firstPlay = false;
	}
	public void SetScreen(int index)
	{
		Resolutions.res = res[index];
		Screen.SetResolution(res[index].width, res[index].height, Screen.fullScreen);
	}
}
