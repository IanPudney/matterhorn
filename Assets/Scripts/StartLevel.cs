using UnityEngine;
using System.Collections;

public class StartLevel : MonoBehaviour {
	public void Run(string levelName) {
		Application.LoadLevel(levelName);
	}
}
