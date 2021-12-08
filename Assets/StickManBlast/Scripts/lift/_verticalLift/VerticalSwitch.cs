using UnityEngine;

public class VerticalSwitch : MonoBehaviour
{
   
    private Vector2 _switchCurPos;
    private float _switchYpos;
    public VerticalLift _verticalLift;
    private bool _canPressSwitch=true;


    // Start is called before the first frame update
    void Start()
    {
        _switchCurPos=transform.position;
        _switchYpos=_switchCurPos.y;
    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col.gameObject.CompareTag("Carate"))
        {
            if(_canPressSwitch)
            {
                _switchYpos-=0.3f;
                _canPressSwitch=false;
            }
             _verticalLift._switchedOn=true;
            transform.position=new Vector2(transform.position.x,_switchYpos);
        }
    }
}
