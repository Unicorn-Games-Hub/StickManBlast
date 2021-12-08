using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_30_Script : MonoBehaviour {

	public GameObject prefab;
	public Transform empty;
	int i =1;

	void Start () {
		StartCoroutine (Instantiation(empty));
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (i <= 5) {
			Invoke ("Instantiation",5);
		}
	}
	IEnumerator Instantiation(Transform empty){

		yield return new WaitForSeconds(1f);
			Instantiate (prefab, empty.position, Quaternion.identity);
			i++;

	}
}
