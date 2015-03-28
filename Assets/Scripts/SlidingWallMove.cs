using UnityEngine;
using System.Collections;

public class SlidingWallMove : MonoBehaviour {

	public GameObject wall;
	public GameObject endpieceOne;
	public GameObject endpieceTwo;
	public float velocity = 3f;
	public float height = 2f;
	public float travel = 10f;
	public float startPosition = 5f;

	private Vector3 velocityVector;

	Rigidbody wallRigidBody;

	// Use this for initialization
	void Start () {
		if (wallRigidBody == null) {
			wallRigidBody = wall.GetComponent<Rigidbody>();
		}
		setHeight (height);
		setTravel (travel);
		setStartPosition (startPosition);
	}

	void OnGameStart() {
		velocityVector = new Vector3 (0, velocity, 0);
	}

	void setHeight(float height) {
		Transform[] transforms = GetComponentsInChildren<Transform> ();
		for (int i=0; i<transforms.Length; ++i) {
			if (transforms[i].name == "SlidingWall") {
				transforms[i].localScale = new Vector3(1f, height, 1f);
			}
		}
	}

	void setTravel(float travel) {
		Transform[] transforms = GetComponentsInChildren<Transform> ();
		for (int i=0; i<transforms.Length; ++i) {
			if (transforms[i].name == "EndpieceOne") {
				transforms[i].localPosition = new Vector3(0f, travel, 0f);
			}
		}
	}

	void setStartPosition(float startPosition) {
		Transform[] transforms = GetComponentsInChildren<Transform> ();
		for (int i=0; i<transforms.Length; ++i) {
			if (transforms[i].name == "SlidingWall") {
				transforms[i].localPosition = new Vector3(0f, startPosition, 0f);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		wall.transform.localPosition += velocityVector * Time.deltaTime;
	}

	public void ReverseDirection() {
		Debug.Log ("Direction Reverse");
		velocity *= -1;
		velocityVector *= -1;
	}
}
