using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scorekeeper : MonoBehaviour {
	public int NumLevels;
	public static List<int> HighScores;
	
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
		if (HighScores[index] > score) {
			HighScores[index] = score;
			PlayerPrefs.SetInt(index.ToString(), score);
		} else {
			print (HighScores[index] + " " + score);
		}
	}
}
