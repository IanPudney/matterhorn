using UnityEngine;
using System.Collections;

public class CannonCore : MonoBehaviour {
	GameObject cannonLaunchButton;
	float force = 20f;
	float launchTime = 0.25f;
	
	bool hasPlayer = false;
	CharacterPhysics player;
	
	//Functions like start, but must be called after another Start routine.
	//This ordering allows the parent hierarchy to work as intended (this button being covered by another).
	public void DependentStart() {
		cannonLaunchButton = Instantiate(StateControl.main.CannonLaunchButtonPrefab) as GameObject;
		cannonLaunchButton.transform.SetParent(StateControl.main.transform);
		cannonLaunchButton.transform.localScale = Vector3.one;
		CannonLaunchButton clb = cannonLaunchButton.GetComponent<CannonLaunchButton>();
		clb.cannonCore = this;
	}
	
	void Update() {
		Vector3 buttonBase = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
		cannonLaunchButton.transform.position = buttonBase + Vector3.forward;
		cannonLaunchButton.transform.rotation = transform.parent.rotation;
	}
	
	void OnTriggerEnter(Collider other) {
		if (hasPlayer || other.name != "Character") {
			return;
		}
		if (player != null && player.isLaunching) {
			return;
		}
		hasPlayer = true;
		player = other.GetComponent<CharacterPhysics>();
		player.EnterCannon(transform.position);
	}
	
	public void LaunchPlayer() {
		if (!hasPlayer) {
			return;
		}
		player.StartLaunchFromCannon(transform.right * force, launchTime);
		hasPlayer = false;
	}
}
