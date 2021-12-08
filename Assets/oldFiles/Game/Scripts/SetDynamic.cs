using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDynamic : MonoBehaviour {

	public Rigidbody2D rb;
	void Start () {
		
	}
	

	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D hit){
		if (hit.collider.tag== ("ring")) {
			rb.bodyType = RigidbodyType2D.Dynamic;
		} 

	}
}
