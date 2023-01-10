using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

// [RequireComponent(typeof(Button))]
public class MainMenuButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
	[SerializeField] private GameObject settings;
	[SerializeField] private GameObject[] mouse;
	[SerializeField] private RectTransform text;
	[SerializeField] private Sprite[] sp;
	[SerializeField] private Image myImage;
	private AudioSource clip;
	private Button button;
	private bool notInside = false;
	private void Awake()
	{
		button = GetComponent<Button>();
		clip = GetComponent<AudioSource>();
	}
	private void Start() 
	{
		Time.timeScale = 1.0f;
	}
	public void OnPointerDown(PointerEventData ptrData)
	{
		if (!button.interactable)
			return ;
		else
		{
			text.position = new Vector3(text.position.x, text.position.y - 0.17f, text.position.z);
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
				text.position = new Vector3(text.position.x, text.position.y + 0.17f, text.position.z);
				myImage.sprite = sp[0];
				return ;
			}
			text.position = new Vector3(text.position.x, text.position.y + 0.17f, text.position.z);
			myImage.sprite = sp[0];
			GameObject pressed = ptrData.pointerPress;
			clip.Play();
			if (pressed.CompareTag("Play"))
			{
				Activation.Activate(mouse);
				Play.StartGame();
			}
			else if (pressed.CompareTag("Settings"))
				settings.SetActive(true);
			else if (pressed.CompareTag("Quit"))
				QuitGame.Quit();
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
