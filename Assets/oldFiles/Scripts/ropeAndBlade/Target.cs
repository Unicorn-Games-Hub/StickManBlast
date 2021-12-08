using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public List<Rigidbody2D> _rigidBody=new List<Rigidbody2D>();

    public int _noOfAnchorPoints;

    private int _bodyCounter=0;
  
  
    private Vector2 _connectedAnchor=new Vector2(-0.1f,-0.1f);


   public void AddTheHingeJoint()
   {
     this.gameObject.AddComponent<HingeJoint2D>();
   }
   
   void IdentifyingTheJoints()
   {
     Component[] _joint2D;
     _joint2D=GetComponents(typeof(HingeJoint2D));
     foreach (HingeJoint2D _hinge in _joint2D)
     {
         _hinge.autoConfigureConnectedAnchor=false;
         _hinge.connectedBody=_rigidBody[_bodyCounter];
         _hinge.connectedAnchor=_connectedAnchor;
         _bodyCounter++;
     }
   }


   public void RigidbodyToConnect(Rigidbody2D _targetBody)
   {
       _rigidBody.Add(_targetBody);
       if(_rigidBody.Count==_noOfAnchorPoints)
       {
           IdentifyingTheJoints();
       }
   }

}
