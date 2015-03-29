using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class StateControl : MonoBehaviour {
	public static StateControl main;
	[HideInInspector]
	public int currentRoom;

	public enum State {
		drawing,
		launching,
		gameover
	};
	public static State state;

	public static float magneticPower;
	public float magneticPowerStart;
	public Text magneticDisplay;

	public GameObject magnetWellPrefab;
	public static bool polarityIsPositive;

	public Image magneticStrengthUIImage;
	public Text magneticPolarityUIText;
	public Image magneticPolarityUIImage;
	
	public MagneticNodeCounter magneticNodeCounter;
	public static bool levelWon = false;
	
	void Start () {
		main = this;
		magneticPower = magneticPowerStart;
		state = State.drawing;
		levelWon = false;
		PrintBestScore();
	}

	public static void BroadcastAll(string fun, System.Object msg) {
		GameObject[] gos = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
		foreach (GameObject go in gos) {
			if (go) {
				go.gameObject.BroadcastMessage(fun, msg, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
	
	void Update() {
		if (state == State.drawing) {
			DrawUpdate();
		} else if (state == State.launching) {
			LaunchUpdate();
			UpdateTextDisplay();
		}

		if (Input.GetKeyDown(KeyCode.Space) && state == State.drawing) {
			BroadcastAll ("BackupState",null);

			state = State.launching;

			BroadcastAll ("OnGameStart",null);
			ToggleMusic();
		}

		if (Input.GetKeyDown(KeyCode.P)) {
			BroadcastAll ("RestoreState", null);
		}
	}

	void RestoreState() {
		ToggleMusic();
		Start ();
	}
	
	void DrawUpdate() {
		if (magneticNodeCounter == null) {
			magneticNodeCounter = FindObjectOfType<MagneticNodeCounter>();
			if (magneticNodeCounter == null) {
				return;
			}
		}
		if (Input.GetMouseButtonDown(0)) {
			DrawClick(true);
		} else if (Input.GetMouseButtonDown(1)) {
			DrawClick(false);	
		}
	}
	
	void DrawClick(bool leftClick) {
		Vector3 mousePosition = GetMousePosition();
		foreach (MagnetWell well in FindObjectsOfType<MagnetWell>()) {
			if (Vector3.Distance(well.transform.position, mousePosition) < 1f) {
				well.ClickedOn(leftClick);
				return;
			}
		}
		if (!magneticNodeCounter.addNode()) {
			return;
		}
		GameObject newMagnetWell = Instantiate(magnetWellPrefab) as GameObject;
		newMagnetWell.transform.position = mousePosition;
		newMagnetWell.GetComponent<MagnetWell>().isPositive = !leftClick;
			//Makes sure it is opposite so that initialization happens properly
		newMagnetWell.GetComponent<MagnetWell>().ClickedOn(leftClick);
		GameObject.Find ("Character").GetComponent<CharacterPhysics> ().AddWell(newMagnetWell.GetComponent<MagnetWell>());
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
	
	void UpdateTextDisplay() {
		if (magneticStrengthUIImage == null) {
			magneticStrengthUIImage = GameObject.FindGameObjectWithTag("MagneticConstantDisplay").GetComponent<Image>();
			if (magneticStrengthUIImage == null) {
				return	;
			}	
		}
		if (magneticPower > 0.1f) {
			magneticStrengthUIImage.GetComponentInChildren<Text>().text = "+";
			magneticStrengthUIImage.color = Color.blue;
		} else if (magneticPower < -0.1f) {
			magneticStrengthUIImage.GetComponentInChildren<Text>().text = "-";
			magneticStrengthUIImage.color = Color.red;
		} else {
			magneticStrengthUIImage.GetComponentInChildren<Text>().text = "0";
			magneticStrengthUIImage.color = Color.gray;
		}
	}
	
	public static Vector3 GetMousePosition() {
		float zDisplacement = -Camera.main.transform.position.z;
		Vector3 mousePos = Input.mousePosition;
		return Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, zDisplacement));
	}

	public void EndLoss() {
		if (state != State.gameover) {
			state = State.gameover;
			levelWon = false;
			GameObject screen = (GameObject) Instantiate (Resources.Load("Prefabs/GameOverCanvas"));
			screen.BroadcastMessage("GameLoss");
		}
	}

	public void EndWin() {
		if (state != State.gameover) {
			state = State.gameover;
			levelWon = true;
			GameObject screen = (GameObject) Instantiate (Resources.Load("Prefabs/GameOverCanvas"));
			screen.BroadcastMessage("GameWin");
			PrintBestScore();
		}
	}

	void PrintBestScore() {
		string regex = Regex.Match(Application.loadedLevelName, @"\d+").Value;
		if (string.Compare(regex, "") != 0) {
			currentRoom = int.Parse(regex);
			GameObject.FindGameObjectWithTag("LevelText").GetComponent<Text>().text = "Level " + regex;
			int record = PlayerPrefs.GetInt(regex);
			if (record == 0) {
				GameObject.FindGameObjectWithTag("RecordText").GetComponent<Text>().text
					= "Not yet beaten";
			} else {
				GameObject.FindGameObjectWithTag("RecordText").GetComponent<Text>().text
					= "Best: " + record.ToString() + " Nodes";
			}			
		} else {
			GameObject.FindGameObjectWithTag("LevelText").GetComponent<Text>().text = "Custom Level";
			GameObject.FindGameObjectWithTag("RecordText").SetActive(false);
		}
	}
	
	void ToggleMusic() {
		AudioSource[] sources = GetComponents<AudioSource>();
		foreach (AudioSource source in sources) {
			source.mute = !source.mute;
		}
	}
}
