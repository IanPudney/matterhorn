using UnityEngine;
using System.Collections;

public class GoalContact : MonoBehaviour {
	public string destinationScene;
	
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.GetComponent<CharacterPhysics>() != null && StateControl.state == StateControl.State.launching) {
			StateControl.main.EndWin (destinationScene);
		}
	}
}
