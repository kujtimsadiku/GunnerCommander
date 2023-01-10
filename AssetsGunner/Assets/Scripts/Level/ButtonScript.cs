using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
 
public class ButtonScript : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private LevelChange change;
	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right)
			change.StopLevel();
	}
}
