using UnityEngine;

public class PauseMenu : MonoBehaviour, IDataPersistence
{
	[SerializeField] private GameObject[] deactivateObj;
	[SerializeField] private GameObject settings;
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private ResumeGame resumeGame;
	private bool paused = false;
	private int pressed = 0;
	private bool inPause = false;
	
	private void Awake()
	{
		Resolutions.resAtQuit = Resolutions.res;
		if (newGame.newgame && !Resolutions.beenHere)
		{
			Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, Screen.fullScreen);
			Resolutions.res = Screen.currentResolution;
		}
		else
			Screen.SetResolution(Resolutions.res.width, Resolutions.res.height, Screen.fullScreen);
		Resolutions.beenHere = true;
	}
	private void Update()
	{
		if (resumeGame.resumeGame)
		{
			paused = false;
			pressed = 0;
			resumeGame.resumeGame = false;
			Activation.Activate(deactivateObj);
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (pressed == 0)
			{
				paused = true;
				inPause = true;
				pressed = 1;
				pauseMenu.SetActive(true);
				Activation.Deactivate(deactivateObj);
			}
			else
			{
				paused = false;
				pressed = 0;
				pauseMenu.SetActive(false);
				if (settings.activeSelf)
					settings.SetActive(false);
				Activation.Activate(deactivateObj);
			}
		}
		if (!paused && inPause)
		{
			Time.timeScale = 1.0f;
			inPause = false;
		}
		else if (resumeGame.resumeGame)
			Time.timeScale = 1.0f;
		else if (paused)
			Time.timeScale = 0f;
	}
	public void LoadData(GameData data)
	{
		return ;
	}
	public void SaveData(ref GameData data)
	{
		data.effect = Remember.effect;
		data.effectVol = Volumes.effectVol;
		data.music = Remember.music;
		data.musicVol = Volumes.musicVol;
		data.resHeight = Resolutions.res.height;
		data.resWidth = Resolutions.res.width;
		data.fullscreen = FS.fullscreen;
	}
}