using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class TheTarget : MonoBehaviour
{
    public List<Rigidbody2D> _therbd2D=new List<Rigidbody2D>();
    public int _noOfAnchorPoints;
    private int _rbdCounter=0;
    
    public void AdjustThePointOfConnection()
    {
        this.gameObject.AddComponent<HingeJoint2D>();
    }


    private JointAngleLimits2D _angle2D;

    IEnumerator IdentifyingThePoints()
    {
        yield return new WaitForSeconds(0.1f);
    
        Component[] _joints2D;
        _joints2D=GetComponents(typeof(HingeJoint2D));
        foreach(HingeJoint2D _hinge in _joints2D)
        {
            _hinge.autoConfigureConnectedAnchor=false;
            _hinge.connectedAnchor=new Vector2(0f,0f);
            /* 
            _hinge.useLimits=true;
            _angle2D=_hinge.limits;
            _angle2D.min=-10f;
            _angle2D.max=20f;
            _hinge.limits=_angle2D;
           */
    
            if(_hinge.connectedBody==null)
            {
                _hinge.connectedBody=_therbd2D[_rbdCounter];
            }
            _rbdCounter++;
        }
      
    }

    public void FillTheRigidbodyList(Rigidbody2D _passedRbd2D)
    {
        _therbd2D.Add(_passedRbd2D);
        if(_therbd2D.Count==_noOfAnchorPoints)
        {
          StartCoroutine(IdentifyingThePoints());
        }
    }

}
