using UnityEngine;

public class TheBaloon : MonoBehaviour
{
    private LineRenderer _baloonRope;
    private Rigidbody2D _rb2d;
    public float _flySpeed;
    public Material _ropeMaterial;
    public Rigidbody2D _characterHead;




    // Start is called before the first frame update
    void Start()
    {
       // _rb2d=GetComponent<Rigidbody2D>();
        AdjustAllTheComponents();
       // _rb2d.velocity=new Vector2(_rb2d.velocity.x,_flySpeed);

    }

    void AdjustAllTheComponents()
    {
        _baloonRope=GetComponent<LineRenderer>();
        _baloonRope.material=_ropeMaterial;
        _baloonRope.startWidth=0.04f;
    }

    void Update()
    {
        _baloonRope.SetPosition(0,transform.GetChild(0).transform.position);
        _baloonRope.SetPosition(1,_characterHead.transform.position);
    }

   
}
