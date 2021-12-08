using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ExtraRopeHandler : MonoBehaviour
{
    private Rigidbody2D _anchorBody;
    public int _noOfRopeJoints=5;

    public GameObject _theRopePrefab;

    private Rigidbody2D _oldBody;

    public HingeJoint2D _theTarget;

    void Start()
    {
        LetsCreateRope();      
    }

    void  LetsCreateRope()
    {
        _oldBody=GetComponent<Rigidbody2D>();
        for(int i=0;i<_noOfRopeJoints;i++)
        {
            GameObject _newRopePoints=Instantiate(_theRopePrefab)as GameObject;
            _newRopePoints.GetComponent<HingeJoint2D>().connectedBody=_oldBody;
            _oldBody=_newRopePoints.GetComponent<Rigidbody2D>();
            _newRopePoints.transform.SetParent(this.transform);
            _newRopePoints.GetComponent<HingeJoint2D>().autoConfigureConnectedAnchor=false;
        }
        NowAdjustTheRope();
    }

    void NowAdjustTheRope()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            if(i>0)
            {
                transform.GetChild(i).GetComponent<TheRopeLine>()._previousPoint=transform.GetChild(i-1).transform;
                transform.GetChild(i).GetComponent<HingeJoint2D>().connectedAnchor=new Vector2(0f,-0.25f);
            }
            else
            {
                transform.GetChild(i).GetComponent<TheRopeLine>()._previousPoint=this.transform;
                transform.GetChild(i).GetComponent<HingeJoint2D>().connectedAnchor=new Vector2(0f,-0.1f);
            }
            transform.GetChild(i).GetComponent<TheRopeLine>()._theParentAnchor=this.transform;
        }
        _theTarget.connectedBody=transform.GetChild(transform.childCount-1).GetComponent<Rigidbody2D>();
        _theTarget.autoConfigureConnectedAnchor=false;
        _theTarget.connectedAnchor=new Vector2(0f,-0.1f);
    }

}
