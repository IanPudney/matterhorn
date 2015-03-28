using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateControl : MonoBehaviour {
	public static StateControl main;
	public enum State {
		drawing,
		launching
	};
	public State desiredState;
	public bool toggleEnabled;
	public static State state;
	
	public float magneticPower = -0.5f;
	public Text magneticDisplay;
	
	void Start () {
		main = this;
	}
	
	void Update() {
		if (toggleEnabled) {
			toggleEnabled = false;
			state = desiredState;
		}
		
		UpdateDisplay();
	}
	
	void UpdateDisplay() {
		if (magneticDisplay == null) {
			magneticDisplay = GameObject.FindGameObjectWithTag("MagneticConstantDisplay").GetComponent<Text>();
			if (magneticDisplay == null) {
				return	;
			}	
		}
		magneticDisplay.text = magneticPower.ToString("F4");
	}
}
