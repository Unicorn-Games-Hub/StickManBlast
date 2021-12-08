using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsOff40 : MonoBehaviour {
	public GameObject obj;
	public GameObject obj1;
	public GameObject obj2;
	public GameObject obj3;
	public Rigidbody2D rb;
	void Start () {
		
	}
	

	void Update () {
		if (obj.activeSelf == false) {
			rb.mass = rb.mass - 1;
		}
	}

}
