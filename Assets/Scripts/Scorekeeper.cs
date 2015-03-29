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
		if (HighScores[index] > score) {
			HighScores[index] = score;;
			scoreText = "Congratulations!  You have beaten the old record of " + HighScores[index] + " with"
					+ "a new best of only " + score + " Nodes!";
			PlayerPrefs.SetInt(index.ToString(), score);
		} else {
			print (HighScores[index] + " " + score);
		}
	}
}
