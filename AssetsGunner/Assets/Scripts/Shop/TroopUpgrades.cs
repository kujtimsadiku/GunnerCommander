using UnityEngine;
using TMPro;

[System.Serializable]
public struct Upgrades
{
	public float health;
	public float armor;
	public float damageMin;
	public float damageMax;
	public float speed;
	public float deploySpeed;
}

public class TroopUpgrades : MonoBehaviour
{
	[SerializeField] private UpStats stats;
	[SerializeField] private Deploy deploySpeed;
	[SerializeField] private int unit;
	[SerializeField] private int[] costs;
	[SerializeField] private Upgrades[] upgrades;
	[SerializeField] private ShopBar[] bar;
	[SerializeField] private TMP_Text max;
	private float healthScale => 1.0f / 900.0f;
	private float damageScale => 1.0f / 220.0f;
	private float speedScale => 1.0f / 1.6f;
	private float deployScale => 1.0f / 0.075f;
	private InteractButton button;
	private int current;
	private float mini = 8.0f;
	private float sub = 4.0f;
	private void Awake()
	{
		button = GetComponent<InteractButton>();
	}
	private void Start()
	{
		bar[0].ChangeTransform(healthScale * (stats.stats[unit].health + stats.stats[unit].armor));
		if (unit == 5)
			bar[1].ChangeTransform(damageScale * (((stats.stats[unit].damageMin * mini) + (stats.stats[unit].damageMax * mini)) / 2.0f));
		else if (unit == 6)
			bar[1].ChangeTransform(damageScale * (((stats.stats[unit].damageMin * sub) + (stats.stats[unit].damageMax * sub)) / 2.0f));
		else
			bar[1].ChangeTransform(damageScale * ((stats.stats[unit].damageMin + stats.stats[unit].damageMax) / 2.0f));
		bar[2].ChangeTransform(speedScale * stats.stats[unit].speed);
		bar[3].ChangeTransform(deployScale * (deploySpeed.deplopySpeeds[unit]));
		button.cost = costs[0];
	}
	public void MouseOver()
	{
		if (current < upgrades.Length)
		{
			bar[0].Hover(healthScale * (stats.stats[unit].health + stats.stats[unit].armor + upgrades[current].health + upgrades[current].armor));
			if (unit == 5)
				bar[1].Hover(damageScale * ((((stats.stats[unit].damageMin + upgrades[current].damageMin) * mini) + ((stats.stats[unit].damageMax + upgrades[current].damageMax) * mini)) / 2.0f));
			else if (unit == 6)
				bar[1].Hover(damageScale * ((((stats.stats[unit].damageMin + upgrades[current].damageMin) * sub) + ((stats.stats[unit].damageMax + upgrades[current].damageMax) * sub)) / 2.0f));
			else
				bar[1].Hover(damageScale * ((stats.stats[unit].damageMin + upgrades[current].damageMin + stats.stats[unit].damageMax + upgrades[current].damageMax) / 2.0f));
			bar[2].Hover(speedScale * (stats.stats[unit].speed + upgrades[current].speed));
			bar[3].Hover(deployScale * (deploySpeed.deplopySpeeds[unit] + (0.001f * upgrades[current].deploySpeed)));
		}
	}
	public void Clicked()
	{
		bar[0].ChangeTransform(healthScale * (stats.stats[unit].health + stats.stats[unit].armor + upgrades[current].health + upgrades[current].armor));
		if (unit == 5)
			bar[1].ChangeTransform(damageScale * ((((stats.stats[unit].damageMin + upgrades[current].damageMin) * mini) + ((stats.stats[unit].damageMax + upgrades[current].damageMax) * mini)) / 2.0f));
		else if (unit == 6)
			bar[1].ChangeTransform(damageScale * ((((stats.stats[unit].damageMin + upgrades[current].damageMin) * sub) + ((stats.stats[unit].damageMax + upgrades[current].damageMax) * sub)) / 2.0f));
		else
			bar[1].ChangeTransform(damageScale * ((stats.stats[unit].damageMin + upgrades[current].damageMin + stats.stats[unit].damageMax + upgrades[current].damageMax) / 2.0f));
		bar[2].ChangeTransform(speedScale * (stats.stats[unit].speed + upgrades[current].speed));
		bar[3].ChangeTransform(deployScale * (deploySpeed.deplopySpeeds[unit] + (0.001f * upgrades[current].deploySpeed)));
		stats.stats[unit].damageMin += upgrades[current].damageMin;
		stats.stats[unit].damageMax += upgrades[current].damageMax;
		stats.stats[unit].health += upgrades[current].health;
		stats.stats[unit].armor += upgrades[current].armor;
		stats.stats[unit].speed += upgrades[current].speed;
		deploySpeed.deplopySpeeds[unit] += (0.001f * upgrades[current].deploySpeed);
		current++;
		if (current < upgrades.Length)
			button.cost = costs[current];
		else
			button.reUseable = false;
	}
	public void MouseQuit()
	{
		bar[0].QuitHover();
		bar[1].QuitHover();
		bar[2].QuitHover();
		bar[3].QuitHover();
	}
}
