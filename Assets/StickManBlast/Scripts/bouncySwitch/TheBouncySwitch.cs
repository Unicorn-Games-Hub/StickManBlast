using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBouncySwitch : MonoBehaviour
{
    public GameObject _objToHide;

    private float _switchYpos;

    public GameObject _theBouncyPlatform;

    public CharacterCarrier _cC;

    // Start is called before the first frame update
    void Start()
    {
        _switchYpos=transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col.gameObject.CompareTag("BouncyBall"))
        {
            _col.gameObject.SetActive(false);
            _theBouncyPlatform.GetComponent<BoxCollider2D>().sharedMaterial=null;
            if(_objToHide!=null)
            {
                _objToHide.SetActive(false);
            }
            if(_cC!=null)
            {
                _cC._liftStarted=true;
            }
        }
    }
}
