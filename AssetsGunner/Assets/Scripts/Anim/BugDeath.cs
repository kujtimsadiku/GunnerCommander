using UnityEngine;

public class BugDeath : MonoBehaviour
{
	private bool grounded = true;
	private void Update()
	{
		if (grounded)
			transform.position = new Vector3(transform.position.x, transform.position.y - 0.15f * Time.deltaTime, 1.0f);
	}
	private void Death()
	{
		Destroy(transform.parent.gameObject);
	}
	private void SetStand()
	{
		grounded = false;
	}
}
