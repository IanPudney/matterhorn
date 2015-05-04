using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Scorekeeper : MonoBehaviour {
	const int NumLevels = 15;
	public static List<int> HighScores;
	public static string scoreText;
	
	void Awake() {
		HighScores = new List<int>();
		for (int i = 0; i < NumLevels; ++i) {
			if (PlayerPrefs.GetInt(i.ToString()) != 0) {
				HighScores.Add (PlayerPrefs.GetInt(i.ToString()));
			} else {
				HighScores.Add (int.MaxValue);
			}
		}
	}
	
	public static void UpdateScores(int index, int score) {
		scoreText = "Nodes Used: " + score;
		if (HighScores.Count < index) {
			scoreText = "Level number invalid!";
		} else if (HighScores[index] > score) {
			HighScores[index] = score;
			if (score == 1) {
				scoreText = "You have beaten the old record (" + HighScores[index] + ") using"
					+ " only 1 Node!";
			} else {
				scoreText = "You have beaten the old record (" + HighScores[index] + ") with"
					+ " your score of " + score + "!";
			}
			PlayerPrefs.SetInt(index.ToString(), score);
		} else if (HighScores[index] == score) {
			if (score == 1) {
				scoreText = "You tied the record using 1 node!";
			} else {
				scoreText = "You tied the record using " + score + " nodes!";
			}
		} else {
			scoreText = "You used " + score + " nodes."
				+ "  The record is  " + HighScores[index] + "!";
		}
	}
}
