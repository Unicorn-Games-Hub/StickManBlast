using UnityEngine;

public class MovableAnchorSwitch : MonoBehaviour
{
    private float _switchYpos;
    private bool _switchedOff=true;

    private bool _isSwitchedOn=false;



    public Transform _objToMove,_startingPoint,_endingPoint;

    private float _objXpos;
    public float _moveSpeed=0.02f;

    private bool _moveLeft,_moveRight=false;

    // Start is called before the first frame update
    void Start()
    {
        _switchYpos=transform.position.y;
        _objToMove.transform.position=_startingPoint.position;
        _objXpos=_objToMove.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isSwitchedOn)
        {
            if(_objToMove.position.x<_endingPoint.position.x)
            {
                _objXpos+=_moveSpeed;
            }
            else
            {
                _isSwitchedOn=false;
            }
            _objToMove.position=new Vector2(_objXpos,_objToMove.position.y);

        }
        
    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col.gameObject.CompareTag("Carate")&&_switchedOff)
        {
            _switchYpos-=0.2f;
            transform.position=new Vector2(transform.position.x,_switchYpos);
            _isSwitchedOn=true;
            _switchedOff=false;
        }
    }
}
