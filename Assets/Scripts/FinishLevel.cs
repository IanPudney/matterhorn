using UnityEngine;
using System.Collections;

public class FinishLevel : MonoBehaviour {
	public int levelNumber, numNodes;
	
	//
	float timer = 0f;
	
	void Update () {
		timer += Time.deltaTime	;
		if (timer >= 4f) {
			Scorekeeper.UpdateScores(levelNumber, numNodes);
			Application.LoadLevel("_TitleScene");
		}
	}
}
