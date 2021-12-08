using UnityEngine;

public class TheAnchorHandler : MonoBehaviour
{
    public Target _theTarget;

   void Awake()
   {
       for(int i=0;i<transform.childCount;i++)
       {
           if(i>0)
           {
                _theTarget.AddTheHingeJoint();
           }
       }
       _theTarget._noOfAnchorPoints=transform.childCount;
   }
}
