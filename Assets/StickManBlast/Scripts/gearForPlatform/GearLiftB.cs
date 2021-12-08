using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearLiftB : MonoBehaviour
{
    public Transform _thePlatform,_initialPoint,_finalPoint;
    public float _moveSpeed=0.05f;
    private float _curXpos;
    public bool _canMoveLeftRight=false;
    private bool _timeToGoLeft,_timeToGoRight,_removeExtraPlatform=false;

    public Transform _theExtraPlatform,_pointA,_pointB;
    private float _YposforExtra;


    // Start is called before the first frame update
    void Start()
    {
        _thePlatform.position=_finalPoint.position;
        _curXpos=_finalPoint.position.x;

        //
        _theExtraPlatform.position=_pointA.position;
        _YposforExtra=_theExtraPlatform.position.y;
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

        if(_removeExtraPlatform)
        {
            if(_theExtraPlatform.position.y>=_pointB.position.y)
            {
                _YposforExtra-=0.01f;
                _theExtraPlatform.position=new Vector2(_theExtraPlatform.position.x,_YposforExtra);
            }
            else
            {
                _removeExtraPlatform=false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        if(!_col.gameObject.CompareTag("Destroyable"))
        {
            _canMoveLeftRight=true;
            _removeExtraPlatform=true;
        }
    }
}
