using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MetalChainHandler : MonoBehaviour
{
    private LineRenderer _theLine;
    public float _lineWidth=0.2f;
    public Material _lineMaterial;
    public Transform _pointA,_pointB;

    public bool _canDrawLine=true;

    // Start is called before the first frame update
    void Start()
    {
        _theLine=GetComponent<LineRenderer>();
        _theLine.material=_lineMaterial;
        _theLine.startWidth=_lineWidth;
        _theLine.receiveShadows=false;
        _theLine.textureMode=LineTextureMode.Tile;
    }

    // Update is called once per frame
    void Update()
    {
        if(_canDrawLine&&_pointA.gameObject.activeInHierarchy&&_pointB.gameObject.activeInHierarchy)
        {
            _theLine.SetPosition(0,_pointA.position);
            _theLine.SetPosition(1,_pointB.position);
        }
        else
        {
            _theLine.enabled=false;
        }
    }
}
