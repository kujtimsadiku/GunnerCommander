using UnityEngine;

public class MusicChange : MonoBehaviour
{
	[SerializeField] private int musicChangeLevel;
	private AudioSource music;
	[SerializeField] private AudioClip secondSong;
	private bool change = false;
	private bool startCange = false;
	//0.36
	private void Awake()
	{
		music = GetComponent<AudioSource>();
	}
	private void Update()
	{
		if (change)
			return ;
		if (LeveleActive.currentLevel == musicChangeLevel)
			startCange = true;
	}
	private void FixedUpdate()
	{
		if (change)
			return ;
		if (startCange)
		{
			music.volume -= 0.003f;
			if (music.volume <= 0.0f)
			{
				music.clip = secondSong;
				change = true;
				music.volume = 0.36f;
				music.Play();
			}
		}
	}
}
