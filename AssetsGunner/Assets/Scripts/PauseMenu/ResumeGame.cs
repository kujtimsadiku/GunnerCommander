using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGame : MonoBehaviour
{
	[SerializeField] private GameObject resume;
	[HideInInspector] public bool resumeGame = false;
	public void Resume()
	{
		resumeGame = true;
		Time.timeScale = 1f;	
		resume.SetActive(false);
	}
}
