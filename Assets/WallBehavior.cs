using UnityEngine;
using System.Collections;

public class WallBehavior : MonoBehaviour {
	public bool isBouncy;
	public bool isDeadly; 
	
	public PhysicMaterial bouncyWall, rigidWall;
	
	// Use this for initialization
	void Start () {
		if (isDeadly) {
			return;
		}
		if (isBouncy) {
			PhysicMaterial material = GetComponent<PhysicMaterial>();
			material = bouncyWall;
			GetComponent<Renderer>().material.color = Color.magenta;
		} else {
			PhysicMaterial material = GetComponent<PhysicMaterial>();
			material = bouncyWall;
			GetComponent<Renderer>().material.color = Color.gray;
		}
	}
	
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.GetComponent<CharacterPhysics>() != null) {
			StateControl.BroadcastAll("RestoreState", null);
		}
	}
}
