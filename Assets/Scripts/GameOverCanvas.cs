using UnityEngine;
using System.Collections;

public class GameOverCanvas : MonoBehaviour {	
	// Update is called once per frame
	void Update () {
		Vector3 mousePosition = Input.mousePosition;		    
		if (Input.GetKeyDown ("space") || (
				((Input.GetMouseButton(0) || Input.GetMouseButton(1)) &&
				 (Mathf.Abs(mousePosition.x - transform.position.x) < GetComponent<RectTransform>().sizeDelta.x / 2
				 && Mathf.Abs(mousePosition.y - transform.position.y) < GetComponent<RectTransform>().sizeDelta.y / 2)))
		) {
			if (StateControl.levelWon) {
				Application.LoadLevel(StateControl.main.destinationLevel);
			} else {
				StateControl.BroadcastAll("RestoreState", null);
				Object.Destroy(this.gameObject);
			}
		}
	}


}
