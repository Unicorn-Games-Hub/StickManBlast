using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineScript : MonoBehaviour {
	public GameObject obj;
	public GameObject myobj;

	void Start () {
		
	}
	

	void Update () {
		if (obj.activeSelf == false) {
			myobj.GetComponent<Linescript> ().enabled = false;
			myobj.GetComponent<LineRenderer> ().enabled = false;
		}
		
	}
}
