using UnityEngine;

public class SubManDead : MonoBehaviour
{
	private void Dead()
	{
		Destroy(transform.parent.gameObject);
	}
}
