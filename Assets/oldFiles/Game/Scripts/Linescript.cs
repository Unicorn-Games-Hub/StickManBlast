using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linescript : MonoBehaviour {

	public Transform ConnectedObj;
	LineRenderer linerend;
	// Use this for initialization
	void Start () {
		linerend = GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		linerend.SetPosition (0,transform.position);
		linerend.SetPosition (1,ConnectedObj.position);
	}
}
