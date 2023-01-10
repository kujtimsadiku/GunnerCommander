using UnityEngine;
public class AmmoBox : MonoBehaviour
{
	private float move;
	private bool ByTime = false;
	[SerializeField] private AudioClip sound;
	private bool visited = false;
	[SerializeField] private Sprite[] sprites;
	private SpriteRenderer rend;
	private int zeroOne = 0;
	private void Awake()
	{
		rend = GetComponent<SpriteRenderer>();
	}
	private void Start()
	{
		gameObject.SetActive(false);
		Invoke(nameof(Activate), 0.4f);
		InvokeRepeating(nameof(Fade), 3.5f, 0.2f);
	}
	private void FixedUpdate()
	{
		float newPos = 0.0f;
		newPos = 0.005f * Mathf.Sin(move * 5.0f);
		move += 0.01f;
		transform.position = new Vector3(transform.position.x, transform.position.y + newPos, 1.0f);
	}
	private void Activate()
	{
		gameObject.SetActive(true);
	}
	private void Fade()
	{
		rend.sprite = sprites[zeroOne];
		if (!visited)
			Invoke(nameof(DestroyThis), 2.0f);
		visited = false;
		if (zeroOne == 0)
			zeroOne = 1;
		else
			zeroOne = 0;
	}
	private void OnApplicationQuit()
	{
		ByTime = true;
	}
	private void OnDestroy()
	{
		if (!ByTime)
		{
			AddAmmo();
			AudioSource.PlayClipAtPoint(sound, new Vector3(0.29f, 0.0f, -10.0f), 0.5f * Volumes.effectVol);
		}
		CancelInvoke();
	}
	private void AddAmmo()
	{
		GameObject.FindGameObjectWithTag("Guns").GetComponent<PickGun>().AmmoComing = true;
	}
	private void DestroyThis()
	{
		ByTime = true;
		Destroy(gameObject);
	}
}
