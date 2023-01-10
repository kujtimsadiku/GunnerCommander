using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Activation
{
	public static void Activate(GameObject[] activate)
	{
		for (int i = 0; i < activate.Length; i++)
		{
			if (activate == null)
				return ;
			if (activate[i].activeSelf)
				continue ;
			else
				activate[i].SetActive(true);
		}
	}
	public static void Deactivate(GameObject[] deactivate) 
	{
		for (int i = 0; i < deactivate.Length; i++)
		{
			if (deactivate == null)
				return ;
			if (!deactivate[i].activeSelf)
				continue ;
			else
				deactivate[i].SetActive(false);
		}
	}
}
