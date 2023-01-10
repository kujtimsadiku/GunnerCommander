using UnityEngine;

public class TankShootingEffect : MonoBehaviour
{
	public GameObject shotEffectPrefab;
	private Animator anim;
	private TrooperMoement bools;
	private void Awake()
	{
		anim = GetComponent<Animator>();
		bools = GetComponent<TrooperMoement>();
	}
	private void Update()
	{
		anim.SetBool("Shooting", bools.shoot);
	}
	public void ActivateAnimation()
	{
		GameObject effect = Instantiate(shotEffectPrefab, transform.parent.position, Quaternion.identity);
		effect.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
		if (transform.localScale.x < 0.0f)
			effect.transform.localScale = new Vector3(-0.5f, 0.5f, 1.0f);
		effect.transform.localPosition = GetShotPosition();
	}
	private Vector3 GetShotPosition()
	{
		Vector3 shotPos;
		if (transform.localPosition.x > 0.0f)
			shotPos = new Vector3(transform.position.x + 0.47f, transform.position.y, 1f);
		else
			shotPos = new Vector3(transform.position.x - 0.47f, transform.position.y, 1f);
		if (transform.localPosition.y > 0.0f)
			shotPos.y -= 0.13f;
		else
			shotPos.y += 0.13f;
		return  (shotPos);
	}
}