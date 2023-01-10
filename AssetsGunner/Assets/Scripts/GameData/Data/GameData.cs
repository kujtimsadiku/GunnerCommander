using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
	// TODO MAYBE - INSTEAD OF RESOLUATION TAKE A STRING WITH | WIDTH + "X" + HEIGHT |
	public float resWidth;
	public float resHeight;
	public bool fullscreen;
	public float music;
	public float effect;
	public float musicVol;
	public float effectVol;
	public float effectSettings;
	public float musicSettings;
	public GameData()
	{
		resHeight = 0f;
		resWidth = 0f;
		fullscreen = true;
		music = 0f;
		effect = 0f;
		musicVol = 1f;
		effectVol = 1f;
		musicSettings = 1f;
		effectSettings = 1f;
	}
}
