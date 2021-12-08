using UnityEngine;

public class TheWheelSwitch : MonoBehaviour
{
   public GameObject _movingWheel;

   private float _switchYpos;
   private bool _switchEnabled=true;

   void Start()
   {
       _switchYpos=transform.position.y;
   }

   void OnCollisionEnter2D(Collision2D _col)
   {
       if(_col.gameObject.CompareTag("Carate")&&_switchEnabled)
       {
           _switchYpos-=0.2f;
           transform.position=new Vector2(transform.position.x,_switchYpos);
           _movingWheel.GetComponent<MovingWheelHandler>().MoveWheelNow();
            _switchEnabled=false;
       }
   }
}
