using UnityEngine;
using System.Collections;

public class GoalContact : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.GetComponent<CharacterPhysics>() != null && StateControl.state == StateControl.State.launching) {
			StateControl.main.EndWin ();
		}
	}
}
