using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheRopeManager : MonoBehaviour
{
  public GameObject _theRopePrefab;
  public int _noOfRopeSegments;
  private Rigidbody2D _characterHead;

  public GameObject _theJointPrefab;

  void Start()
  {
      _characterHead=GameObject.FindGameObjectWithTag("HeadAnchor").GetComponent<Rigidbody2D>();
      LetsCreateSomeRope();
  }

  void LetsCreateSomeRope()
  {
      for(int i=0;i<_noOfRopeSegments;i++)
      {
          GameObject _newRopePoints=Instantiate(_theRopePrefab)as GameObject;
          _newRopePoints.transform.SetParent(this.transform);
          _newRopePoints.transform.position= new Vector2(transform.position.x,transform.position.y);
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
            transform.GetChild(i).GetComponent<HingeJoint2D>().connectedBody=transform.GetChild(i-1).gameObject.GetComponent<Rigidbody2D>();


            transform.GetChild(i).GetComponent<TheRopeLine>()._previousPoint=transform.GetChild(i-1).transform;
             transform.GetChild(i).GetComponent<HingeJoint2D>().connectedAnchor=new Vector2(0f,-0.25f);

               transform.GetChild(i).GetComponent<DistanceJoint2D>().connectedBody=transform.GetChild(i-1).gameObject.GetComponent<Rigidbody2D>();
               transform.GetChild(i).GetComponent<DistanceJoint2D>().connectedAnchor=new Vector2(0f,-0.25f);
          }
          else
          {
             transform.GetChild(i).GetComponent<HingeJoint2D>().connectedBody=transform.gameObject.GetComponent<Rigidbody2D>();
              transform.GetChild(i).GetComponent<TheRopeLine>()._previousPoint=this.transform;
             transform.GetChild(i).GetComponent<HingeJoint2D>().connectedAnchor=new Vector2(0f,-0.05f);
              transform.GetChild(i).GetComponent<DistanceJoint2D>().connectedBody=transform.gameObject.GetComponent<Rigidbody2D>();
          }
          transform.GetChild(i).GetComponent<TheRopeLine>()._theParentAnchor=this.transform;
      }
      GameObject _theLastJoint=Instantiate(_theJointPrefab)as GameObject;
      _theLastJoint.transform.SetParent(transform.GetChild(transform.childCount-1).transform);

      _theLastJoint.GetComponent<FixedJoint2D>().connectedBody=_characterHead;
     _theLastJoint.GetComponent<DistanceJoint2D>().connectedBody=transform.GetChild(transform.childCount-1).GetComponent<Rigidbody2D>();
  }

  private JointAngleLimits2D _jointAngle;

  IEnumerator WaitFewSeconds()
  {
    yield return new WaitForSeconds(0.1f);
         Component[] _joints2D;
        _joints2D=transform.GetChild(transform.childCount-1).GetComponents(typeof(HingeJoint2D));

         foreach(HingeJoint2D _hinge in _joints2D)
        {
          _hinge.autoConfigureConnectedAnchor=false;
          _hinge.connectedAnchor=new Vector2(0f,0.1f);
        }
  }


}
