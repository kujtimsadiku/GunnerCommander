using UnityEngine;

public class RifleAnim : MonoBehaviour
{
	private TrooperMoement bools;
	private Animator anim;
	private void Awake()
	{
		anim = GetComponent<Animator>();
		bools = GetComponent<TrooperMoement>();
	}
	private void Update()
	{
		anim.SetBool("Idle", bools.idle);
		anim.SetBool("shooting", bools.shoot);
	}
}
