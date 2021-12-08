using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide2 : MonoBehaviour {
	public GameObject objName;

		void Update(){
			if (Input.GetMouseButtonDown (0)) {

				Vector2 worldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				RaycastHit2D hit = Physics2D.Raycast (worldPoint, Vector2.zero);
				if (hit.collider == null) {
					return;
				}
				
				if (hit.collider.tag.Contains ("50")) {
				objName.SetActive (false);
				}
			}


		}






	}