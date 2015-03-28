using UnityEngine;
using System.Collections;

public class GravityWell : MonoBehaviour {
	[HideInInspector]
	public bool disabled, active;
	
	public static float gravitationalConstant = 9.8f;
	
	public float activationDistance, disableDistance, mass;
	
	public CharacterPhysics character;
	
	void Start() {
		disabled = false;
		active = false;
	}
	
	void Update () {
		if (character == null) {
			character = FindObjectOfType<CharacterPhysics>();
			if (character == null) {
				return;
			}
		}

		if (!disabled) {
			if (!active) {
				if (GetDistance() < activationDistance) {
					active = true;
					character.AddWell(this);
				}
			} else {
				if (GetDistance() < disableDistance) {
					disabled = true;
					active = false;
					character.RemoveWell(this);
				}
			}
		}	
	}
	
	float GetDistance() {
		return Vector3.Distance(character.transform.position, transform.position);
	}
}
