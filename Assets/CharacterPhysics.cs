﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterPhysics : MonoBehaviour {
	public List<MagnetWell> MagneticBodies;
	Rigidbody characterRigidbody;
	
	void Start () {
		if (characterRigidbody == null) {
			characterRigidbody = GetComponent<Rigidbody>();
		}
	}
	
	void FixedUpdate () {
		UpdateTrajectory();
	}
	
	public void AddWell(MagnetWell well) {
		if (!MagneticBodies.Contains(well)) {
			MagneticBodies.Add(well);
		}
	}
	
	void UpdateTrajectory() {
		foreach(MagnetWell well in MagneticBodies) {
			GetComponent<Rigidbody>().AddForce(well.GetForce());
		}
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log ("Game Over");
	}
}
