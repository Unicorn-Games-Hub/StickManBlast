using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSpike : MonoBehaviour
{
    public GameObject _popEffectPrefab;

    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col.gameObject.name=="Baloon")
        {
            _col.gameObject.SetActive(false);
            GameObject _theEffect=Instantiate(_popEffectPrefab)as GameObject;
            _theEffect.transform.position=_col.transform.position;
           _theEffect.GetComponent<ParticleSystem>().Play();
        }
    }
}
