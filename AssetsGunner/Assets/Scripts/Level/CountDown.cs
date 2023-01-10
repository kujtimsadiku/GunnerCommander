using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
	[SerializeField] private TMP_Text countDown;
	[SerializeField] GameObject count;
	private LevelChange activate;
	private bool countStart = false;
	private int seconds;
	private int hundredths;
	private void Awake()
	{
		activate = GetComponent<LevelChange>();
	}
	private void Update()
	{
		if (activate.ending == true && countStart == false)
		{
			count.SetActive(true);
			seconds = 10;
			hundredths = 0;
			countStart = true;
			InvokeRepeating(nameof(CountHundredths), 0.0f, 0.01f);
		}
		else if (activate.ending == false)
		{
			count.SetActive(false);
			CancelInvoke();
			countStart = false;
		}
		if (hundredths < 0)
		{
			seconds--;
			hundredths = 99;
		}
		else if (hundredths <= 0 && seconds <= 0)
			count.SetActive(false);
		if (countStart)
		{
			if (seconds < 10)
			{
				countDown.text = "0";
				countDown.text += seconds.ToString();
			}
			else
				countDown.text = seconds.ToString();
			countDown.text += ":";
			if (hundredths < 10)
			{
				countDown.text += "0";
				countDown.text += hundredths.ToString();
			}
			else
				countDown.text += hundredths.ToString();
		}
	}
	private void CountHundredths()
	{
		hundredths -= 1;
	}
}
