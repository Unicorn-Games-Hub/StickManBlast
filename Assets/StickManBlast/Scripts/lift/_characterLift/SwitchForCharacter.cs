using UnityEngine;

public class SwitchForCharacter : MonoBehaviour
{
    private float _curYpos;
    public GameObject _charLift;
    private bool _isInteractable=true;

    void Start()
    {
        _curYpos=transform.position.y;
    }

   void OnCollisionEnter2D(Collision2D _col)
   {
       if(_col.gameObject.CompareTag("Carate")&&_isInteractable)
       {
           _charLift.GetComponent<CharacterCarrier>()._liftStarted=true;
           _curYpos-=0.2f;
           transform.position=new Vector2(transform.position.x,_curYpos);
           _isInteractable=false;
       }
   }
}
