using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicResolutionCamera : MonoBehaviour
{
    private float _screenHeight,_screenWidth,_screenAspect,_camCurSize;
    private Camera _gameCam;

    private float _leftBorder,_rightBorder,_topBorder,_bottomBorder;
    private float _borderXsize,_borderYsize;

    private float _offsetValue=0.6f;
    // Start is called before the first frame update
    void Start()
    {
        _gameCam=GetComponent<Camera>();
        AdjustTheGameResolution();
    }

    void AdjustTheGameResolution()
    {
        _screenWidth=Screen.width;
        _screenHeight=Screen.height;
        _camCurSize=_gameCam.orthographicSize;

        _screenAspect=_screenWidth/_screenHeight;

        _gameCam.orthographicSize=_camCurSize+_screenAspect;
        _gameCam.transform.position=new Vector3(_gameCam.transform.position.x,_screenAspect,_gameCam.transform.position.z);

       AdjustingTheBorder();
    }

    void AdjustingTheBorder()
    {
        _leftBorder=Camera.main.ViewportToWorldPoint(new Vector3(0f,0f,-10f)).x;
        _rightBorder=Camera.main.ViewportToWorldPoint(new Vector3(1f,0f,-10f)).x;
        _topBorder=Camera.main.ViewportToWorldPoint(new Vector3(0f,1f,-10f)).y;
        _bottomBorder=Camera.main.ViewportToWorldPoint(new Vector3(0f,0f,-10f)).y;

        _borderXsize=_rightBorder-_leftBorder;
       _borderYsize=_topBorder-_bottomBorder;

        //position
        transform.GetChild(0).transform.position=new Vector2(_leftBorder-_offsetValue,_screenAspect);
        transform.GetChild(1).transform.position=new Vector2(_rightBorder+_offsetValue,_screenAspect);
        transform.GetChild(2).transform.position=new Vector2(0f,_topBorder+_offsetValue);

        //scale
        transform.GetChild(0).transform.localScale=new Vector3(1f,_borderYsize,1f);
        transform.GetChild(1).transform.localScale=new Vector3(1f,_borderYsize,1f);
        transform.GetChild(2).transform.localScale=new Vector3(_borderXsize,1f,1f);
    }
    
}
