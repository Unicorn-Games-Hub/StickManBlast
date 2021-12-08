using UnityEngine;

public class OneTimeLift : MonoBehaviour
{
   public Transform _initialPoint,_finalPoint;
   public float _speed;
   public bool _canMove=false;
   private bool _hasLoad=false;

   private float _curPos;

    void Start()
    {
        transform.position=_initialPoint.position;
        _curPos=transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(_canMove)
        {
              if(_curPos>_finalPoint.position.y)
            {
                if(_hasLoad)
                {
                    _curPos-=_speed*2f;
                   transform.position=new Vector2(transform.position.x,_curPos);
                }
                else
                {
                    _curPos-=_speed;
                   transform.position=new Vector2(transform.position.x,_curPos);
                }
            }
        }
    }

    public void CheckTheMovingCondition()
    {
        _canMove=true;
    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col.collider!=null)
        {
            _hasLoad=true;
        }
    }
}
