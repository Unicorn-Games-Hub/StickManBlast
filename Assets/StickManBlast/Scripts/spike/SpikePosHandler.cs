using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePosHandler : MonoBehaviour
{
    public Transform _screenTopPos;
    private float _offsetY=0.75f;

    private float _animationSpeed=5f;
    private bool _canAnimate=false;

    void Start()
    {
      StartCoroutine(WaitBeforeAdjusting());
    }

    IEnumerator WaitBeforeAdjusting()
    {
        yield return new WaitForSeconds(0.2f);
        _canAnimate=true;
    }

    void Update()
    {
        if(_canAnimate)
        {
            if(transform.position.y>_screenTopPos.position.y-_offsetY)
            {
                transform.position=Vector2.Lerp(transform.position,
                new Vector2(transform.position.x,_screenTopPos.position.y-_offsetY),Time.deltaTime*_animationSpeed);
            }
            else
            {
                _canAnimate=false;
            }
        }
    }

}
