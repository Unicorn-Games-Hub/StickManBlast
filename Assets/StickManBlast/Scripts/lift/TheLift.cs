using UnityEngine;

public class TheLift : MonoBehaviour
{
    public Transform _pointA,_pointB;
   private float _liftSpeed= 0.05f;
    public bool _liftStarted=false;

    private bool _moveUp,_moveDown=false;

    private float _curYpos;
    // Start is called before the first frame update
    void Start()
    {
        transform.position=_pointB.position;
        _curYpos=transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(_liftStarted)
        {
            if(transform.position.y>=_pointA.position.y)
            {
                _moveUp=false;
                _moveDown=true;
            }
            else if(transform.position.y<=_pointB.position.y)
            {
                _moveDown=false;
                _moveUp=true;
            }

            if(_moveUp)
            {
                _curYpos+=_liftSpeed;
            }
            else if(_moveDown)
            {
                _curYpos-=_liftSpeed;
            }
            transform.position=new Vector2(transform.position.x,_curYpos);
        }
     
    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col!=null)
        {
            _col.gameObject.transform.SetParent(this.transform);
        }
    }
}
