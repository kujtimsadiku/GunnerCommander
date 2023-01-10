using UnityEngine;

public class MechAnimation : MonoBehaviour
{
	private Animator anim;
	private TrooperMoement bools;
	private void Awake()
	{
		bools = GetComponent<TrooperMoement>();
		anim = GetComponent<Animator>();
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
}
