using UnityEngine;

public class Scroll : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetAxis("Wheel") < 0f) // forward
		{
			if (transform.position.y < 11.3f)
				transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, 1.0f);
			if (transform.position.y > 11.3f)
				transform.position = new Vector3(transform.position.x, 11.3f, 1.0f);
		}
		else if (Input.GetAxis("Wheel") > 0f) // backwards
		{
			if (transform.position.y > 0.0f)
				transform.position = new Vector3(transform.position.x, transform.position.y - 1.0f, 1.0f);
			if (transform.position.y < 0.0f)
				transform.position = new Vector3(transform.position.x, 0.0f, 1.0f);
		}
	}
}
