using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StaticHandler : MonoBehaviour {

	void ClearNodes() {
		if (StateControl.state == StateControl.State.drawing) {
			FindObjectOfType<MagneticNodeCounter>().ClearNodes();
		}
	}
	void ToggleLevelMode() {
		//toggles between launching and drawing
		if (StateControl.state == StateControl.State.launching) {
			ResetLevel ();
		} else if (StateControl.state == StateControl.State.drawing) {
			Launch();
		}
	}
	void ResetLevel() {
		StateControl.BroadcastAll ("RestoreState", null);
	}
	void Launch() {
		StateControl.BroadcastAll ("BackupState",null);
		StateControl.state = StateControl.State.launching;
		StateControl.BroadcastAll ("OnGameStart",null);
		StateControl.main.ToggleMusic();
	}
	
	void TogglePolarity() {
		if (StateControl.state == StateControl.State.launching) {
			StateControl.magneticPower = -StateControl.magneticPower;
		} else {
			StateControl.InvertNodePlacement();
			//todo: toggle color of nodes being placed
		}
	}
}
