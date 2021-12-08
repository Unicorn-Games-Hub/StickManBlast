using UnityEngine;

public class SawMachine : MonoBehaviour
{
    [Header("For Saw Machine")]
    private float _sawZangle;
    public float _rotationSpeed=2f;
    
    public float _direction;
    public bool _canSawRotate=false;
 
    void Update()
    {
        if(_canSawRotate)
        {
            _sawZangle+=_rotationSpeed*_direction;
           transform.rotation=Quaternion.Euler(0f,0f,_sawZangle);
        }
    }
}
