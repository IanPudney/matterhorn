using UnityEngine;
using System.Collections;

public class MagnetWell : MonoBehaviour {
	public GameObject positiveWavePrefab, negativeWavePrefab;
	
	public bool isPositive;
	
	public float mass = 30f;
	private float maxForce = 10f;
	public float timer = 0f;

	
	public CharacterPhysics character;
	
	void Start() {
		if (isPositive) {
			GetComponent<MeshRenderer> ().material.color = Color.blue;
		} else {
			GetComponent<MeshRenderer> ().material.color = Color.red;
		}
	}

	void Update () {
		if (character == null) {
			character = FindObjectOfType<CharacterPhysics>();
			if (character == null) {
				return;
			}
		}
		character.AddWell(this);
		
		GenerateInfluenceBubbles();
	}
	
	float GetDistance() {
		return Vector3.Distance(character.transform.position, transform.position);
	}
	
	public Vector3 GetForce() {
		float distance = Vector3.Distance(character.transform.position, transform.position);
		Vector3 direction = (character.transform.position - transform.position).normalized;
		Vector3 baseForce = direction * mass * StateControl.magneticPower / Mathf.Pow (distance, 2f);
		if (baseForce.magnitude > maxForce) {
			baseForce = baseForce.normalized * maxForce;
		}
		if (isPositive) {
			return -baseForce;
		} else {
			return baseForce;
		}
	}
	
	void GenerateInfluenceBubbles() {
		timer += Time.deltaTime;
		float power = Mathf.Abs (StateControl.magneticPower);
		if (timer > (0.5f - power)) {
			timer -= 0.5f;
			GameObject magnetWave;
			if ((StateControl.magneticPower > 0f) != (isPositive)) {	//XOR
				magnetWave = Instantiate(positiveWavePrefab) as GameObject;
			} else {
				magnetWave = Instantiate(negativeWavePrefab) as GameObject;
			}
			magnetWave.transform.parent = transform;
			magnetWave.transform.localPosition = new Vector3(0f, 10f, 0f);
			magnetWave.GetComponent<MagneticWave>().maxRadius = 1f + 4f * power;
			magnetWave.GetComponent<MagneticWave>().deathTime = 1.5f;
			if (isPositive) {

				magnetWave.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255, 128);
			} else {
				magnetWave.GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0, 128);
			}
		}
	}


}
