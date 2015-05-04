using UnityEngine;
using System.Collections;

public class CannonCore : MonoBehaviour {
	GameObject cannonLaunchButton;
	ParticleSystem readyEffect, launchEffect;
	float force = 5f;
	float launchTime = 1f;
	
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
		ParticleSystem[] systems = GetComponentsInChildren<ParticleSystem>();
		readyEffect = systems[0];
		readyEffect.enableEmission = true;
		launchEffect = systems[1];
		launchEffect.enableEmission = false;
	}
	
	void Update() {
		Vector3 buttonBase = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
		cannonLaunchButton.transform.position = buttonBase + Vector3.forward;
		cannonLaunchButton.transform.rotation = transform.parent.rotation;
		if (player != null && !hasPlayer && !player.isLaunching) {
			player = null;
			readyEffect.enableEmission = true;
			launchEffect.enableEmission = false;
		}
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
		readyEffect.enableEmission = false;
		launchEffect.enableEmission = true;
	}
}
