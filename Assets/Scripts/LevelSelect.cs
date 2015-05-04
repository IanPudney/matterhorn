using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {
	void Beginner() {
		Instantiate (Resources.Load ("Prefabs/BeginnerLevelSelect"));
	}

	void Challenge() {
		Instantiate (Resources.Load ("Prefabs/AdvancedLevelSelect"));
	}

	void Custom() {
		Application.LoadLevel ("_CustomLevel");
	}

	void Remoting() {
		Application.LoadLevel ("Remote");
	}
}
