using UnityEngine;

public class GroundDestroyable : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody=GetComponent<Rigidbody2D>();
        _rigidBody.bodyType=RigidbodyType2D.Static;
    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        _rigidBody.bodyType=RigidbodyType2D.Dynamic;
    }

   
}
