using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpaceButton : MonoBehaviour {
	void Update() {
		if (StateControl.state == StateControl.State.launching) {
			GetComponentInChildren<Text>().text = "Reverse Polarity";
		} else if (StateControl.state == StateControl.State.drawing) {
			GetComponentInChildren<Text>().text = "Launch";
		}
	
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
			Vector3 mousePosition = Input.mousePosition;
			if (Mathf.Abs(mousePosition.x - transform.position.x) < GetComponent<RectTransform>().sizeDelta.x / 2
			    && Mathf.Abs(mousePosition.y - transform.position.y) < GetComponent<RectTransform>().sizeDelta.y / 2) {
				if (StateControl.state == StateControl.State.launching) {
					TogglePolarity();
				} else if (StateControl.state == StateControl.State.drawing) {
					Launch ();
				}
			}
		}
	}
	
	void Launch() {
		StateControl.BroadcastAll ("BackupState",null);
		StateControl.state = StateControl.State.launching;
		StateControl.BroadcastAll ("OnGameStart",null);
		StateControl.main.ToggleMusic();
	}
	
	void TogglePolarity() {
		StateControl.magneticPower = -StateControl.magneticPower;
	}
}
