using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private SpriteRenderer _theSwitch;
    public Sprite _switchOnSprite;

    
    //
    private bool _timeToOpenDoor=false;
    public Transform _theDoor;

    public float _targetXpos;
    public float _doorSpeed;
    private float _doorCurXpos;

    // Start is called before the first frame update
    void Start()
    {
        _theSwitch=GetComponent<SpriteRenderer>();
        _doorCurXpos=_theDoor.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
             _theSwitch.sprite=_switchOnSprite;
            _timeToOpenDoor=true;
        }


        if(_timeToOpenDoor)
        {
            if(_theDoor.transform.position.x>_targetXpos)
            {
                _doorCurXpos-=_doorSpeed;
            }
            else
            {
                _timeToOpenDoor=false;
            }
            _theDoor.transform.position=new Vector2(_doorCurXpos,_theDoor.transform.position.y);
        }
    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col!=null)
        {
           _theSwitch.sprite=_switchOnSprite;
            _timeToOpenDoor=true;
        }
    }


}
