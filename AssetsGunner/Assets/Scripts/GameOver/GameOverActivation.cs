using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
public static class GameState
{
	public static bool gameOver;
}
public class GameOverActivation : MonoBehaviour
{
	[SerializeField] private GameObject buttons;
	[SerializeField] private GameObject textAnimation;
	[SerializeField] private GameObject[] deactivate;
	private void Awake()
	{
		GameState.gameOver = false;
	}
	public void GameOver()
	{
		Cursor.visible = false;
		Activation.Deactivate(deactivate);
		textAnimation.SetActive(true);
		Invoke(nameof(MakeButtonVisible), 4f);
		return ;
	}
	private void MakeButtonVisible()
	{
		buttons.SetActive(true);
	}
}
