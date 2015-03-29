using UnityEngine;
using System.Collections;

public class MouseIcon : MonoBehaviour {
	void OnGameStart() {
		gameObject.SetActive(false);
	}
	
	void RestoreState() {
		gameObject.SetActive(true);
	}
}
