using UnityEngine;
using System.Collections;

public class MagneticWave : MonoBehaviour {
	public float maxRadius;
	public float lifetime = 0;
	public float deathTime;
	
	void Update () {
		lifetime += Time.deltaTime;
		if (lifetime > deathTime) {
			Destroy(gameObject);
		}
		float ratio = lifetime / deathTime;
		Adjust (ratio);
	}
	
	public virtual void Adjust(float ratio) {
		
	}
}
