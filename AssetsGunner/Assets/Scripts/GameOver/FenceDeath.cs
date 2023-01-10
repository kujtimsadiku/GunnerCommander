using UnityEngine;

public class FenceDeath : MonoBehaviour
{
	[SerializeField] private GameObject fenceExp;
	[SerializeField] private Health health;
	private void Update()
	{
		if (health.health <= 0.0f)
		{
			GameState.gameOver = true;
			fenceExp.SetActive(true);
			return ;
		}
	}
}
