using UnityEngine;
using System.Collections;

public class MagnetWell : MonoBehaviour {
	public GameObject positiveWavePrefab, negativeWavePrefab;
	public AudioClip creationNoise;
	
	public bool isPositive;
	
	public float mass = 30f;
	private float maxForce = 10f;
	public float timer = 0f;
	
	public bool draggingThis = false;
	public static bool draggingAny = false;
	
	public CharacterPhysics character;
	
	void Start() {
		AudioSource.PlayClipAtPoint(creationNoise,Camera.main.transform.position);
	}
	
	void SetState(bool toPositive) {
		if (toPositive) {
			isPositive = true;
			GetComponent<MeshRenderer> ().material.color = new Color(0.3f, 0.3f, 0.6f);
			GetComponent<ParticleSystem> ().startColor = Color.blue;
			GetComponent<ParticleSystem> ().maxParticles = 60;
			GetComponent<ParticleSystem> ().emissionRate = 30;
		} else {
			isPositive = false;
			GetComponent<MeshRenderer> ().material.color = new Color(0.6f, 0.3f, 0.3f);
			GetComponent<ParticleSystem> ().startColor = new Color(1f, 0, 0, 0.2f);
			GetComponent<ParticleSystem> ().maxParticles = 30;
			GetComponent<ParticleSystem> ().emissionRate = 15;
		}
	}

	void Update () {
		GenerateInfluenceBubbles();
		if(draggingThis) {
			if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1)) {
				ClickReleased();
			}
			transform.position = StateControl.GetMousePosition();
		}
	}
	
	float GetDistance() {
		if (character == null) {
			character = FindObjectOfType<CharacterPhysics>();
			if (character == null) {
				return float.MaxValue;
			}
		}
		return Vector3.Distance(character.transform.position, transform.position);
	}
	
	public Vector3 GetForce() {
		float distance = GetDistance();
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

	public void ClickedOn(bool leftClick) {
		draggingThis = true;
		draggingAny = true;
		SetState (leftClick);
	}
	
	public void ClickReleased() {
		draggingThis = false;
		GetComponent<MeshRenderer>().material.color = Color.black;
		foreach (MagnetWell well in FindObjectsOfType<MagnetWell>()) {
			if (well.draggingThis) {
				return;
			}
		}
		draggingAny = false;
	}
}
