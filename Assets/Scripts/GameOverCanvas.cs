using UnityEngine;
using System.Collections;

public class GameOverCanvas : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			Debug.Log ("Space");
			StateControl.BroadcastAll("RestoreState", null);
			this.enabled = false;
			Object.Destroy(this.gameObject);

		}
	}


}
