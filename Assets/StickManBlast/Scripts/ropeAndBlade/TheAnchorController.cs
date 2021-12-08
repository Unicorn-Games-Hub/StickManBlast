using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheAnchorController : MonoBehaviour
{
    public TheTarget _target;
  

   void Awake()
   {
       SendInfoToTarget();
   }

   void SendInfoToTarget()
   {
       for(int i=0;i<transform.childCount;i++)
       {
           if(i>0)
           {
               _target.AdjustThePointOfConnection();
           }
       }
       _target._noOfAnchorPoints=transform.childCount;
   }
}
