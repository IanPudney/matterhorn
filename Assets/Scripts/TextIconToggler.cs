using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextIconToggler : MonoBehaviour {
	public string drawingName;
	public Sprite drawingSprite;
	public string launchingName;
	public Sprite launchingSprite;

	//todo: add icon support

	void Start () {
		gameObject.GetComponentInChildren<Text> ().text = drawingName;
		gameObject.GetComponent<Image> ().overrideSprite = drawingSprite;
	}
	
	void OnGameStart() {
		gameObject.GetComponentInChildren<Text> ().text = drawingName;
		gameObject.GetComponent<Image> ().overrideSprite = launchingSprite;
	}

	void RestoreState() {
		gameObject.GetComponentInChildren<Text> ().text = drawingName;
		gameObject.GetComponent<Image> ().overrideSprite = drawingSprite;
	}


}
