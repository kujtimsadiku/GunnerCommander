using UnityEngine.UI;
using UnityEngine;

public static class Remember
{
	public static float effect;
	public static float music;
}
public static class Loaded
{
	public static bool loaded;
}
public static class FS
{
	public static bool fullscreen;
	public static bool firstPlay;
}

public class VolumeRemember : MonoBehaviour, IDataPersistence
{
	[SerializeField] private Slider musicSlider;
	[SerializeField] private Slider effectSlider;

	public void LoadData(GameData data)
	{
		if (!Loaded.loaded)
		{
			if ((int)data.effect != 0)
				effectSlider.value = data.effect;
			Remember.effect = data.effect;
			Volumes.effectVol = data.effectVol;
			if ((int)data.music != 0)
				musicSlider.value = data.music;
			Remember.music = data.music;
			Volumes.musicVol = data.musicVol;
			FS.fullscreen = data.fullscreen;
			Resolutions.res.width = (int)data.resWidth;
			Resolutions.res.height = (int)data.resHeight;
			Loaded.loaded = true;
		}
	}
	public void SaveData(ref GameData data)
	{
		data.effect = Remember.effect;
		data.effectVol = Volumes.effectVol;
		data.music = Remember.music;
		data.musicVol = Volumes.musicVol;
		data.fullscreen = FS.fullscreen;
		data.resWidth = Resolutions.res.width;
		data.resHeight = Resolutions.res.height;
	}
}