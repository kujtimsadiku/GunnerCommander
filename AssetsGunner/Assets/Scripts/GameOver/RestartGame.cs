using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
	public void Restart()
	{
		SceneManager.LoadScene(1);
	}
}
