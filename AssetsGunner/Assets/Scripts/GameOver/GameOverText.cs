using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverText : MonoBehaviour
{
	[SerializeField] private GameObject text;
	private Animator anim;
	private void Awake() {
		anim = GetComponent<Animator>();
	}
	private void ExitAnimation() {
		text.SetActive(true);
		gameObject.SetActive(false);
	}
}
