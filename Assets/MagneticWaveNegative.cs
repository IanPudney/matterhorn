﻿using UnityEngine;
using System.Collections;

public class MagneticWaveNegative : MagneticWave {
	public override void Adjust (float ratio) {
		Color color = GetComponent<MeshRenderer>().material.color;
		GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 0.5f * ratio);
		transform.localScale = Vector3.one * (1f - ratio) * maxRadius;
	}
}
