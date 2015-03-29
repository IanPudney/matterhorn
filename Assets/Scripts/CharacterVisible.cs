using UnityEngine;
using System.Collections;

public class CharacterVisible : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnBecameInvisible() {
		StateControl.main.EndLoss ();
	}
}
