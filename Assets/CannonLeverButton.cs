using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class CannonLeverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public float minAngle = 0f;
	public float maxAngle = 360f;

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
		if (mouseIsDragging) {
			FollowMouse();
		}
		
		if (!mouseIsDragging && Input.GetMouseButton(0) && mouseIsOver) {
			print ("Dragging!");
			mouseIsDragging = true;
		} else if (mouseIsDragging && !Input.GetMouseButton(0)) {
			print ("Released!");
			mouseIsDragging = false;
		}
	}
	
	public void OnPointerEnter(PointerEventData mouseData){
		mouseIsOver = true;
	}

	public void OnPointerExit(PointerEventData mouseData){
		mouseIsOver = true;
	}
	
	void FollowMouse() {
		Vector3 mousePos;
		if (!RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, Input.mousePosition, Camera.main, out mousePos)) {
			return;
		}
		Vector3 direction = (mousePos - cannonTransform.position).normalized;
		Debug.DrawRay(cannonTransform.position, direction * 5f);
		float angle = Mathf.Atan2(direction.y, direction.x) * (180f / Mathf.PI) + 90f;
		print (angle);
		cannonTransform.eulerAngles = new Vector3(0, 0, angle);
	}
}
