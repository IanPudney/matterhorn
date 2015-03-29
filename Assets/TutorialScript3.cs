using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialScript3 : MonoBehaviour {
	void Update () {
		GetComponent<Text>().text = "After launching, you can reverse your polarity with Spacebar.\n"
			+ "Try switching right as you pass through anomalies to launch yourself!";
	}
}
