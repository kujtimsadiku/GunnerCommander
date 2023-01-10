using UnityEngine;

public class EnemyStrength : MonoBehaviour
{
	[SerializeField] private GameObject bar;
	[SerializeField] private LaneUnits enemies;
	[SerializeField] private EnemyDeployer money;
	private float currentStrength;
	private float maxMoney;
	private void Update()
	{
		CalculateCurrentStrength();
		bar.transform.localScale = new Vector3(2.0f * currentStrength, 0.5f, 1.0f);
		if (bar.transform.localScale.x < 0.0f)
			bar.transform.localScale = new Vector3(0.0f, 0.5f, 1.0f);
	}
	private void CalculateCurrentStrength()
	{
		float strengthMoney = money.money;
		for (int i = 0; i < 7; i++)
		{
			for (int j = 0; j < enemies.lanes[i].Count; j++)
				strengthMoney += TroopCosts.costs[enemies.lanes[i][j].unit];
		}
		if (strengthMoney > maxMoney)
			currentStrength = 1.0f;
		else
			currentStrength = strengthMoney / maxMoney;
	}
	public void MaxMoneyAdded(float add)
	{
		maxMoney = add;
	}
}
