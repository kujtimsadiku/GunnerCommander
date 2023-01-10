using UnityEngine;

public class SubManAnimation : MonoBehaviour
{
	private Animator anim;
	private TrooperMoement bools;

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
}
