using UnityEngine;

public class VerticalLift : MonoBehaviour
{
    public Transform _pointA,_pointB;
    public float _liftSpeed;
    
    private float _curLiftYpos;

    public bool _switchedOn,_oneWayLift=false;
    
    private bool _goUp,_goDown=false;
 
    void Start()
    {
        transform.position=_pointA.position;
        _curLiftYpos=transform.position.y;
    }
   

    // Update is called once per frame
    void Update()
    {
        if(_switchedOn)
        {
             if(transform.position.y>=_pointA.position.y)
            {
                _goDown=true;
                 _goUp=false;
            }
            else if(transform.position.y<=_pointB.position.y)
            {
                if(!_oneWayLift)
                {
                    _goUp=true;
                }
                _goDown=false;
            }

            if(_goDown)
            {
                _curLiftYpos-=_liftSpeed;
            }
            else if(_goUp)
            {
                _curLiftYpos+=_liftSpeed;
            }
            transform.position=new Vector2(transform.position.x,_curLiftYpos);
        }
    }
}
