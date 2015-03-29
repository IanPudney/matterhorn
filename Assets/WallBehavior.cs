using UnityEngine;
using System.Collections;

public class WallBehavior : MonoBehaviour {
	public enum WallType {
		bouncy,
		deadly,
	}

	public WallType wallType;
	
	public PhysicMaterial bouncyWall, rigidWall;
	
	// Use this for initialization
	void Start () {
		if (wallType==WallType.bouncy) {
			PhysicMaterial material = GetComponent<PhysicMaterial> ();
			material = bouncyWall;
			GetComponent<Renderer> ().material.color = Color.gray;
		} else {

		}

	}
	
	void OnCollisionEnter(Collision other) {
		if (wallType == WallType.deadly && other.gameObject.GetComponent<CharacterPhysics>() != null) {
			StateControl.main.EndLoss();
		}
	}
}
