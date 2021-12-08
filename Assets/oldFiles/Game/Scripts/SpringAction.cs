using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringAction : MonoBehaviour {

	public int i = 0;
	public Animator anim;
	public GameObject obj;

	void Start () {
		
	}
	

	void Update () {
		if(i ==1){
			obj.GetComponent<Movement> ().enabled = false;
			anim.SetBool ("springin", false);
			anim.SetBool ("springout", true);
		}
		if (gameObject.transform.localScale.x == 0.2f) {
			obj.GetComponent<Movement> ().enabled = false;
			i = 1;
		}
		
	}
	void OnCollisionEnter2D(Collision2D hit){
		if (hit.collider.tag.Contains ("Player")) {
			anim.SetBool ("springin", true);
		}
	}

}
