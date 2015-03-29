using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverCanvas_Score : MonoBehaviour {
	string ScoreText;

	void GameWin() {
		MagneticNodeCounter magneticNodes = GameObject.FindObjectOfType<MagneticNodeCounter> ();
		Scorekeeper.UpdateScores(StateControl.main.currentRoom, magneticNodes.getMagneticNodes());
		GetComponent<Text> ().text = Scorekeeper.scoreText;
	}
	
	void GameLoss() {
		var magneticNodes = GameObject.FindObjectOfType<MagneticNodeCounter> ();
		if (magneticNodes) {
			Scorekeeper.UpdateScores(StateControl.main.currentRoom, magneticNodes.getMagneticNodes());
			GetComponent<Text> ().text = "";
		}
	}
}
