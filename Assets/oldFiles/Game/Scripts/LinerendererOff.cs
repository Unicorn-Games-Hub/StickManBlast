using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinerendererOff : MonoBehaviour {

	public GameObject obj;
	void Start () {
		
	}
	

	void Update () {
		if (obj.activeSelf == false) {
			gameObject.GetComponent<Linescript> ().enabled = false;
			gameObject.GetComponent<LineRenderer> ().enabled = false;
		}
		
	}
}
