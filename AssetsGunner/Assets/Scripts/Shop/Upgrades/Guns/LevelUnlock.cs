using UnityEngine;
using UnityEngine.UI;

public class LevelUnlock : MonoBehaviour
{
	[SerializeField] private PickGun owned;
	[SerializeField] private InteractButton button;
	[SerializeField] private InteractButton button2;
	[SerializeField] private ButtonLine line1;
	[SerializeField] private ButtonLine line2;
	[SerializeField] private GameObject image1;
	[SerializeField] private GameObject image2;
	[SerializeField] private Image star1;
	[SerializeField] private Image star2;
	private bool one = false;
	private bool two = false;
	private Color upStar = new Color(1.0f, 0.729f, 0.0f);
	private void FixedUpdate()
	{
		if (one && two)
			return ;
		if (LeveleActive.currentLevel == 6 && !one)
		{
			owned.ownedGuns[1] = 1;
			button.unlocked = true;
			button.showPrice = true;
			line1.ChangeText();
			star1.color = upStar;
			Destroy(image1);
			one = true;
		}
		if (LeveleActive.currentLevel == 10 && !two)
		{
			owned.ownedGuns[2] = 1;
			Destroy(image2);
			star2.color = upStar;
			line2.ChangeText();
			button2.unlocked = true;
			button2.showPrice = true;
			two = true;
		}
	}
}
