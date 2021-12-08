using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchableDoor : MonoBehaviour
{
    private bool _isInteractable=true;

    public Transform _theLift;
    private bool _canOpenDoor=false;
    private float _curYpos;
    public Transform _initialPoint,_targetPoint;
    public float _speed;

    // Start is called before the first frame update
    void Start()
    {
        _theLift.transform.position=_initialPoint.position;
        _curYpos=_theLift.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(_canOpenDoor)
        {
            if(_theLift.transform.position.y>_targetPoint.position.y)
            {
                _curYpos-=_speed;
            }
            else
            {
                _canOpenDoor=false;
            }
            _theLift.transform.position=new Vector2(_theLift.transform.position.x,_curYpos); 
        }
        
    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col.gameObject.CompareTag("Gear")&&_isInteractable)
        {
            StartCoroutine(WaitBeforeLiftStart());
            _isInteractable=false;
        }
    }

    IEnumerator WaitBeforeLiftStart()
    {
        yield return new WaitForSeconds(0.5f);
        _canOpenDoor=true;
    }
}
