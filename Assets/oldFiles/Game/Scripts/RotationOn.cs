using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationOn : MonoBehaviour {
	public GameObject rg;
	public GameObject rg1;
	public GameObject rg2;
	public GameObject rg3;
	public GameObject rg5;
	public GameObject rg4;
	public GameObject rg6;
	public GameObject rg7;
	public GameObject rg8;
	public GameObject rg9;
	public GameObject g;

	void Start () {
		
	}
	

	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D hit){
		if(hit.collider.tag.Contains("ring") ){
			Debug.Log (".................");
			rg.GetComponent<Rotation> ().enabled = true;
			rg1.GetComponent<Rotation> ().enabled = true;
			rg2.GetComponent<Rotation> ().enabled = true;
			rg3.GetComponent<Rotation> ().enabled = true;
			rg4.GetComponent<Rotation> ().enabled = true;
			rg5.GetComponent<Rotation> ().enabled = true;
			rg6.GetComponent<Rotation> ().enabled = true;
			rg7.GetComponent<Rotation> ().enabled = true;
			rg8.GetComponent<Rotation> ().enabled = true;
			rg9.GetComponent<Rotation> ().enabled = true;
			g.SetActive (false);
			
		}
		
	}
}
