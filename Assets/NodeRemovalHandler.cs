using UnityEngine;
using System.Collections;

public class NodeRemovalHandler : MonoBehaviour {
	float width = 100f;
	float height = 30f;
	public void Update() {
		if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) {
			Vector3 mousePosition = Input.mousePosition;
			if (Mathf.Abs(mousePosition.x - transform.position.x) < width / 2
			    && Mathf.Abs(mousePosition.y - transform.position.y) < height / 2) {
				ClearNodes ();
			}
		}
	}
	
	void ClearNodes() {
		FindObjectOfType<MagneticNodeCounter>().ClearNodes();
	}
}
