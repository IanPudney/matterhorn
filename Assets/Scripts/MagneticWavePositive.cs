using UnityEngine;
using System.Collections;

public class MagneticWavePositive : MagneticWave {
	public override void Adjust (float ratio) {
		Color color = GetComponent<SpriteRenderer>().material.color;
		GetComponent<SpriteRenderer>().material.color = new Color(color.r, color.g, color.b, 0.5f * (1f - ratio));
		transform.localScale = Vector3.one * ratio * maxRadius;
	}
}
