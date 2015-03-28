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
			GetComponent<MeshRenderer> ().material.color = Color.black;
			GetComponent<ParticleSystem> ().startColor = Color.blue;
			GetComponent<ParticleSystem> ().maxParticles = 60;
			GetComponent<ParticleSystem> ().emissionRate = 30;
		} else {
			GetComponent<MeshRenderer> ().material.color = Color.black;
			GetComponent<ParticleSystem> ().startColor = new Color(1, 0, 0, 0.2f);
			GetComponent<ParticleSystem> ().maxParticles = 30;
			GetComponent<ParticleSystem> ().emissionRate = 15;
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
		if (timer > 0.65f) {
			timer -= 0.65f;
			if (StateControl.magneticPower == 0f) {
				return;
			}
			GameObject magnetWave;
			if ((StateControl.magneticPower > 0f) != (isPositive)) {	//XOR
				magnetWave = Instantiate(positiveWavePrefab) as GameObject;
			} else {
				magnetWave = Instantiate(negativeWavePrefab) as GameObject;
			}
			magnetWave.transform.parent = transform;
			magnetWave.transform.localPosition = Vector3.zero;
			magnetWave.GetComponent<MagneticWave>().maxRadius = 4.5f * power;
			magnetWave.GetComponent<MagneticWave>().deathTime = 2.25f;
			if (isPositive) {
				magnetWave.GetComponent<SpriteRenderer>().material.color = new Color(0, 0, 1f, 0.5f);
			} else {
				magnetWave.GetComponent<SpriteRenderer>().material.color = new Color(1f, 0, 0, 0.5f);
			}
		}
	}


}
