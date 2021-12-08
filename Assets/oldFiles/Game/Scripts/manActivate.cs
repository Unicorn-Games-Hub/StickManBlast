using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManActivate : MonoBehaviour {

	public GameObject Man;
	public GameObject Obj;
	void Start () {
		Invoke ("Activate",2);
	}

	// Update is called once per frame
	void Update () {

	}
	void Acvtivate(){
		Man.SetActive (true);
		Obj.SetActive (true);
	}
}