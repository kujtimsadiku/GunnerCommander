using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TriggerNotActive : EventTrigger
{
	public Button _button = null;
	private InteractButton active;
	public Button button { get { if (_button == null) { _button = Get(); } return _button; } }
	private void Awake()
	{
		active = GetComponent<InteractButton>();
	}
	private Button Get()
	{
		return GetComponent<Button>();
	}
	public override void OnPointerEnter(PointerEventData eventData)
	{
		if (button.interactable || active.unlocked)
			return;
		base.OnPointerEnter(eventData);
	}
}
