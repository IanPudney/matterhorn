using UnityEngine;
using System.Collections;

public class CharacterVisible : MonoBehaviour {
	void OnBecameInvisible() {
		if (GetComponentInParent<CharacterPhysics>() != null && StateControl.main != null) {
			StateControl.main.EndLoss ();
		}
	}
}
