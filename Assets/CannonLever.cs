using UnityEngine;
using System.Collections;

public class CannonLever : MonoBehaviour {
	GameObject cannonLeverButton;
	public float minAngle = 0f;
	public float maxAngle = 360f;
	
	// Use this for initialization
	void Start () {
		cannonLeverButton = Instantiate(StateControl.main.CannonLeverButtonPrefab) as GameObject;
		cannonLeverButton.transform.SetParent(StateControl.main.transform);
		cannonLeverButton.transform.localScale = Vector3.one;
		CannonLeverButton clb = cannonLeverButton.GetComponent<CannonLeverButton>();
		clb.cannonTransform = transform.parent;
		clb.minAngle = minAngle;
		clb.maxAngle = maxAngle;
	}
	
	// Update is called once per frame
	void Update () {
		cannonLeverButton.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
	}
	
	void OnClick() {
		print("this thing");
	}
}
