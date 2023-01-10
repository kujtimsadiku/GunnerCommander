using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	[SerializeField] private Toggle fullscreen;
	private void Awake()
	{
		if (fullscreen == null)
			return ;
		fullscreen.isOn = FS.fullscreen;
		Screen.fullScreen = FS.fullscreen;
	}
	private void Update()
	{
		if (fullscreen == null)
			return ;
		Screen.fullScreen = fullscreen.isOn;
		FS.fullscreen = fullscreen.isOn;
	}
	public void ExitSettings()
	{
		gameObject.transform.parent.gameObject.SetActive(false);
	}
}
