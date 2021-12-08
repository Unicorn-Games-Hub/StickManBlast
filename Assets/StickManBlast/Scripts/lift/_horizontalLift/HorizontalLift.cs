using UnityEngine;

public class HorizontalLift : MonoBehaviour
{
    public Transform _pointA,_pointB;
    public float _liftSpeed;
    private float _curXpos;

    public bool _liftStarted=false;

    private bool _goLeft,_goRight=false;
    
  


    // Start is called before the first frame update
    void Start()
    {
        transform.position=_pointB.position;
        _curXpos=transform.position.x;
     
    }

    // Update is called once per frame
    void Update()
    {
        if(_liftStarted)
        {
            if(transform.position.x>=_pointB.position.x)
            {
               _goLeft=true;
               _goRight=false;
            }
            else if(transform.position.x<=_pointA.position.x)
            {
                _goRight=true;
                _goLeft=false;
            }

            if(_goLeft)
            {
                _curXpos-=_liftSpeed;
            }
            else if(_goRight)
            {
                _curXpos+=_liftSpeed;
            }
             transform.position=new Vector2(_curXpos,transform.position.y);
        }
    }

    /*
    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_canI)
        {
            _theHinge.connectedBody= _col.gameObject.transform.parent.transform.parent.gameObject.GetComponent<Rigidbody2D>();
            _theHinge.connectedAnchor=new Vector2(-0.95f,-2.1f);
            _theHinge.anchor=new Vector2(-0.95f,1f);
            _canI=false;
        }
    }
    bool _canI=true;
    */

      void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col.gameObject.CompareTag("Carate"))
        {
            if(GetComponent<FixedJoint2D>()!=null)
            {
                GetComponent<FixedJoint2D>().connectedBody=_col.gameObject.GetComponent<Rigidbody2D>();
                GetComponent<FixedJoint2D>().connectedAnchor=Vector2.zero;
                GetComponent<FixedJoint2D>().anchor=new Vector2(0f,1.9f);
            }
        }
    }
}
