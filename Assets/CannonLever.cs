using UnityEngine;
using System.Collections;

public class CannonLever : MonoBehaviour {
	GameObject cannonLeverButton;
	
	// Use this for initialization
	void Start () {
		cannonLeverButton = Instantiate(StateControl.main.CannonLeverButtonPrefab) as GameObject;
		cannonLeverButton.transform.SetParent(StateControl.main.transform);
		cannonLeverButton.transform.localScale = Vector3.one;
		cannonLeverButton.GetComponent<CannonLeverButton>().cannonTransform = transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
		cannonLeverButton.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
	}
	
	void OnClick() {
		print("this thing");
	}
}
