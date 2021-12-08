using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {
	public float  x = 0;
	public float  y = 0;
	public float  z = 0;
	public   float speed =2;


	void Start()
	{
		//StartRotation = transform.localEulerAngles;
	}
	void Update()
	{  
		transform.Rotate(x*speed*Time.smoothDeltaTime,y*speed*Time.smoothDeltaTime,z*speed*Time.smoothDeltaTime);
	}

}
