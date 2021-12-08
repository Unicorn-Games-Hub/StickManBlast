using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class TheBlade : MonoBehaviour
{
    private Rigidbody2D _blade;
    private CircleCollider2D _collider;

    [Header("Blade Refrence")]
    public float _bladeRadius=0.3f;
    private Vector2 _bladeLastPos;
    private Vector2 _bladeCurPos;
    public float _minBladeVelocity=0.03f;
    public bool _canCutTheRope=false;
    private bool _touchTheRope=false;

    [Header("Blade Trail")]
    public Transform _bladeTrail;
    private int _trailSelector=0;

    [Header("Rope Particle")]
    public Transform _particleContainer;
    private int _particleSelector=0;


    // Start is called before the first frame update
    void Start()
    {
        _blade=GetComponent<Rigidbody2D>();
        _collider=GetComponent<CircleCollider2D>();
        _collider.isTrigger=true;
        _collider.radius=_bladeRadius;
        _collider.enabled=false;
    }   

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCuttingRope();
        }

        if(Input.GetMouseButtonUp(0))
        {
            DontCutTheRope();
        }

        if(_canCutTheRope)
        {
            UpdateTheBlade();
        }
    }

    void StartCuttingRope()
    {
         _canCutTheRope=true;
        _bladeLastPos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _touchTheRope=true;

        if(_trailSelector<_bladeTrail.transform.childCount-1)
        {
            _trailSelector++;
        }
        else
        {
            _trailSelector=0;
        }
    }

    void UpdateTheBlade()
    {
        _bladeCurPos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _blade.position=_bladeCurPos;

        float _bladeVelocity=(_bladeLastPos-_bladeCurPos).magnitude*Time.deltaTime;

        if(_bladeVelocity>=_minBladeVelocity)
        {
          _collider.enabled=true;
          _bladeTrail.GetChild(_trailSelector).gameObject.SetActive(true);
          _bladeTrail.GetChild(_trailSelector).transform.position=_bladeCurPos;
        }
       
    }

    void DontCutTheRope()
    {
        _canCutTheRope=false;
        _collider.enabled=false;
        StartCoroutine(DisableAllTheTrails());
    }

    IEnumerator DisableAllTheTrails()
    {
        yield return new WaitForSeconds(0.5f);
        for(int i=0;i<_bladeTrail.transform.childCount;i++)
        {
            _bladeTrail.GetChild(i).transform.position=Vector3.zero;
            _bladeTrail.GetChild(i).gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D _col)
    {
        if(_col.CompareTag("Rope"))
        {
            if(_touchTheRope)
            {
                MakeRopeFadeAway(_col.gameObject.GetComponent<TheRopeLine>()._theParentAnchor);
                _col.gameObject.GetComponent<LineRenderer>().enabled=false;
                _col.gameObject.GetComponent<TheRopeLine>().enabled=false;
                _col.gameObject.GetComponent<HingeJoint2D>().enabled=false;
                _col.gameObject.GetComponent<DistanceJoint2D>().enabled=false;
                //
                if(_particleSelector<_particleContainer.childCount-1)
                {
                  _particleSelector++;
                }
                else
                {
                   _particleSelector=0;
                }

                _particleContainer.GetChild(_particleSelector).transform.position=_col.transform.position;
                _particleContainer.GetChild( _particleSelector).GetComponent<ParticleSystem>().Play();
                _touchTheRope=false;
            }
        }
    }


    void MakeRopeFadeAway(Transform _theParent)
    {
        for(int i=0;i<_theParent.childCount;i++)
        {
            _theParent.GetChild(i).GetComponent<TheRopeLine>()._startFading=true;
        }

    }

}
