using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearLiftA : MonoBehaviour
{
    public Transform _thePlatform,_initialPoint,_finalPoint;
    public float _moveSpeed=0.05f;
    private float _curXpos;
    public bool _canMoveLeftRight=false;
    private bool _timeToGoLeft,_timeToGoRight=false;

    // Start is called before the first frame update
    void Start()
    {
        _thePlatform.position=_initialPoint.position;
        _curXpos=_initialPoint.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(_canMoveLeftRight)
        {
            if(_thePlatform.position.x>=_initialPoint.position.x)
            {
                _timeToGoRight=false;
                _timeToGoLeft=true;
            }
            else if(_thePlatform.position.x<=_finalPoint.position.x)
            {
                _timeToGoLeft=false;
                _timeToGoRight=true;
            }

            if(_timeToGoLeft)
            {
                _curXpos-=_moveSpeed;
            }
            else if(_timeToGoRight)
            {
                _curXpos+=_moveSpeed;
            }
            _thePlatform.position=new Vector2(_curXpos,_thePlatform.position.y);
        }
    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        if(!_col.gameObject.CompareTag("Destroyable"))
        {
            _canMoveLeftRight=true;
        }
    }
}
