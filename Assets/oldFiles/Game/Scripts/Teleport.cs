using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	public Transform target;
	public string otherTargets;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D incoming){

		if (incoming.tag.Contains (otherTargets)) {
			transform.position = target.position;
		
		}
	}

		
}
