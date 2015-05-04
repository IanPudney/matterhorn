using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;
using System;

public class StateControl : MonoBehaviour {
	public static StateControl main;
	GameObject eventSystem;

	public GameObject magnetWellPrefab;
	public GameObject CannonLeverButtonPrefab;
	public GameObject CannonLaunchButtonPrefab;
	public GameObject ParticleLinePrefab;

	static bool _nodePlacementInverted;

	public static bool nodePlacementInverted {
		get {
			return _nodePlacementInverted;
		}
		protected set {
			_nodePlacementInverted = value;
		}
	}
	[HideInInspector]
	public int currentRoom;

	public enum State {
		drawing,
		launching,
		gameover
	};
	public static State state;

	public static float magneticPower;
	[HideInInspector]
	public float magneticPowerStart;
	[HideInInspector]
	public Text magneticDisplay;
	
	[HideInInspector]
	public static bool polarityIsPositive;

	[HideInInspector]
	public Image magneticStrengthUIImage;
	[HideInInspector]
	public Text magneticPolarityUIText;
	[HideInInspector]
	public Image magneticPolarityUIImage;
	
	[HideInInspector]
	public MagneticNodeCounter magneticNodeCounter;
	[HideInInspector]
	public static bool levelWon = false;
	[HideInInspector]
	public string destinationLevel;
	
	void Awake() {
		main = this;
	}
	
	void Start () {
		nodePlacementInverted = false;
		magneticPower = magneticPowerStart;
		state = State.drawing;
		levelWon = false;
		PrintBestScore();

		if (eventSystem == null) {
			eventSystem = GameObject.Find ("EventSystem");
		}

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
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel("_TitleScene");
		}
		if (state == State.drawing) {
			DrawUpdate();
		} else if (state == State.launching) {
			LaunchUpdate();
			UpdateTextDisplay();
		}
		if ((Input.GetKeyDown(KeyCode.Space))) {
			SpaceFn();
		}

		if (Input.GetKeyDown(KeyCode.P)) {
			BroadcastAll ("RestoreState", null);
		}
	}

	void RestoreState() {
		ToggleMusic();
		Start ();
	}
	public void SpaceFn() {
		Debug.Log ("SpaceFn");
		if (state == State.drawing) {

			BroadcastAll ("BackupState", null);
			
			state = State.launching;
			
			BroadcastAll ("OnGameStart", null);
			ToggleMusic ();
		}
	}
	
	void DrawUpdate() {
		if (magneticNodeCounter == null) {
			magneticNodeCounter = FindObjectOfType<MagneticNodeCounter>();
			if (magneticNodeCounter == null) {
				return;
			}
		}
		EventSystem system = EventSystem.current;
		bool touchOutOfBounds = false;
		touchOutOfBounds = system.IsPointerOverGameObject (-1);
		foreach (Touch touch in Input.touches) {
			touchOutOfBounds = touchOutOfBounds || system.IsPointerOverGameObject(touch.fingerId);
		}
		if (!touchOutOfBounds) {
			if (Input.GetMouseButtonDown(0)) {
				DrawClick(!nodePlacementInverted);
			} else if (Input.GetMouseButtonDown(1)) {
				DrawClick(nodePlacementInverted);	
			}
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
		if (UtilityButton.clicked || !magneticNodeCounter.addNode()) {
			return;
		}
		GameObject newMagnetWell = Instantiate(magnetWellPrefab) as GameObject;
		newMagnetWell.transform.position = mousePosition;
		newMagnetWell.GetComponent<MagnetWell>().isPositive = !leftClick;
			//Makes sure it is opposite so that initialization happens properly
		newMagnetWell.GetComponent<MagnetWell>().ClickedOn(leftClick);
	}
	
	void LaunchUpdate() {
		if (Input.GetKeyDown (KeyCode.A)) {
			magneticPower = -1f;
		} else if (Input.GetKeyDown (KeyCode.D)) {
			magneticPower = 1f;
		} else if (Input.GetKeyDown (KeyCode.S)) {
			magneticPower = 0f;
		}

		if (Input.GetKeyDown ("space") || GoButton.goButtonDown) {
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
		if (magneticPower < -0.1f) {
			magneticStrengthUIImage.GetComponentInChildren<Text>().text = "+";
			magneticStrengthUIImage.color = Color.blue;
		} else if (magneticPower > 0.1f) {
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

	public void EndWin(string destinationScene) {
		if (state != State.gameover) {
			state = State.gameover;
			levelWon = true;
			GameObject screen = (GameObject) Instantiate (Resources.Load("Prefabs/GameOverCanvas"));
			screen.BroadcastMessage("GameWin");
			PrintBestScore();
			destinationLevel = destinationScene;
		}
	}

	void PrintBestScore() {
		string regex = Regex.Match(Application.loadedLevelName, @"\d+").Value;
		if (string.Compare (Application.loadedLevelName, "_CustomLevel") == 0) {
			GameObject.FindGameObjectWithTag("LevelText").GetComponent<Text>().text = "Custom Level";
			GameObject.FindGameObjectWithTag("RecordText").GetComponent<Text>().text = "";
			return;
		}
		try {
			currentRoom = int.Parse(regex);
		} catch (FormatException) {
			currentRoom = int.MaxValue;
		}
		GameObject.FindGameObjectWithTag("LevelText").GetComponent<Text>().text = "Level " + regex;
		int record;
		try {
			record = PlayerPrefs.GetInt(regex);
		} catch (FormatException) {
			record = 0;
		}
		if (record == 0) {
				GameObject.FindGameObjectWithTag("RecordText").GetComponent<Text>().text
				= "Not yet beaten";
		} else {
			if (record == 1) {
				GameObject.FindGameObjectWithTag("RecordText").GetComponent<Text>().text
					= "Best: 1 Node";
			} else {
				GameObject.FindGameObjectWithTag("RecordText").GetComponent<Text>().text
					= "Best: " + record.ToString() + " Nodes";
			}
		}
	}
	
	public void ToggleMusic() {
		AudioSource[] sources = GetComponents<AudioSource>();
		foreach (AudioSource source in sources) {
			source.mute = !source.mute;
		}
	}

	static public void InvertNodePlacement() {
		nodePlacementInverted = !nodePlacementInverted;
	}
}
