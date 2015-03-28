using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {
	public float rotationSpeed = 1f;
	public float stickSize = 4f;
	public float crossbeamWidth = 10f;

	void setCrossbeamWidth(float width) {
		Transform[] transforms = GetComponentsInChildren<Transform> ();
		for (int i=0; i<transforms.Length; ++i) {
			if (transforms[i].name == "LeftWall") {
				transforms[i].localPosition = new Vector3(-width/2f, 0f, 0f);
			} else if (transforms[i].name == "RightWall") {
				transforms[i].localPosition = new Vector3(width/2f, 0f, 0f);
			} else if (transforms[i].name == "SpinnerBack") {
				transforms[i].localScale = new Vector3(width, 1f, 0.2f);
			} else if (transforms[i].name == "SpinnerLeftWing") {
				transforms[i].localPosition = new Vector3(-width*.45f, 0f, 1.5f);
			} else if (transforms[i].name == "SpinnerRightWing") {
				transforms[i].localPosition = new Vector3(width*.45f, 0f, 1.5f);
			}
		}
	}

	void setWallHeight(float height) {
		Transform[] transforms = GetComponentsInChildren<Transform> ();
		for (int i=0; i<transforms.Length; ++i) {
			if (transforms[i].name == "LeftWall") {
				transforms[i].localScale = new Vector3(1f, height, 1f);
			} else if (transforms[i].name == "RightWall") {
				transforms[i].localScale = new Vector3(1f, height, 1f);
			}
		}
	}

	bool gameStarted = false;
	// Use this for initialization
	void Start () {
		setCrossbeamWidth (crossbeamWidth);
		setWallHeight (stickSize);


	}
	
	// Update is called once per frame
	void Update () {
		if (gameStarted) {
			transform.Rotate (Vector3.back, rotationSpeed * Time.deltaTime);
		}
	}

	void OnGameStart() {
		gameStarted = true;
	}


}
