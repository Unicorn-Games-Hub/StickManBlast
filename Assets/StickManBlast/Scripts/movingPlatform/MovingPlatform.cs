using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool _canMove=false;
    public Transform _pointA,_pointB;
    public Transform _movableObject;
    public float _moveSpeed=2f;
    private float _theDirection;
    private bool _moveLeft,_moveRight=false;

    void Start()
    {
        _movableObject.transform.position=_pointB.position;
    }

    void Update()
    {
        if(_canMove)
        {
            if(_movableObject.transform.position.x>=_pointB.position.x)
            {
                _moveRight=false;
                _moveLeft=true;
            }
            else if(_movableObject.transform.position.x<=_pointA.position.x)
            {
                _moveLeft=false;
                _moveRight=true;
            }
            if(_moveLeft)
            {
                _theDirection-=Time.deltaTime*_moveSpeed;
            }
            if(_moveRight)
            {
                _theDirection+=Time.deltaTime*_moveSpeed;
            }
            _movableObject.transform.position=new Vector2(_theDirection,_movableObject.transform.position.y);
        }
    }
}
