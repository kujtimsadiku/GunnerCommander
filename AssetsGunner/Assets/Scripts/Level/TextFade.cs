using UnityEngine;
using TMPro;

public class TextFade : MonoBehaviour
{
	[SerializeField] private TMP_Text text;
	private Color fade;
	private int count;
	public int howManyFades;
	[SerializeField] private float fadeSpeed;
	private void Start()
	{
		fade = text.color;
		count = howManyFades;
	}
	private void Update()
	{
		if (count > 0 && fade.a > 0.0f)
			fade.a -= Time.deltaTime * fadeSpeed;
		if (fade.a <= 0.0f)
		{
			count--;
			if (count > 0)
				fade.a = 1.0f;
		}
		text.color = fade;
	}
	private void OnEnable()
	{
		count = howManyFades;
		fade.a = 1.0f;
	}
}
