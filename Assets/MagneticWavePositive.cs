﻿using UnityEngine;
using System.Collections;

public class MagneticWavePositive : MagneticWave {
	void Start() {
		GetComponent<MeshRenderer>().material.color = Color.blue;
	}

	public override void Adjust (float ratio) {
		Color color = GetComponent<MeshRenderer>().material.color;
		GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, (1f - ratio));
		transform.localScale = Vector3.one * ratio * maxRadius;
	}
}
