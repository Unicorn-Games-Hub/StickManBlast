using UnityEngine;

public class SpikeLiftSwitch : MonoBehaviour
{
    private bool _canPressSwitch=true;

    private float _curYpos;

    public TheSpikeLift _spikeLift;

    // Start is called before the first frame update
    void Start()
    {
        _curYpos=transform.position.y;
    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col.gameObject.CompareTag("Carate")&&_canPressSwitch)
        {
            _curYpos-=0.13f;
            transform.position=new Vector2(transform.position.x,_curYpos);
            _spikeLift._liftStarted=true;
            _canPressSwitch=false;
        }
    }
}
