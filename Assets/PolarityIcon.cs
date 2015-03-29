using UnityEngine;
using System.Collections;

public class PolarityIcon : MonoBehaviour {
	/*void Awake() {
		gameObject.SetActive(false);
	}*/

	void OnGameStart() {
		gameObject.SetActive(true);
	}
	
	void RestoreState() {
		gameObject.SetActive(false);
	}
}
