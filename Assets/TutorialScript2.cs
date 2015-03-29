using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialScript2 : MonoBehaviour {
	void Start() {
		GetComponent<Text>().text = "Watch out for the deadly red wall in the middle!\n"
			+ "You can use red anomalies (right click) to push instead of pull";
	}
	
	void OnGameStart() {
		GetComponent<Text>().text = "Press P to restart if you get stuck!";
	}
	
	void RestoreState() {
		Start();
	}
}
