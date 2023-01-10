using TMPro;
using UnityEngine;

public class TextBubble : MonoBehaviour
{
	[SerializeField] private TMP_Text info;
	public string text;
	private void Update()
	{
		info.text = text;
	}
}
