using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeperateRope : MonoBehaviour
{
    public GameObject _theRopePrefab;
    public GameObject _theLastJointPrefab;
    public int _noOfRopeSegments;
    private Rigidbody2D _previousBody;

    public Rigidbody2D _connectedTo;


void Start()
{
    LetsCreateSomeRope();
}

void LetsCreateSomeRope()
{
    _previousBody=GetComponent<Rigidbody2D>();

    for(int i=0;i<_noOfRopeSegments;i++)
    {
        GameObject _newRopePoints=Instantiate(_theRopePrefab)as GameObject;
        _newRopePoints.transform.position=new Vector2(transform.position.x,transform.position.y);
        _newRopePoints.GetComponent<HingeJoint2D>().connectedBody=_previousBody;
        _previousBody=_newRopePoints.GetComponent<Rigidbody2D>();
        _newRopePoints.transform.SetParent(this.transform);
        _newRopePoints.GetComponent<HingeJoint2D>().autoConfigureConnectedAnchor=false;
    }
    ReAdjustTheRope();
}

    void ReAdjustTheRope()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            if(i>0)
            {
                transform.GetChild(i).GetComponent<TheRopeLine>()._previousPoint=transform.GetChild(i-1).transform;
                transform.GetChild(i).GetComponent<HingeJoint2D>().connectedAnchor=new Vector2(0f,-0.25f);
                transform.GetChild(i).GetComponent<DistanceJoint2D>().connectedBody=transform.GetChild(i-1).gameObject.GetComponent<Rigidbody2D>();
                transform.GetChild(i).GetComponent<DistanceJoint2D>().connectedAnchor=new Vector2(0f,-0.25f);
            }
            else
            {
                transform.GetChild(i).GetComponent<TheRopeLine>()._previousPoint=this.transform;
                transform.GetChild(i).GetComponent<HingeJoint2D>().connectedAnchor=new Vector2(0f,0f);
                transform.GetChild(i).GetComponent<DistanceJoint2D>().connectedBody=transform.gameObject.GetComponent<Rigidbody2D>();
            }
            transform.GetChild(i).GetComponent<TheRopeLine>()._theParentAnchor=this.transform;
        }
        GameObject _newJoint=Instantiate(_theLastJointPrefab)as GameObject;
        _newJoint.transform.SetParent(transform.GetChild(transform.childCount-1).transform);
        _newJoint.GetComponent<FixedJoint2D>().connectedBody=_connectedTo;
        _newJoint.GetComponent<DistanceJoint2D>().connectedBody=transform.GetChild(transform.childCount-1).GetComponent<Rigidbody2D>();
          _newJoint.GetComponent<Rigidbody2D>().mass=2f;
        _newJoint.GetComponent<Rigidbody2D>().gravityScale=2f;
    }
}
