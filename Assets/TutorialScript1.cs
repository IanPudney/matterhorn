using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialScript1 : MonoBehaviour {
	int state =0;
	
	void Update () {
		switch(state) {
			case 0:
				if (FindObjectOfType<MagneticNodeCounter>().getMagneticNodes() != 0 && !MagnetWell.draggingAny) {
					state = 1;
					GetComponent<Text>().text = "The color of your anomaly depends on whether you clicked left or right!\n"
							+ "Left click and drag the anomaly to the top of the gray rod!";
				}
				break;
			case 1:
				if (MagnetWell.draggingAny) {
					state = 2;
				}
				break;
			case 2:
				if (!MagnetWell.draggingAny) {
					state = 3;
					GetComponent<Text>().text = "Press Spacebar to launch from the tube!  Try to pass through the thin goal!";
				}
				break;
			case 3:
				break;
			case 4:
				GetComponent<Text>().text = "If you get stuck, press P and return to the start of the tutorial!";
				break;
		}
	}
	
	void OnGameStart() {
		state = 4;
	}
	
	void RestoreState() {
		state = 0;
	}
}
