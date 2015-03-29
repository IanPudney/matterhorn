using UnityEngine;
using System.Collections;

public class GameOverCanvas : MonoBehaviour {	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			if (StateControl.levelWon) {
				Application.LoadLevel(StateControl.main.destinationLevel);
			} else {
				StateControl.BroadcastAll("RestoreState", null);
				Object.Destroy(this.gameObject);
			}
		}
	}


}
