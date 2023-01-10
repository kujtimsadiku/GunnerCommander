using UnityEngine;

public class RpgRocket : MonoBehaviour
{
	public Vector2 target;
	private Vector2 moving;
	[SerializeField] private GameObject explosionPrefab;
	public System.Action destroyed;
	private void Start()
	{
		CalculateMovingVector();
	}
	private void FixedUpdate()
	{
		transform.position = new Vector3(transform.position.x - moving.x, transform.position.y - moving.y, 1.0f);
		transform.localScale = transform.localScale / 1.02f;
		if (moving != Vector2.zero) 
		{
			float angle = Mathf.Atan2(moving.y, moving.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
		if (CheckIfInPosition())
		{
			Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			if (destroyed != null)
				destroyed.Invoke();
			Destroy(gameObject);
		}
	}
	private bool CheckIfInPosition()
	{
		Vector2 distance = new Vector2(transform.position.x, transform.position.y) - target;
		if (distance.magnitude < 0.1f)
			return (true);
		return (false);
	}
	private void CalculateMovingVector()
	{
		Vector2 distance = new Vector2(transform.position.x, transform.position.y) - target;
		moving = distance / 100.0f;
	}
}
