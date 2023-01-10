using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteScreen : MonoBehaviour
{
	[SerializeField] private GameObject explosion;
	[SerializeField] private GameObject gameOver;
	[SerializeField] private GameObject fence;
	[SerializeField] private Sprite fenceDestroyed;
	private SpriteRenderer sp;
	private bool beenThere = false;
	private bool outOfThere = false;
	private float opacity = 0.0f;
	private void Awake() {
		Invoke(nameof(StopTheExplosions), 1.6f);
		sp = GetComponent<SpriteRenderer>();
		Invoke(nameof(CallGameOver), 1.0f);
		Invoke(nameof(FenceDes), 1.4f);
	}
	private void Update()
	{
		if (!beenThere)
		{
			ChangeOpacityPositive();
			if (opacity >= 1f)
				beenThere = true;
		}
		else if (!outOfThere)
		{
			ChangeOpacityNegative();
			if (opacity <= 0f)
				outOfThere = true;
		}
	}
	private void StopTheExplosions()
	{
		explosion.GetComponent<FenceExplosion>().ammExp = 0;
	}
	private void ChangeOpacityPositive()
	{
		sp.color = new Color(1f, 1f, 1f, opacity);
		opacity += 0.66f * Time.deltaTime;
	}
	private void ChangeOpacityNegative()
	{
		sp.color = new Color(1f, 1f, 1f, opacity);
		opacity -= 0.17f * Time.deltaTime;
	}
	private void CallGameOver()
	{
		gameOver.SetActive(true);
		gameOver.GetComponent<GameOverActivation>().GameOver();
	}
	private void FenceDes()
	{
		fence.GetComponent<SpriteRenderer>().sprite = fenceDestroyed;
	}
}
