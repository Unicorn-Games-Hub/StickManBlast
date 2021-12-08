using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {

	Rigidbody2D rig;
	void Start () {
		
	}

	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D hit){

		if(hit.collider.tag.Contains("Bounce")){
			//rig.AddForce (5,5);

		}

	}
}
