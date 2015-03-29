using UnityEngine;
using System.Collections;

public class SpaceButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Activate() {
		if (StateControl.state == StateControl.State.drawing) {
			StateControl.BroadcastAll ("BackupState",null);
			StateControl.state = StateControl.State.launching;
			StateControl.BroadcastAll ("OnGameStart",null);
			ToggleMusic();
	}
}
