using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceExplosion : MonoBehaviour
{
	[SerializeField] private GameObject[] prefabExp;
	[SerializeField] private GameObject whiteScreen;
	private int originalOrder;
	private bool quit = false;
	public int ammExp;
	private void Awake()
	{
		if (!quit)
			InvokeRepeating(nameof(InstExplosion), 0f, 0.2f);
	}
	private void OnApplicationQuit()
	{
		quit = true;
	}
	private void Start() {
		Invoke("StartWhiteScreen", 1.4f);
	}
	private void StartWhiteScreen()
	{
		whiteScreen.SetActive(true);
	}
	private void InstExplosion()
	{
		if (quit)
			return ;
		GameObject explosion = Instantiate(prefabExp[Random.Range(0, prefabExp.Length)], new Vector3(Random.Range(-6f, -5.4f),
								Random.Range(-3.5f, 3.5f), 1.0f), Quaternion.identity);
		originalOrder = explosion.GetComponent<SpriteRenderer>().sortingOrder;
		if (originalOrder != 1)
			explosion.GetComponent<SpriteRenderer>().sortingOrder = 1;
		float x_y = Random.Range(explosion.transform.localScale.x, explosion.transform.localScale.x + 1.5f);
		Vector3 xy = new Vector3(x_y, x_y, 1.0f);
		explosion.transform.localScale = xy;
		if (ammExp-- == 0)
			CancelInvoke();
	}
}
