using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentHider : MonoBehaviour
{
    public GameObject _destroyParticle;
    public Rigidbody2D[] _theCarates;

    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 _curmousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D _hit2D=Physics2D.Raycast(_curmousePos,Vector2.zero);
            if(_hit2D.collider!=null)
            {
            if(_hit2D.collider.gameObject.CompareTag("TheParent"))
            {
                GameObject _theParticle=Instantiate(_destroyParticle) as GameObject;
                _theParticle.transform.position=_curmousePos;
                _theParticle.GetComponent<ParticleSystem>().Play();
                 FreeAllTheChild();
            }
            }
        }
    }

    void FreeAllTheChild()
    {
        for(int i=0;i<_theCarates.Length;i++)
        {
            _theCarates[i].gameObject.transform.parent=null;
            _theCarates[i].gravityScale=1;
        }
        if(_carateRbd!=null)
        {
         _carateRbd.gravityScale=1;
        _carateRbd.constraints=RigidbodyConstraints2D.None;
        }
        this.gameObject.SetActive(false);
    }

    private Rigidbody2D _carateRbd;

    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col.gameObject.CompareTag("Destroyable"))
        {
            _carateRbd=_col.gameObject.GetComponent<Rigidbody2D>();
            //_carateRbd.gravityScale=0;
            _carateRbd.constraints=RigidbodyConstraints2D.FreezePositionX;
            _col.gameObject.transform.SetParent(this.transform);
           // _col.gameObject.GetComponent<Rigidbody2D>().gravityScale=0;
        }
    }
}
