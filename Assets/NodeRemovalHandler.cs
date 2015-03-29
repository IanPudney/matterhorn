using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NodeRemovalHandler : MonoBehaviour {
	public Sprite button, trashcan;
	public static bool clicked;

	public void Update() {
		clicked = false;
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
			Vector3 mousePosition = Input.mousePosition;
			if (Mathf.Abs(mousePosition.x - transform.position.x) < GetComponent<RectTransform>().sizeDelta.x / 2
			    && Mathf.Abs(mousePosition.y - transform.position.y) < GetComponent<RectTransform>().sizeDelta.y / 2) {
				ClearNodes ();
			}
		}
		if (MagnetWell.draggingAny) {
			GetComponent<Image>().sprite = trashcan;
			GetComponentInChildren<Text>().enabled = false;
			GetComponent<RectTransform>().sizeDelta = new Vector2(28, 44);
		} else {
			GetComponent<Image>().sprite = button;
			GetComponentInChildren<Text>().enabled = true;
			GetComponent<RectTransform>().sizeDelta = new Vector2(140, 30);
		}
	}
	
	void ClearNodes() {
		FindObjectOfType<MagneticNodeCounter>().ClearNodes();
		clicked = true;
	}
}
