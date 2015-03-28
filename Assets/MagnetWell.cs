using UnityEngine;
using System.Collections;

public class MagnetWell : MonoBehaviour {
	public GameObject positiveWavePrefab, negativeWavePrefab;
	
	public bool isPositive;
	
	public float mass;
	
	public float timer = 0f;
	
	public CharacterPhysics character;
	
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
		Vector3 baseForce = direction * mass * StateControl.magneticPower / Mathf.Pow (distance, 2);
		if (isPositive) {
			return baseForce;
		} else {
			return -baseForce;
		}
	}
	
	void GenerateInfluenceBubbles() {
		timer += Time.deltaTime;
		float power = Mathf.Abs (StateControl.magneticPower);
		if (timer > (1.2f - power)) {
			timer -= (1.2f - power);
			GameObject magnetWave;
			if (StateControl.magneticPower > 0f) {
				magnetWave = Instantiate(positiveWavePrefab) as GameObject;
			} else {
				magnetWave = Instantiate(negativeWavePrefab) as GameObject;
			}
			magnetWave.transform.parent = transform;
			magnetWave.transform.localPosition = Vector3.zero;
			magnetWave.GetComponent<MagneticWave>().maxRadius = 1f + 2f * power;
			magnetWave.GetComponent<MagneticWave>().deathTime = (1.2f - power);
			
		}
	}
}
