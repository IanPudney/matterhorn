using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialScript2 : MonoBehaviour {
	void Start() {
		GetComponent<Text>().text = "Watch out for the deadly red wall in the middle!\n"
			+ "You might need several anomalies of different colors for this one.";
	}
	
	void OnStart() {
	
	}
	
	void RestoreState() {
		Start();
	}
}
