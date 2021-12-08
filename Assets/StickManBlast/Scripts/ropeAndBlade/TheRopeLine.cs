using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TheRopeLine : MonoBehaviour
{
    private LineRenderer _theLine;
    public Transform _previousPoint;
    public Transform _theParentAnchor;

    public bool _startFading=false;

    //
    private Gradient _theLineGradient;
    private GradientAlphaKey[] _theAlphaKey;
    private float _alpha=1f;
    private float _fadeSpeed=1.5f;

  

    void Start()
    {
        _theLine=GetComponent<LineRenderer>();
        _theLineGradient=_theLine.colorGradient;
        _theAlphaKey=_theLineGradient.alphaKeys;
    }

   
    void Update()
    {
        _theLine.SetPosition(0,transform.position);
        _theLine.SetPosition(1,_previousPoint.position);

        if(_startFading)
        {
            if(_alpha>0)
            {
                _alpha-=Time.deltaTime/_fadeSpeed;
                _theAlphaKey[0].alpha=_theAlphaKey[1].alpha=_alpha;
                _theLineGradient.alphaKeys=_theAlphaKey;
                _theLine.colorGradient=_theLineGradient;
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }

    }
   

    
    
}
