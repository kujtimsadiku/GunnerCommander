using UnityEngine;

public class EasterEgg : MonoBehaviour
{
	[SerializeField] private AudioClip play;
	private int count = 0;
	public void CountUp()
	{
		count++;
	}
	public void Play()
	{
		if (count == 55)
			AudioSource.PlayClipAtPoint(play, new Vector3(0.29f, 0.0f, -10.0f), 0.5f);
	}
}
