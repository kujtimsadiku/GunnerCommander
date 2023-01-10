using UnityEngine;

public class LaneMoving : MonoBehaviour
{
	[HideInInspector] public int currentLane = 0;
	private float laneAddition = 0.75f;
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.S))
		{
			if (currentLane == 6)
			{
				currentLane = 0;
				transform.position = new Vector3(transform.position.x, transform.position.y + (laneAddition * 6.0f), transform.position.z);
			}
			else
			{
				currentLane++;
				transform.position = new Vector3(transform.position.x, transform.position.y - laneAddition, transform.position.z);
			}
		}
		if (Input.GetKeyDown(KeyCode.W))
		{
			if (currentLane == 0)
			{
				currentLane = 6;
				transform.position = new Vector3(transform.position.x, transform.position.y - (laneAddition * 6.0f), transform.position.z);
			}
			else
			{
				currentLane--;
				transform.position = new Vector3(transform.position.x, transform.position.y + laneAddition, transform.position.z);
			}
		}
	}
}
