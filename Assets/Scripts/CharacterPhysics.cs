using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterPhysics : MonoBehaviour {
	Rigidbody characterRigidbody;
	public Vector3 initialForce;
	
	Vector3 velocity, acceleration;
	RigidbodyConstraints initialConstraints;
	
	void Start () {
		if (characterRigidbody == null) {
			characterRigidbody = GetComponent<Rigidbody>();
			initialConstraints = characterRigidbody.constraints;
			characterRigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}
	}

	void OnGameStart() {
		characterRigidbody.constraints = initialConstraints;
		characterRigidbody.AddForce (initialForce);
	}
	
	void FixedUpdate () {
		UpdateTrajectory();
		UpdateColor();
	}
	
	void UpdateTrajectory() {
		acceleration = Vector3.zero;
		foreach(MagnetWell well in FindObjectsOfType<MagnetWell>()) {
			Vector3 force = well.GetForce();
			GetComponent<Rigidbody>().AddForce(force);
			acceleration += force;
		}
		Debug.DrawRay(transform.position, acceleration * 5f, Color.red);
		Debug.DrawRay(transform.position, characterRigidbody.velocity * 5f, Color.blue);
		if (acceleration.x - acceleration.y > 0) {
			characterRigidbody.rotation *= Quaternion.Euler(Vector3.forward * acceleration.magnitude);
		} else {
			characterRigidbody.rotation *= Quaternion.Euler(Vector3.back * acceleration.magnitude);
		}
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log ("Game Over");
	}
	
	void UpdateColor() {
		Color newColor;
		if (StateControl.magneticPower < 0f) {
			float offColor = 1f + StateControl.magneticPower;
			newColor = new Color(offColor, offColor, 1);
		} else {
			float offColor = 1f - StateControl.magneticPower;
			newColor = new Color(1, offColor, offColor);
		}
		GetComponentInChildren<ParticleSystem>().startColor = newColor;
	}
}
