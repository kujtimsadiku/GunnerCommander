using UnityEngine;

public class MousePosition : MonoBehaviour
{
	[HideInInspector] public Vector3 mousePosition = new Vector3(1, 1, 1);
	[SerializeField] private MouseShot shoot;
	[HideInInspector] public bool click;
	[HideInInspector] public bool holdClick;
	[HideInInspector] public bool rightClick;
	private void Awake()
	{
		Cursor.visible = false;
	}
	private void Update()
	{
		click = false;
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = 1.0f;
		transform.position = mousePosition;
		MouseClick();
	}
	private void FixedUpdate()
	{
		if (transform.localScale.x > 0.76f)
		{
			Vector3 newScale = transform.localScale;
			newScale.x -= 0.05f;
			newScale.y -= 0.05f;
			transform.localScale = newScale;
		}
	}
	public void ChangeScale(float scale)
	{
		transform.localScale = new Vector3(scale, scale, 1.0f);
	}
	private void MouseClick()
	{
		if (Input.GetKeyDown(KeyCode.Mouse1))
			rightClick = true;
		else
			rightClick = false;
		if (Input.GetKeyDown(KeyCode.Mouse0))
			click = true;
		if (Input.GetKey(KeyCode.Mouse0))
			holdClick = true;
		else
			holdClick = false;
	}
}
