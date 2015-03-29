using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverCanvas_Result : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GameWin() {
		GetComponent<Text> ().text = "Victory";
	}

	void GameLoss() {
		GetComponent<Text> ().text = "Loss";
	}
}
