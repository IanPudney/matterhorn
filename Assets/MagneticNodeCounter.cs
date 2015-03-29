using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MagneticNodeCounter : MonoBehaviour {
	public int maxNodes = 5;
	int magneticNodes;
	
	void Start () {
		magneticNodes = 0;
		GetComponent<Text>().color = Color.white;
		GetComponent<Text>().text = "0/" + maxNodes.ToString();
	}
	
	public bool addNode() {
		if (magneticNodes < maxNodes) {
			magneticNodes += 1;
			GetComponent<Text>().text = magneticNodes.ToString() + "/" + maxNodes.ToString();
			if (magneticNodes == maxNodes) {
				GetComponent<Text>().color = Color.gray;
			}
			return true;
		} else {
			return false;
		}
	}
	
	public void ClearNodes() {
		foreach (MagnetWell well in FindObjectsOfType<MagnetWell>()) {
			Destroy (well.gameObject);
		}
		Start ();
	}

	public int getMagneticNodes() {
		return magneticNodes;
	}
}
