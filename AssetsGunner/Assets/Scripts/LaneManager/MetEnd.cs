using UnityEngine;

public class MetEnd : MonoBehaviour
{
	[SerializeField] private Camera shake;
	private new AudioSource audio;
	private Vector3 cameraPos;
	private int startShake = (-1);
	private void Awake()
	{
		audio = GetComponent<AudioSource>();
		DamageToEndLine.metEnd = false;
	}
	private void Start()
	{
		cameraPos = shake.transform.position;
	}
	private void FixedUpdate()
	{
		if (DamageToEndLine.metEnd)
		{
			audio.Play();
			DamageToEndLine.metEnd = false;
			shake.transform.position = cameraPos;
			startShake = 0;
		}
		if (startShake != (-1))
			ShakeCamera();
	}
	private void ShakeCamera()
	{
		if (startShake >= 5)
		{
			shake.transform.position = cameraPos;
			startShake = (-1);
			return ;
		}
		if (startShake < 1)
			shake.transform.position = new Vector3(shake.transform.position.x - 0.15f, shake.transform.position.y + 0.05f, shake.transform.position.z);
		else if (startShake < 2)
			shake.transform.position = new Vector3(shake.transform.position.x + 0.15f, shake.transform.position.y - 0.05f, shake.transform.position.z);
		else if (startShake < 3)
			shake.transform.position = new Vector3(shake.transform.position.x + 0.1f, shake.transform.position.y + 0.15f, shake.transform.position.z);
		else
			shake.transform.position = new Vector3(shake.transform.position.x - 0.2f, shake.transform.position.y - 0.15f, shake.transform.position.z);
		startShake++;
	}
}
