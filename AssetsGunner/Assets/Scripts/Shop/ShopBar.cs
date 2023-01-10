using UnityEngine;

public class ShopBar : MonoBehaviour
{
	[SerializeField] private GameObject progress;
	private bool hovering = false;
	private void Update()
	{
		if (!hovering)
			progress.transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
	}
	public void ChangeTransform(float scale)
	{
		transform.localScale = new Vector3(scale, 1.0f, 1.0f);
	}
	public void QuitHover()
	{
		hovering = false;
	}
	public void Hover(float scale)
	{
		progress.transform.localScale = new Vector3(scale, 1.0f, 1.0f);
		hovering = true;
	}
	private void OnDisable()
	{
		hovering = false;
	}
}
