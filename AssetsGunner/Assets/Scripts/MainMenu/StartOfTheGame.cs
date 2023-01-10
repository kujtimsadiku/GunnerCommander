using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public static class Play
{
	public static void MainMenu()
	{
		SceneManager.LoadScene(0);
	}
	public static void StartGame()
	{
		SceneManager.LoadScene(1);
	}
}
public static class Volumes
{
	public static float musicVol;
	public static float effectVol;
}
