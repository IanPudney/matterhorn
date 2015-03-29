using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseIcon : MonoBehaviour {
	void OnGameStart() {
		SetRendererAndCollider(gameObject, false);
	}
	
	void RestoreState() {
		SetRendererAndCollider(gameObject, true);
	}
	
	protected void SetRendererAndCollider(GameObject gameObject, bool toggle) {
		if (gameObject.GetComponent<Renderer>()) {
			gameObject.GetComponent<Renderer>().enabled = toggle;
		}
		if (gameObject.GetComponent<Image>()) {
			gameObject.GetComponent<Image>().enabled = toggle;
		}
		if (gameObject.GetComponent<Text>()) {
			gameObject.GetComponent<Text>().enabled = toggle;
		}
		
		if (gameObject.GetComponent<Collider>()) {
			gameObject.GetComponent<Collider>().enabled = toggle;
		}
		
		foreach (Transform child in gameObject.transform) {
			SetRendererAndCollider(child.gameObject, toggle);
		}
	}
}
