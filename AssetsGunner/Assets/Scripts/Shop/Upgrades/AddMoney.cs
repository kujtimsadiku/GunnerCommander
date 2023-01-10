using UnityEngine;

public class AddMoney : MonoBehaviour
{
	[SerializeField] private int amount;
	public void Money()
	{
		PlayerMoney.normalMoney += amount;
		PlayerMoney.moneyComing = true;
	}
}
