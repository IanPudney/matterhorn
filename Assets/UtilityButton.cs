using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UtilityButton : MonoBehaviour {
	public Sprite button, trashcan;
	public static bool clicked;

	public void Update() {
		clicked = false;
		if (StateControl.state == StateControl.State.launching) {
			GetComponentInChildren<Text>().text = "Reset Level";
		} else if (StateControl.state == StateControl.State.drawing) {
			GetComponentInChildren<Text>().text = "Delete All Anomalies";
		}
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
			Vector3 mousePosition = Input.mousePosition;
			if (Mathf.Abs(mousePosition.x - transform.position.x) < GetComponent<RectTransform>().sizeDelta.x / 2
			    	&& Mathf.Abs(mousePosition.y - transform.position.y) < GetComponent<RectTransform>().sizeDelta.y / 2) {
				if (StateControl.state == StateControl.State.launching) {
					ResetLevel();
				} else if (StateControl.state == StateControl.State.drawing) {
					ClearNodes ();
				}
			}
		}
		if (MagnetWell.draggingAny) {
			GetComponent<Image>().sprite = trashcan;
			GetComponentInChildren<Text>().enabled = false;
			GetComponent<RectTransform>().sizeDelta = new Vector2(65, 100);
		} else {
			GetComponent<Image>().sprite = button;
			GetComponentInChildren<Text>().enabled = true;
			GetComponent<RectTransform>().sizeDelta = new Vector2(300, 100);
		}
	}
	
	void ClearNodes() {
		FindObjectOfType<MagneticNodeCounter>().ClearNodes();
		clicked = true;
	}
	
	void ResetLevel() {
		StateControl.BroadcastAll("RestoreState",null);
	}
}
