using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterPhysics : MonoBehaviour {
	public List<GravityWell> GravitationalBodies;
	Rigidbody characterRigidbody;
	
	void Start () {
		if (characterRigidbody == null) {
			characterRigidbody = GetComponent<Rigidbody>();
		}
		characterRigidbody.AddForce(150f * Vector3.right);
	}
	
	void FixedUpdate () {
		UpdateTrajectory();
	}
	
	public void AddWell(GravityWell well) {
		GravitationalBodies.Add(well);
	}
	
	public void RemoveWell(GravityWell well) {
		GravitationalBodies.Remove(well);
	}
	
	void UpdateTrajectory() {
		foreach(GravityWell well in GravitationalBodies) {
			GetComponent<Rigidbody>().AddForce(GetForce(well));
		}
	}
	
	Vector3 GetForce(GravityWell well) {
		float distance = Vector3.Distance(well.transform.position, transform.position);
		Vector2 direction = (well.transform.position - transform.position).normalized;
		return direction * well.mass * GravityWell.gravitationalConstant / Mathf.Pow (distance, 2);
	}
}
