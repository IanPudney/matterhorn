using UnityEngine;
using System.Collections;

public class CollisionMessenger : MonoBehaviour {
	
	public GameObject target1;
	public GameObject target2;
	public GameObject recipient;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject == target1 || collision.gameObject == target2) {
			recipient.SendMessage("ReverseDirection");
		}
	}
}
