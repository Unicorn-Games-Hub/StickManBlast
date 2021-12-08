using UnityEngine;

public class CharLiftSwitch : MonoBehaviour
{
   public Transform _charCarrier;

   void OnTriggerEnter2D(Collider2D _col)
   {
       if(_col.gameObject.CompareTag("Destroyable")||_col.gameObject.CompareTag("SwitchWheel"))
       {
           _charCarrier.GetComponent<CharacterCarrier>()._liftStarted=true;
       }
   }
}
