using UnityEngine;

public class PlayerHead : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D _col)
    {
        if(_col.CompareTag("Goal")&&this.gameObject.tag=="Head")
        {
            GameController._instance.LevelCompleted ();
        }
    }
}
