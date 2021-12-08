using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeactivate : MonoBehaviour {

	public GameObject obj;
	public GameObject myObj;
	void Start () {
		
	}
	

	void Update () {
		if (myObj.activeSelf == false) {
			obj.SetActive (false);
		}
		
	}
}
