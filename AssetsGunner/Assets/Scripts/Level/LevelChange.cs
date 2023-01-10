using UnityEngine;
using TMPro;

public static class LeveleActive
{
	public static bool leveleActive;
	public static int currentLevel;
}

public class LevelChange : MonoBehaviour
{
	private AudioSource levelUp;
	[SerializeField] TMP_Text levelNumber;
	[SerializeField] GameObject newLevel;
	[SerializeField] GameObject skip;
	[HideInInspector] public int level { get; private set;} = 0;
	[SerializeField] private EnemyDeployer enemyMoney;
	[SerializeField] private Deploy playerMoney;
	[HideInInspector] public bool levelActive;
	[SerializeField] private LaneUnits enemies;
	[SerializeField] private LaneUnits allies;
	[SerializeField] private EnemyStrength addMoneyToThis;
	private float maxMoney;
	private bool canEnd = false;
	private bool skipped = false;
	[HideInInspector] public bool ending = false;
	private void Awake()
	{
		DamageToEndLine.money = false;
		LeveleActive.leveleActive = true;
		levelUp = GetComponent<AudioSource>();
		StartNextLevel();
	}
	private void Update()
	{
		if (levelActive && !(ending))
		{
			if (maxMoney < 3000.0f)
			{
				if (enemyMoney.money < (maxMoney * 0.1f))
					EndLevel();
			}
			else
			{
				if (enemyMoney.money <= 300.0f)
					EndLevel();
			}
		}
		if (levelActive == false)
			enemyMoney.money = 0.0f;
		if (canEnd && !ending)
			StartEnding();
		if (ending)
			CanWeSkip();
	}
	private void CanWeSkip()
	{
		for (int i = 0 ; (i < 7 && !skipped) ; i++)
		{
			if (enemies.strength[i] > 0.0f)
			{
				skip.SetActive(false);
				return ;
			}
		}
		skipped = true;
		skip.SetActive(true);
	}
	private void StartEnding()
	{
		Invoke(nameof(StopLevel), 10.0f);
		ending = true;
	}
	private void EndLevel()
	{
		float strength = 0.0f;
		for (int i = 0 ; i < 7 ; i++)
			strength += enemies.strength[i];
		if (strength >= 2.0f)
			return ;
		canEnd = true;
	}
	public void StopLevel()
	{
		CancelInvoke();
		levelActive = false;
		LeveleActive.leveleActive = false;
		ending = false;
		canEnd = false;
		skipped = false;
		DestroyTroops(enemies);
		DestroyTroops(allies);
		enemyMoney.money = 0.0f;
		levelUp.Play();
		Invoke(nameof(PutText), 0.5f);
		Invoke(nameof(StartNextLevel), 0.5f);
	}
	private void PutText()
	{
		newLevel.SetActive(false);
		newLevel.SetActive(true);
	}
	private void DestroyTroops(LaneUnits toDestroy)
	{
		for (int i = 0 ; i < 7 ; i++)
		{
			for (int j = 0 ; j < toDestroy.lanes[i].Count ; j++)
			{
				Health heal = toDestroy.lanes[i][j].troop.GetComponentInChildren<Health>();
				if (heal != null)
				{
					heal.armor = 0.0f;
					heal.health = 0.0f;
				}
			}
			toDestroy.lanes[i].Clear();
		}
	}
	private void StartNextLevel()
	{
		level += 1;
		enemyMoney.money = EnemyLevelMoney();
		int massi = PlayerLevelMoney();
		if (massi > playerMoney.money)
			playerMoney.money = massi;
		maxMoney = enemyMoney.money;
		addMoneyToThis.MaxMoneyAdded(maxMoney);
		levelActive = true;
		LeveleActive.leveleActive = true;
		LeveleActive.currentLevel = level;
		levelNumber.text = "LEVEL ";
		levelNumber.text += level.ToString();
		canEnd = false;
		ending = false;
	}
	private float EnemyLevelMoney()
	{
		float[] moneyChunk = {500.0f, 750.0f, 1000.0f, 1100.0f, 1500.0f, 2000.0f, 2200.0f, 2600.0f, 2900.0f, 3000.0f, 3500.0f, 4500.0f, 5500.0f, 7000.0f, 8500.0f, 10000.0f};
		if ((level - 1) < moneyChunk.Length)
			return (moneyChunk[level - 1]);
		return (10000.0f + (2000.0f * (level - moneyChunk.Length)));
	}
	private int PlayerLevelMoney()
	{
		int[] moneyChunk = {200, 300, 400, 500, 600, 800, 1000, 1200, 1400, 1400, 1800, 2300, 3500, 4500, 5000, 5500};
		if ((level - 1) < moneyChunk.Length)
			return (moneyChunk[level - 1]);
		return (6000);
	}
}
