using UnityEngine;

public class TheSpikeLift : MonoBehaviour
{
    public Transform _initialPoint,_finalPoint;
    public float _liftSpeed;

    public bool _liftStarted=false;

    private bool _moveLeft,_moveRight=false;

    private float _curXpos;


    void Start()
    {
        _moveLeft=true;
        _curXpos=transform.position.x;
    }

    void Update()
    {
        if(_liftStarted)
        {
            if(transform.position.x<=_initialPoint.position.x)
            {
                _moveRight=true;
                _moveLeft=false;
            }
            else if(transform.position.x>=_finalPoint.position.x)
            {
                _moveLeft=true;
                _moveRight=false;
            }

            if(_moveLeft)
            {
                _curXpos-=_liftSpeed;
            }
            else if(_moveRight)
            {
                _curXpos+=_liftSpeed;
            }

            transform.position=new Vector2(_curXpos,transform.position.y);
        }
    }
}
