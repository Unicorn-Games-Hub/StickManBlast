using UnityEngine;

public class TriggeredSpikeSwitch : MonoBehaviour
{
    
   private float _switchYpos;
   private bool _switchEnabled=true;

   public GameObject _triggeredSpikeWheel;
  

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
             _triggeredSpikeWheel.GetComponent<TriggeredSpikeWheel>().TimeToGoUp();
             _switchEnabled=false;
        }
    }
}
