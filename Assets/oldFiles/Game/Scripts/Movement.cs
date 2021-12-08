using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	public int speed;
	bool b;

	void Start () {
		
	}
	

	void Update () {
		if(b == false){
		transform.Translate(Vector3.right * Time.deltaTime*speed);
	}
		}
	void OnCollisionEnter2D(Collision2D other){
		if (other.collider.tag.Contains ("ring")) {
			b = true;
		} else {
			b = false;
		}
		
	}
}
