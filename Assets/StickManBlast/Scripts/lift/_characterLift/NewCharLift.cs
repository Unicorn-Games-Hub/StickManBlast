using UnityEngine;

public class NewCharLift : MonoBehaviour
{
    public Transform _theCharCarrier;
    private bool _canActivateSwitch=true;

    private Vector2 _curPos;
    private float _switchYpos;

  
    void Start()
    {
        _curPos=transform.position;
        _switchYpos=_curPos.y;
    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col.gameObject.CompareTag("Destroyable")&&_canActivateSwitch)
        {
            moveSwitchDown();
            _theCharCarrier.gameObject.GetComponent<CharacterCarrier>()._liftStarted=true;
            _canActivateSwitch=false;
        }
    }

    void moveSwitchDown()
    {
        _switchYpos-=0.2f;
        transform.position=new Vector2(transform.position.x,_switchYpos);
    }

}
