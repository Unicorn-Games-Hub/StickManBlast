using UnityEngine;

public class GroundBreakable : MonoBehaviour
{
    public GameObject _destroyedPrefab;

    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col.gameObject.CompareTag("Destroyable")||_col.gameObject.CompareTag("CharacterCarrier"))
        {
           GameObject _woodDust=Instantiate(_destroyedPrefab,_col.transform.position,Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }
}
