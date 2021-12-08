using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCarrier : MonoBehaviour
{
    private bool _canDetect=true;
    private Rigidbody2D _charRigidbody;
    public GameObject _destroyParticle;

    public Transform _pointA,_pointB;
    public float _liftSpeed;
    private float _curXpos;
    public bool _liftStarted=false;
    private bool _goLeft,_goRight=false;
    
    void Start()
    {
        transform.position=_pointA.position;
        _curXpos=transform.position.x;
        this.gameObject.tag="Destroyable";
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 _curmousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D _hit2D=Physics2D.Raycast(_curmousePos,Vector2.zero);
            if(_hit2D.collider!=null)
            {
                if(_hit2D.collider.gameObject.CompareTag("CharacterCarrier"))
                {
                    GameObject _theParticle=Instantiate(_destroyParticle) as GameObject;
                    _theParticle.transform.position=_curmousePos;
                    _theParticle.GetComponent<ParticleSystem>().Play();
                    FreeAllTheRestriction(_hit2D.collider.gameObject);
                }
            }
        }

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

    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col.gameObject.transform.parent.root.name=="_theCharacter"&&_canDetect)
        {  
            this.gameObject.tag="CharacterCarrier";
            _charRigidbody=_col.gameObject.transform.parent.root.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>();
            _charRigidbody.constraints= RigidbodyConstraints2D.FreezePositionX|RigidbodyConstraints2D.FreezePositionY;
            _col.gameObject.transform.parent.root.transform.SetParent(this.transform);
            _canDetect=false;
        }
    }

    void DisableTheGravity(Character _char)
    {
        for(int i=0;i<_char._playerBodyParts.Count;i++)
        {
            _char._playerBodyParts[i].gameObject.GetComponent<HingeJoint2D>().enabled=false;
            _char._playerBodyParts[i].gravityScale=0f;
        }
    }

    void FreeAllTheRestriction(GameObject _objToHide)
    {
        if(transform.childCount!=0)
        {
            transform.GetChild(0).transform.GetChild(0).GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.None;
            Character _newChar=transform.GetChild(0).GetComponent<Character>();
            for(int i=0;i<_newChar._playerBodyParts.Count;i++)
            {
                _newChar._playerBodyParts[i].gameObject.GetComponent<HingeJoint2D>().enabled=true;
                _newChar._playerBodyParts[i].gravityScale=1f;
            }
            transform.GetChild(0).parent=null;
            _objToHide.gameObject.SetActive(false);
        }
    }

    public void DisableLift()
    {
        _liftStarted=false;
        this.gameObject.AddComponent<Rigidbody2D>();
        //GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.None;
        if(transform.childCount!=0)
        {
             transform.GetChild(0).transform.GetChild(0).GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.None;
        }
    }
}
