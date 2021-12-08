using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partical_Script : MonoBehaviour {


	public GameObject myObj;
	public GameObject Parti;

	void Start () {
		Parti.SetActive (false);
	}
	

	void Update () {
		if (myObj.activeSelf == false) {
			Parti.SetActive (true);
		}
		
	}
}
