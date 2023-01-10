using UnityEngine;

public class MiniGunAnim : MonoBehaviour
{
	private TrooperMoement bools;
	private Animator anim;
	[SerializeField] private GameObject[] shotPrefab;
	private void Awake()
	{
		anim = GetComponent<Animator>();
		bools = GetComponent<TrooperMoement>();
	}
	private void Update()
	{
		if (bools.shoot)
		{
			anim.SetBool("Shoot", true);
			return ;
		}
		else if (bools.idle)
		{
			anim.SetBool("Idle", true);
			return ;
		}
		anim.SetBool("Shoot", false);
		anim.SetBool("Idle", false);
	}
	private void MiniGunShotEffect()
	{
		Vector3 positionAdd = GetPos();
		GameObject shot = Instantiate(shotPrefab[Random.Range(0, 2)], transform.position + positionAdd, Quaternion.identity);
		if (gameObject.layer == LayerMask.NameToLayer("Allied"))
			shot.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
	}
	private Vector3 GetPos()
	{
		if (gameObject.layer ==  LayerMask.NameToLayer("Allied"))
			return (new Vector3(0.6f, -0.1f, 0.0f));
		return (new Vector3(-0.6f, -0.1f, 0.0f));
	}
}
