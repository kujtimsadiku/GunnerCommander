using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
	private void Start()
	{
		Invoke(nameof(DestroyThis), 0.3f);
	}
	private void DestroyThis()
	{
		Destroy(gameObject);
	}
}
