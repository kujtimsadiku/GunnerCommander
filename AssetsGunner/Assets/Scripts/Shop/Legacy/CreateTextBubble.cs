using UnityEngine;

public class CreateTextBubble : MonoBehaviour
{
	private Vector2 mousePosition;
	private void Start()
	{
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector3(mousePosition.x + 2.15f, mousePosition.y - 1.615f, 1.0f);
		if (mousePosition.x > 4.5f)
			transform.position = new Vector3(mousePosition.x - 2.15f, transform.position.y, 1.0f);
		if (mousePosition.y < -1.8f)
			transform.position = new Vector3(transform.position.x, mousePosition.y + 1.615f, 1.0f);
	}
	private void Update()
	{
		TextBoxPosition();
	}
	public void TextBoxPosition()
	{
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector3(mousePosition.x + 2.15f, mousePosition.y - 1.615f, 1.0f);
		if (mousePosition.x > 4.5f)
			transform.position = new Vector3(mousePosition.x - 2.15f, transform.position.y, 1.0f);
		if (mousePosition.y < -1.8f)
			transform.position = new Vector3(transform.position.x, mousePosition.y + 1.615f, 1.0f);
	}
}
