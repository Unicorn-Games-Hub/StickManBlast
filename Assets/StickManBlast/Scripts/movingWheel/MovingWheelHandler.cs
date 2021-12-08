using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingWheelHandler : MonoBehaviour
{
    private Rigidbody2D _wheelRbd;
    public float _wheelVelocity;

    public bool _canMoveAtStart=false;

    public GameObject _destroyParticle;

    void Start()
    {
       _wheelRbd=GetComponent<Rigidbody2D>();

       if(_canMoveAtStart)
       {
           MoveWheelNow();
       }  
    }

    public void MoveWheelNow()
    {
        _wheelRbd.velocity=new Vector2(_wheelVelocity,_wheelRbd.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col.gameObject.CompareTag("Destroyable"))
        {
            StartCoroutine(WaitAndGoahead(_col.gameObject,_col.transform));
        }
    }

    IEnumerator WaitAndGoahead(GameObject _objToDestroy,Transform _pointOfContact)
    {
        yield return new WaitForSeconds(0.8f);
        if(_destroyParticle!=null)
        {
            GameObject _particle=Instantiate(_destroyParticle)as GameObject;
            _particle.transform.position=_pointOfContact.position;
            _particle.GetComponent<ParticleSystem>().Play();
         }
        _objToDestroy.SetActive(false);
        MoveWheelNow();
    }
}
