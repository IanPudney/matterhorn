using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class CannonRotationButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	[HideInInspector]
	public float minAngle;
	[HideInInspector]
	public float maxAngle;

	bool mouseIsOver = false;
	bool mouseIsDragging = false;
	RectTransform rect;
	[HideInInspector]
	public Transform cannonTransform;
	
	void Start() {
		rect = GetComponent<RectTransform>();
	}
	
	void Update() {
		if (cannonTransform == null) {
			return;
		}
		
		if (Input.GetMouseButtonDown(0) && mouseIsOver) {
			mouseIsDragging = true;
		} else if (Input.GetMouseButtonUp(0)) {
			mouseIsDragging = false;
		}
		
		if (mouseIsDragging) {
			FollowMouse();
		}
	}
	
	public void OnPointerEnter(PointerEventData mouseData) {
		mouseIsOver = true;
	}

	public void OnPointerExit(PointerEventData mouseData) {
		mouseIsOver = false;
	}
	
	void FollowMouse() {
		Vector3 mousePos;
		if (!RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, Input.mousePosition, Camera.main, out mousePos)) {
			return;
		}
		Vector3 direction = (mousePos - cannonTransform.position).normalized;
		Debug.DrawRay(cannonTransform.position, direction * 5f);
		float angle = Mathf.Atan2(direction.y, direction.x) * (180f / Mathf.PI);
		
		angle %= 360f;
		while (angle < 0f) {
			angle += 360f;
		}
		
		if (angle < minAngle || angle > maxAngle) {
			Debug.Log("Angle exceeded!  Angle " + angle + " Min " + minAngle + " Max " + maxAngle);
			return;
		}
		
		cannonTransform.eulerAngles = new Vector3(0, 0, angle);
	}
}
