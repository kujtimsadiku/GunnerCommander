using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Button))]
public class GameOverMenu : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
	private Button button;
	private bool notInside = false;
	[SerializeField] private RectTransform text;
	[SerializeField] private Sprite[] sp;
	[SerializeField] private Image myImage;
	[SerializeField] private GameObject[] mouse;
	private AudioSource clip;
	private RestartGame restart;
	// [SerializeField] private MainMenu menu;
	private void Awake() {
		button = GetComponent<Button>();
		restart = GetComponent<RestartGame>();
		clip = GetComponent<AudioSource>();
	}
	public void OnPointerDown(PointerEventData ptrData)
	{
		if (!button.interactable)
			return ;
		else
		{
			text.position = new Vector3(text.position.x, text.position.y - 0.12f, text.position.z);
			myImage.sprite = sp[1];
		}
	}
	public void OnPointerUp(PointerEventData ptrData)
	{
		if (!button.interactable)
			return ;
		else
		{
			if (notInside)
			{
				text.position = new Vector3(text.position.x, text.position.y + 0.12f, text.position.z);
				myImage.sprite = sp[0];
				return ;
			}
			text.position = new Vector3(text.position.x, text.position.y + 0.12f, text.position.z);
			myImage.sprite = sp[0];
			clip.Play();
			GameObject pressed = ptrData.pointerPress;
			if (pressed.CompareTag("Main Menu"))
				Play.MainMenu();
			else if (pressed.CompareTag("Restart"))
				restart.Restart();
			else if (pressed.CompareTag("Quit"))
				QuitGame.Quit();
			else
				return ;
		}
	}
	public void OnPointerExit(PointerEventData ptrData)
	{
		notInside = true;
	}
	public void OnPointerEnter(PointerEventData ptrData)
	{
		notInside = false;
	}
}
