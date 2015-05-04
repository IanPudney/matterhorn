using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class CannonLaunchButton : MonoBehaviour, IPointerClickHandler {
	[HideInInspector]
	public CannonCore cannonCore;
	
	public void OnPointerClick(PointerEventData mouseData) {
		cannonCore.LaunchPlayer();
	}
}
	