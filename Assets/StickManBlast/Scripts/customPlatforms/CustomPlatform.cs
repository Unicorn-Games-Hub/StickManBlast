using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CustomPlatform : MonoBehaviour
{
   public float _initialAngle,_finalAngle;
    private bool _leftRotation,_rightRotation=false;
    public float _rotationSpeed;
    private bool _isDestroyed=false;
    private LineRenderer _theLine;

    public GameObject _theTarget;
    private bool _canCheckTarget=true;

    private bool _rotateIt=false;
    private float _angleZ;
    public Material _lineMaterial;

    void Start()
    {
        _theLine=GetComponent<LineRenderer>();
        TheCalculationAndAdjustments();
    }

    void TheCalculationAndAdjustments()
    {
        //for line
         _theLine.material=_lineMaterial;
        _theLine.receiveShadows=false;
        _theLine.startWidth=0.1f;
        _theLine.textureMode=LineTextureMode.Tile;

        //for rotation
        _angleZ=_initialAngle;
        transform.rotation=Quaternion.Euler(0f,0f,_angleZ);
        if(_initialAngle<0)
        {
            _leftRotation=true;
        }
        else
        {
            _rightRotation=true;
        }
    }

    void Update()
    {
        if(!_theTarget.activeInHierarchy&&_canCheckTarget)
        {
            HangingObjectDestroyed();
            _canCheckTarget=false;
        }
         if(_isDestroyed)
         {
            if(_leftRotation)
            {
                if(_angleZ<_finalAngle)
                {
                    _angleZ+=_rotationSpeed;
                    transform.rotation=Quaternion.Euler(0f,0f,_angleZ);
                }
            }
            else if(_rightRotation)
            {
                if(_angleZ>_finalAngle)
                {
                    _angleZ-=_rotationSpeed;
                    transform.rotation=Quaternion.Euler(0f,0f,_angleZ);
                }
            }
         }
         else
         {
             _theLine.SetPosition(0,transform.GetChild(0).position);
            _theLine.SetPosition(1,_theTarget.transform.position);
         }
    }

    public void HangingObjectDestroyed()
    {
        _isDestroyed=true;
        _theLine.enabled=false;
    }

}
