using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelScoreTextDisplay : MonoBehaviour {
	public string id;
	
	void Start () {
		int score = PlayerPrefs.GetInt(id);
		if (score == 0) {
			GetComponent<Text>().text = "No one has finished!";
		} else {
			GetComponent<Text>().text = "Current record: " + score.ToString();
		}
	}
}
