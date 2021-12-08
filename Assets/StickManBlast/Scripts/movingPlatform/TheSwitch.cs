using UnityEngine;

public class TheSwitch : MonoBehaviour
{
   public MovingPlatform _thePlatform;

   void OnTriggerEnter2D(Collider2D _col)
   {
       if(_col.CompareTag(""))
       {
           _thePlatform.enabled=false;
       }
   }
}
