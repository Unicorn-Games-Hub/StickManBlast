using UnityEngine;

public class RightSpikeWheel : MonoBehaviour
{
    public bool _isStarted=false;
    public float _speed;
    public Transform _initialPoint,_finalPoint;
    private float _curXpos;

  
    void Start()
    {
        transform.position=_initialPoint.position;
        _curXpos=transform.position.x;
    }

    void Update()
    {
        if(_isStarted)
        {
            if(transform.position.x<_finalPoint.position.x)
            {
                _curXpos+=_speed;
                transform.position=new Vector2(_curXpos,transform.position.y);
            }
        }
    }
}
