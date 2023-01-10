using System.Collections.Generic;
using UnityEngine;
public static class UnitStats
{
	public const float rifleStrength = 4.0f;
	public const float rifleDefence = 3.0f;
	public const float scoutStrength = 1.0f;
	public const float scoutDefence = 1.0f;
	public const float bazookaStrength = 10.0f;
	public const float bazookaDefence = 1.0f;
	public const float riotStrength = 0.0f;
	public const float riotDefence = 12.5f;
	public const float tankStrength = 20.0f;
	public const float tankDefence = 15.0f;
	public const float miniGunStrength = 11.0f;
	public const float miniGunDefence = 5.0f;
	public const float mechStrength = 40.0f;
	public const float mechDefence = 18.0f;
	public const float subStrength = 5.0f;
	public const float subDefence = 3.0f;
	public const float flyStrength = 1.1f;
	public const float flyDefence = 1.0f;
}
public class LaneUnits : MonoBehaviour
{
	[HideInInspector] public List<LaneUnitClass>[] lanes = new List<LaneUnitClass>[7];
	[HideInInspector] public float[] strength = new float[7];
	[HideInInspector] public float[] defence = new float[7];
	[HideInInspector] public int[] urgency = new int[7];
	private void Awake()
	{
		int i = 0;
		while (i < 7)
		{
			lanes[i] = new List<LaneUnitClass>();
			i++;
		}
	}
	private void Update()
	{
		ManageLineStrength();
	}
	private void ManageLineStrength()
	{
		int i = 0;
		while (i < 7)
		{
			strength[i] = 0.0f;
			defence[i] = 0.0f;
			urgency[i] = 1;
			if (lanes[i] != null)
			{
				foreach(LaneUnitClass troop in lanes[i])
				{
					UnitStrength(troop, i);
					UnitDefence(troop, i);
					if (troop.checkpoint == 2)
						urgency[i] = 3;
					else if (troop.checkpoint == 1)
						urgency[i] = 2;
				}
			}
			i++;
		}
	}
	private void UnitStrength(LaneUnitClass unit, int i)
	{
		switch(unit.unit)
		{
			case 0:
				strength[i] += UnitStats.rifleStrength;
				return ;
			case 1:
				strength[i] += UnitStats.scoutStrength;
				return ;
			case 2:
				strength[i] += UnitStats.bazookaStrength;
				return ;
			case 3:
				strength[i] += UnitStats.riotStrength;
				return ;
			case 4:
				strength[i] += UnitStats.tankStrength;
				return ;
			case 5:
				strength[i] += UnitStats.miniGunStrength;
				return ;
			case 6:
				strength[i] += UnitStats.mechStrength;
				return ;
			case 8:
				strength[i] += UnitStats.subStrength;
				return ;
			case 7:
				strength[i] += UnitStats.flyStrength;
				return ;
		}
	}
	private void UnitDefence(LaneUnitClass unit, int i)
	{
		switch(unit.unit)
		{
			case 0:
				defence[i] += UnitStats.rifleDefence;
				return ;
			case 1:
				defence[i] += UnitStats.scoutDefence;
				return ;
			case 2:
				defence[i] += UnitStats.bazookaDefence;
				return ;
			case 3:
				defence[i] += UnitStats.riotDefence;
				return ;
			case 4:
				defence[i] += UnitStats.tankDefence;
				return ;
			case 5:
				defence[i] += UnitStats.miniGunDefence;
				return ;
			case 6:
				defence[i] += UnitStats.mechDefence;
				return ;
			case 8:
				defence[i] += UnitStats.subDefence;
				return ;
			case 7:
				defence[i] += UnitStats.flyDefence;
				return ;
		}
	}
}

public class LaneUnitClass
{
	public GameObject troop;
	public int unit;
	public int checkpoint;
	public int index;
	public bool dead = false;
}
