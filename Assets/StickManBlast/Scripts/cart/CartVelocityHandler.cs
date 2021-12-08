using UnityEngine;

public class CartVelocityHandler : MonoBehaviour
{
    private Rigidbody2D _cartRbd;
    private bool _canAccelerate=true;

    public float _velocityX=1f;

    void Start()
    {
        _cartRbd=GetComponent<Rigidbody2D>();
       
    }
    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col.gameObject.CompareTag("BouncyBall")&&_canAccelerate)
        {
            AdjustCartVelocity();
            _canAccelerate=false;
        }
      
    }

    void AdjustCartVelocity()
    {
        _cartRbd.velocity=new Vector2(_velocityX,_cartRbd.velocity.y);
    }


}
