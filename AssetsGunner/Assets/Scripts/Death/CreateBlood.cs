using UnityEngine;

public class CreateBlood : MonoBehaviour
{
	[SerializeField] private GameObject blood;
	
	private void Start()
	{
		Instantiate(blood, transform.position, Quaternion.identity);
	}
}
