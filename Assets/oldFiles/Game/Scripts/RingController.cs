using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingController : MonoBehaviour {

	public Rigidbody2D rb;
	public Vector3 direction;

	void Start () {
		rb = FindObjectOfType<Rigidbody2D> ();
	}
	

	void FixedUpdate () {
		rb.AddForce (direction * 100);
		//rb.AddForce(2, 0, ForceMode.Impulse);
	}

}
