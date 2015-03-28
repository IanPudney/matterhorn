using UnityEngine;
using System.Collections;

public class SlidingWallMove : MonoBehaviour {

	public GameObject wall;
	public GameObject endpieceOne;
	public GameObject endpieceTwo;
	public float velocity;

	private Vector3 velocityVector;

	Rigidbody wallRigidBody;

	// Use this for initialization
	void Start () {
		if (wallRigidBody == null) {
			wallRigidBody = wall.GetComponent<Rigidbody>();
		}
		velocityVector = new Vector3 (0, velocity, 0);
	}
	
	// Update is called once per frame
	void Update () {
		wall.transform.position += velocityVector * Time.deltaTime;
	}

	public void ReverseDirection() {
		Debug.Log ("Direction Reverse");
		velocity *= -1;
		velocityVector *= -1;
	}
}
