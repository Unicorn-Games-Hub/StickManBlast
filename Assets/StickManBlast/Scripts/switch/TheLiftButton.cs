using UnityEngine;

public class TheLiftButton : MonoBehaviour
{
    private Vector2 _buttonCurpos;
    private float _curYpos;
    public OneTimeLift _theLift;
    private bool _moveDown=true;

    void Start()
    {
        _buttonCurpos=transform.position;
        _curYpos=_buttonCurpos.y;
    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col.gameObject.CompareTag("Carate"))
        {
            if(_moveDown)
            {
                _curYpos-=0.28f;
                _moveDown=false;
            }
        
            transform.position=new Vector2(transform.position.x,_curYpos);
            _theLift._canMove=true;

        }
    }
}
