using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterPhysics : MonoBehaviour {
	public List<MagnetWell> MagneticBodies;
	Rigidbody characterRigidbody;
	
	Vector3 velocity, acceleration;
	
	void Start () {
		if (characterRigidbody == null) {
			characterRigidbody = GetComponent<Rigidbody>();
		}
	}
	
	void FixedUpdate () {
		UpdateTrajectory();
		UpdateColor();
	}
	
	public void AddWell(MagnetWell well) {
		if (!MagneticBodies.Contains(well)) {
			MagneticBodies.Add(well);
		}
	}
	
	void UpdateTrajectory() {
		acceleration = Vector3.zero;
		foreach(MagnetWell well in MagneticBodies) {
			Vector3 force = well.GetForce();
			GetComponent<Rigidbody>().AddForce(force);
			acceleration += force;
		}
		Debug.DrawRay(transform.position, acceleration * 5f, Color.red);
		Debug.DrawRay(transform.position, characterRigidbody.velocity * 5f, Color.blue);
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log ("Game Over");
	}
	
	void UpdateColor() {
		Color newColor;
		if (StateControl.magneticPower > 0f) {
			float offColor = 1f - 0.5f * StateControl.magneticPower;
			newColor = new Color(offColor, offColor, 1);
		} else {
			float offColor = 1f + 0.5f * StateControl.magneticPower;
			newColor = new Color(1, offColor, offColor);
		}
		GetComponent<MeshRenderer>().material.color = newColor;
	}
}
