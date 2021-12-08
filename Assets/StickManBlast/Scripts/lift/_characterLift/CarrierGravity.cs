using UnityEngine;

public class CarrierGravity : MonoBehaviour
{
    public Transform _charCarrier;
    public GameObject _destroyParticle;
  
    void Update()
    {
          if(Input.GetMouseButtonDown(0))
        {
            Vector2 _curmousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D _hit2D=Physics2D.Raycast(_curmousePos,Vector2.zero);
            if(_hit2D.collider!=null)
            {
                if(_hit2D.collider.gameObject.CompareTag("SwitchWheel"))
                {
                    GameObject _theParticle=Instantiate(_destroyParticle) as GameObject;
                    _theParticle.transform.position=_curmousePos;
                    _theParticle.GetComponent<ParticleSystem>().Play();
                      _hit2D.collider.gameObject.SetActive(false);
                    _charCarrier.gameObject.GetComponent<CharacterCarrier>().DisableLift();
                }
            }
        }
    }
}
