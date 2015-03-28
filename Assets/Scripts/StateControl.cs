using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateControl : MonoBehaviour {
	public static StateControl main;

	public enum State {
		drawing,
		launching
	};
	public static State state;

	public static float magneticPower;
	public float magneticPowerStart;
	public Text magneticDisplay;

	public GameObject magnetWellPrefab;
	public static bool polarityIsPositive;

	public Text magneticStrengthUIText;
	public Text magneticPolarityUIText;
	public Image magneticPolarityUIImage;
	
	void Start () {
		main = this;
		magneticPower = magneticPowerStart;
	}

	public static void BroadcastAll(string fun, System.Object msg) {
		GameObject[] gos = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
		foreach (GameObject go in gos) {
			if (go) {
				Debug.Log ("Broadcasting Message");
				go.gameObject.BroadcastMessage(fun, msg, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
	
	void Update() {
		if (state == State.drawing) {
			DrawUpdate();
			UpdateImageDisplay();
		} else if (state == State.launching) {
			LaunchUpdate();
			UpdateTextDisplay();
		}

		if (Input.GetKeyDown(KeyCode.Space) && state == State.drawing) {
			state = State.launching;

			BroadcastAll ("OnGameStart",null);
		}
	}
	
	void DrawUpdate() {
		if (Input.GetMouseButtonDown(1)) {
			polarityIsPositive = !polarityIsPositive;
		}
		if (Input.GetMouseButtonDown(0)) {
			Vector3 mousePosition = GetMousePosition();
			GameObject newMagnetWell = Instantiate(magnetWellPrefab) as GameObject;
			newMagnetWell.transform.position = mousePosition;
			newMagnetWell.GetComponent<MagnetWell>().isPositive = polarityIsPositive;
			if (polarityIsPositive) {
				newMagnetWell.GetComponent<MeshRenderer>().material.color = new Color(0.7f, 0.7f, 1);
			} else {
				newMagnetWell.GetComponent<MeshRenderer>().material.color = new Color(1, 0.7f, 0.7f);
			}
		}
	}
	
	void LaunchUpdate() {
		if (Input.GetKeyDown (KeyCode.A)) {
			magneticPower = -1f;
		} else if (Input.GetKeyDown (KeyCode.D)) {
			magneticPower = 1f;
		} else if (Input.GetKeyDown (KeyCode.S)) {
			magneticPower = 0f;
		}

		if (Input.GetKeyDown ("space")) {
			magneticPower = -magneticPower;
		}

	}
	
	void UpdateImageDisplay() {
		if (magneticPolarityUIImage == null) {
			magneticPolarityUIImage = GameObject.FindGameObjectWithTag("MagneticPolarityIcon").GetComponent<Image>();
			if (magneticPolarityUIImage == null) {
				return;
			}
			magneticPolarityUIText = magneticPolarityUIImage.GetComponentInChildren<Text>();
		}
		if (polarityIsPositive) {
			magneticPolarityUIImage.color = Color.blue;
			magneticPolarityUIText.text = "+";
		} else {
			magneticPolarityUIImage.color = Color.red;
			magneticPolarityUIText.text = "-";
		}
	}
	
	void UpdateTextDisplay() {
		if (magneticStrengthUIText == null) {
			magneticStrengthUIText = GameObject.FindGameObjectWithTag("MagneticConstantDisplay").GetComponent<Text>();
			if (magneticStrengthUIText == null) {
				return	;
			}	
		}
		magneticStrengthUIText.text = magneticPower.ToString("F4");
	}
	
	public static Vector3 GetMousePosition() {
		float zDisplacement = -Camera.main.transform.position.z;
		Vector3 mousePos = Input.mousePosition;
		return Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, zDisplacement));
	}
}
