using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredSpikeWheel : MonoBehaviour
{
    public Transform _initialYpos,_finalYpos;
    public float _speed;
    public bool _isStarted=false;
    private bool _moveUp,_moveDown=false;
    private float _curYpos;
    private bool _canWait=false;
    private bool _canCollide=true;


    private GameObject _theBody;

    void Start()
    {
        transform.position=new Vector2(transform.position.x,_initialYpos.position.y);
        _curYpos=transform.position.y;
    }

    void Update()
    {
        if(_isStarted)
        {
            if(_moveUp)
            {
                if(transform.position.y<_finalYpos.position.y)
                {
                    _curYpos+=_speed;
                }
                else
                {
                    StartCoroutine(WaitBeforeGoingDown());
                    _moveUp=false;
                }
            }
            else if(_moveDown)
            {
                if(transform.position.y>_initialYpos.position.y)
                {
                    _curYpos-=_speed;
                }
                else
                {
                    _moveDown=false;
                }
            }
            transform.position=new Vector2(transform.position.x,_curYpos);
        }
    }

    IEnumerator WaitBeforeGoingDown()
    {
        yield return new WaitForSeconds(4f);
        _moveDown=true;
    }

    public void TimeToGoUp()
    {
        _moveUp=true;
    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col.transform.parent!=null&&_canCollide)
        {
            _theBody= _col.transform.parent.root.transform.GetChild(0).transform.GetChild(0).transform.gameObject;
            _col.transform.parent.root.transform.GetChild(0).transform.GetChild(0).transform.parent=null;
            _theBody.GetComponent<Character>().ScatterPlayerBodyParts();
            _theBody.transform.GetChild(0).gameObject.GetComponent<HingeJoint2D>().enabled=false;
            _canCollide=false;
        }
    }
}
