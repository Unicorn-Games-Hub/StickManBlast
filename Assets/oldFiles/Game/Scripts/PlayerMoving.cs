using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour {

	public GameObject mainMenu;
	void Start () {
		
	}
	

	void Update () {
		if (mainMenu.activeSelf == true) {
			gameObject.SetActive (true);
		} else {
			gameObject.SetActive (false);
		}
	}
}
