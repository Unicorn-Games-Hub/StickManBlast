using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class TheRopeHandler : MonoBehaviour
{
    public int _ropeLength=3;
    public GameObject _ropePoints;
    private Rigidbody2D _previousAnchor;
    private Rigidbody2D _lastPoint;

    public Target _target;

    // Start is called before the first frame update
    void Start()
    {
        _previousAnchor=GetComponent<Rigidbody2D>();

        for(int i=0;i<_ropeLength;i++)
        {
           GameObject _newRope=Instantiate(_ropePoints)as GameObject;
           _newRope.GetComponent<HingeJoint2D>().connectedBody=_previousAnchor;
           _previousAnchor=_newRope.GetComponent<Rigidbody2D>();
            _newRope.transform.SetParent(this.transform);
        }
        _lastPoint=transform.GetChild(transform.childCount-1).GetComponent<Rigidbody2D>();
        AdjustTheConnectedLink();
    }

    void AdjustTheConnectedLink()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            if(i>0)
            {
                transform.GetChild(i).GetComponent<RopeController>()._connectedTo=transform.GetChild(i-1).transform;
               
            }
            else
            {
                transform.GetChild(i).GetComponent<RopeController>()._connectedTo=this.transform;
              
            }
           // transform.GetChild(i).GetComponent<RopeController>()._mainParent=this.transform;
        }
        _target.RigidbodyToConnect(transform.GetChild(transform.childCount-1).GetComponent<Rigidbody2D>());
    }
}
