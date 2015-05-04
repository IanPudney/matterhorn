using UnityEngine;
using System.Collections;

public class CannonRotation : MonoBehaviour {
	GameObject cannonLeverButton;
	public float minAngle = 0f;
	public float maxAngle = 360f;
	
	// Use this for initialization
	void Start() {
		cannonLeverButton = Instantiate(StateControl.main.CannonLeverButtonPrefab) as GameObject;
		cannonLeverButton.transform.SetParent(StateControl.main.transform);
		cannonLeverButton.transform.localScale = Vector3.one;
		CannonRotationButton clb = cannonLeverButton.GetComponent<CannonRotationButton>();
		clb.cannonTransform = transform;
		clb.minAngle = minAngle;
		clb.maxAngle = maxAngle;
		CannonCore cc = GetComponentInChildren<CannonCore>();
		cc.DependentStart();
		
		if (minAngle % 360f == maxAngle % 360f) {
			return;
		}
		GameObject minLine = Instantiate (StateControl.main.ParticleLinePrefab);
		minLine.transform.position = transform.position + Quaternion.Euler(0, 0, minAngle) * new Vector3(1, -1, 0);
		minLine.transform.eulerAngles = new Vector3(-minAngle, 90, 0);
		GameObject maxLine = Instantiate (StateControl.main.ParticleLinePrefab);
		maxLine.transform.position = transform.position + Quaternion.Euler(0, 0, maxAngle) * new Vector3(1, 1, 0);
		maxLine.transform.eulerAngles = new Vector3(-maxAngle, 90, 0);
		if (transform.eulerAngles.z < minAngle || transform.eulerAngles.z > maxAngle) {
			transform.eulerAngles = new Vector3(0, 0, minAngle);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 buttonBase = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position + 3.5f * transform.right);
		cannonLeverButton.transform.position = buttonBase;
		cannonLeverButton.transform.rotation = transform.rotation;
	}
	
	void OnClick() {
		print("this thing");
	}
}
