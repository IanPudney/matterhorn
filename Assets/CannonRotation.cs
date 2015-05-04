using UnityEngine;
using System.Collections;

public class CannonRotation : MonoBehaviour {
	GameObject cannonLeverButton;
	RectTransform rect;
	public float minAngle = 0f;
	public float maxAngle = 360f;
	
	// Use this for initialization
	void Start () {
		cannonLeverButton = Instantiate(StateControl.main.CannonLeverButtonPrefab) as GameObject;
		cannonLeverButton.transform.SetParent(StateControl.main.transform);
		cannonLeverButton.transform.localScale = Vector3.one;
		CannonLeverButton clb = cannonLeverButton.GetComponent<CannonLeverButton>();
		clb.cannonTransform = transform;
		clb.minAngle = minAngle;
		clb.maxAngle = maxAngle;
		rect = cannonLeverButton.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 buttonBase = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
		cannonLeverButton.transform.position = buttonBase + rect.rect.width * transform.right / 4;
		cannonLeverButton.transform.rotation = transform.rotation;
	}
	
	void OnClick() {
		print("this thing");
	}
}
