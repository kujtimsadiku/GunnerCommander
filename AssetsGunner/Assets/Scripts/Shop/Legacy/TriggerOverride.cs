using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TriggerOverride : EventTrigger
{
	public Button _button = null;
	public Button button { get { if (_button == null) { _button = Get(); } return _button; } }
	private Button Get()
	{
		return GetComponent<Button>();
	}
	public override void OnPointerEnter(PointerEventData eventData)
	{
		if (!button.interactable)
			return;
		base.OnPointerEnter(eventData);
	}
}