using UnityEngine;
using System.Collections;

public class CustomLevelLoaderButton : MonoBehaviour {
	public string path;
	void Run() {
		GameObject.Find ("CustomLevel").GetComponent<LoadCustomLevel> ().SendMessage ("StartLevel", path);
		GameObject.Destroy ((GameObject)GameObject.Find ("CustomLevelSelect"));
	}
}
