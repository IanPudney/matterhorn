using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class CannonLeverButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
	bool mouseIsOver = false;
	bool isDragging = false;
	
	void Update() {
		if (!isDragging && Input.GetMouseButton(0) && mouseIsOver) {
			print ("Dragging!");
			isDragging = true;
		} else if (isDragging && !Input.GetMouseButton(0)) {
			print ("Released!");
			isDragging = false;
		}
	}
	
	public void OnPointerEnter(PointerEventData mouseData){
		mouseIsOver = true;
	}

	public void OnPointerExit(PointerEventData mouseData){
		mouseIsOver = true;
	}

	public void OnPointerClick(PointerEventData mouseData) {
	/*	print ("Yuck");
		print(mouseData.position);*/
	}
	
	void FollowMouse() {
		print ("Yes");
	}
}
