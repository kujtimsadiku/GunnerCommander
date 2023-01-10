using UnityEngine;

public class ScoutAnimation : MonoBehaviour
{
	private Animator anim;
	private TrooperMoement bools;
	private bool attacked = false;
	private bool idled = false;
	private void Awake()
	{
		anim = GetComponent<Animator>();
		bools = GetComponent<TrooperMoement>();
	}
	private void Update()
	{
		anim.SetBool("ScoutIdle", bools.idle);
		anim.SetBool("Attack", bools.shoot);
		if (bools.shoot == true && attacked == false)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + 0.176f, transform.position.z);
			attacked = true;
		}
		if (bools.shoot == false && attacked == true)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y - 0.176f, transform.position.z);
			attacked = false;
		}
		if (bools.idle == true && idled == false)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y - 0.025f, transform.position.z);
			idled = true;
		}
		if (bools.idle == false && idled == true)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + 0.025f, transform.position.z);
			idled = false;
		}
	}
}
