using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide_Rope : MonoBehaviour {
	public string objName;
	public GameObject particles;
	SoundController mySound;

	void Start()
	{
		mySound = FindObjectOfType<SoundController> ();
	}

	void Update()
	{
		
		if (Input.GetMouseButtonDown (0)) 
		{
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (worldPoint, Vector2.zero);
			if (hit.collider == null) {
				return;
			}
			if (hit.collider.name == objName) {
				//particles.SetActive (true);
				hit.collider.gameObject.SetActive (false);
				GameObject _brustParticle;
				_brustParticle =	Instantiate (particles, hit.transform.position, Quaternion.identity) as GameObject;
				_brustParticle.SetActive (true);
				_brustParticle.GetComponent<ParticleSystem> ().Play ();
			//	mySound.tap.Play ();
			}
		}
	}
}