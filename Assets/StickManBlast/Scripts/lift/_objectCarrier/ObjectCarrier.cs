using UnityEngine;

public class ObjectCarrier : MonoBehaviour
{ 
    
    public Transform _pointA,_pointB;
    public float _liftSpeed;
    private float _curXpos;
    private bool _goLeft,_goRight=false;

    public bool _liftStarted=false;
    public GameObject _destroyParticle;

    void Start()
    {
        transform.position=_pointB.position;
        _curXpos=transform.position.x;
     
    }

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

         if(Input.GetMouseButtonDown(0))
        {
            Vector2 _curmousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D _hit2D=Physics2D.Raycast(_curmousePos,Vector2.zero);
            if(_hit2D.collider!=null)
            {
                if(_hit2D.collider.gameObject.CompareTag("ObjCarrier"))
                {
                    GameObject _theParticle=Instantiate(_destroyParticle) as GameObject;
                    _theParticle.transform.position=_curmousePos;
                    _theParticle.GetComponent<ParticleSystem>().Play();
                    FreeAllTheRestriction();
                }
            }
        }
    }

    void FreeAllTheRestriction()
    {
        if(transform.childCount!=0)
        {
            for(int i=0;i<transform.childCount;i++)
            {
                transform.GetChild(i).transform.parent=null;
            }
            this.gameObject.SetActive(false);
        }
    }



    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col.gameObject.CompareTag("Destroyable")||_col.gameObject.CompareTag("Carate"))
        {
            _col.gameObject.transform.SetParent(this.transform);
        }
    }
}
