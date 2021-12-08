using UnityEngine;

public class DirectionalWheel : MonoBehaviour
{
   public bool _goLeft,_goRight=false;
   public Transform _pointA,_pointB;
   private Vector2 _curPosition;
   private float _curXpos;

   public float _moveSpeed;

   void Start()
   {
       if(_goLeft)
       {
           transform.position=new Vector2(_pointA.position.x,transform.position.y);
           _goRight=false;
       }
       else if(_goRight)
       {
            transform.position=new Vector2(_pointB.position.x,transform.position.y);
           _goLeft=false;
       }
        _curXpos=transform.position.x;
   }

    void Update()
    {
        if(_goLeft)
        {
            if(transform.position.x>_pointB.position.x)
            {
                _curXpos-=_moveSpeed;
            }
            transform.position=new Vector2(_curXpos,transform.position.y);
        }
        if(_goRight)
        {
            if(transform.position.x<_pointA.position.x)
            {
                _curXpos+=_moveSpeed;
            }
            transform.position=new Vector2(_curXpos,transform.position.y);
        }
    }
}
