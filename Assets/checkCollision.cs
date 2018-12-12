using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkCollision : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter (Collider other) {
		Debug.Log ("detect");
	}
}
