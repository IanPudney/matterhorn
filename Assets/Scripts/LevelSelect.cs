using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Beginner() {
		Instantiate (Resources.Load ("Prefabs/BeginnerLevelSelect"));
	}



	void Challenge() {
		Instantiate (Resources.Load ("Prefabs/ChallengeLevelSelect"));
	}

	void Custom() {
		Instantiate (Resources.Load ("Prefabs/CustomLevelSelect"));
	}



}
