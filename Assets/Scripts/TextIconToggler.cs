using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextIconToggler : MonoBehaviour {
	public string drawingName;
	public string launchingName;

	//todo: add icon support

	void Start () {
		gameObject.GetComponent<Text>().text = drawingName;
	}
	
	void OnGameStart() {
		gameObject.GetComponent<Text>().text = launchingName;
	}

	void RestoreState() {
		gameObject.GetComponent<Text>().text = drawingName;
	}


}
