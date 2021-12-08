using UnityEngine;

public class ObjCarrierSwitch : MonoBehaviour
{

    private float _curYpos;
    private bool _canEnableSwitch=true;

    public VerticalLift _theLift;
  
    void Start()
    {
        _curYpos=transform.position.y;
        
    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col.gameObject.CompareTag("Carate")&&_canEnableSwitch)
        {
            _curYpos-=0.2f;
            transform.position=new Vector2(transform.position.x,_curYpos);
           _theLift._switchedOn=true;
            _canEnableSwitch=false;
        }
    }
}
