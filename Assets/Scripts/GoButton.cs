using UnityEngine;
using System.Collections;

public class GoButton : MonoBehaviour {
	public static bool goButtonDown = false;
	static int loops = 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		goButtonDown = false;
	}

	void Run(string s) {
		goButtonDown = true;
		Debug.Log ("Calling SpaceFn");
		StateControl.main.SendMessage ("SpaceFn");
	}
}
