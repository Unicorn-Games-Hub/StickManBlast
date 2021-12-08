using UnityEngine;

public class SwitchForSpikeLift : MonoBehaviour
{
   public LeftSpikeWheel _leftWheel;
   public RightSpikeWheel _rightWheel;

   private bool _canTrigger=true;


   void OnTriggerEnter2D(Collider2D _col)
   {
       if(_col.gameObject.CompareTag("Gear")&&_canTrigger)
       {
           _leftWheel._isStarted=true;
           _rightWheel._isStarted=true;
           _canTrigger=false;
       }
   }
}
