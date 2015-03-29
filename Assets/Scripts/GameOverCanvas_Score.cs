using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverCanvas_Score : MonoBehaviour {
	bool win = false;
	string ScoreText;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (win) {
			GetComponent<Text> ().text = Scorekeeper.scoreText;
		}
	}

	void GameWin() {
		win = true;
	}
	
	void GameLoss() {
		win = false;
	}
}
