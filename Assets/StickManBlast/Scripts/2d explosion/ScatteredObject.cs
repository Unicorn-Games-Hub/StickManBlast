using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatteredObject : MonoBehaviour
{
    private float _theta;
    private float _dirX,_dirY;

    private float _velocity=30f;
    

    // Start is called before the first frame update
    public void StartTheExposion()
    {
        _theta=transform.localEulerAngles.z+90f;
        _dirX=Mathf.Cos(_theta*Mathf.Deg2Rad);
        _dirY=Mathf.Sin(_theta*Mathf.Deg2Rad);
        GetComponent<Rigidbody2D>().velocity=new Vector2(_dirX,_dirY)*_velocity;
    }

}
