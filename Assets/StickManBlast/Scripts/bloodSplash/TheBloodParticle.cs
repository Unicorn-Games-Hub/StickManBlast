using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBloodParticle : MonoBehaviour
{
    private ParticleSystem _theblood;
    private List<ParticleCollisionEvent> _collisionEvent=new List<ParticleCollisionEvent>();

    public GameObject _splatterPrefab;

    public Transform _splashContainer;
    
    // Start is called before the first frame update
    void Start()
    {
        _theblood=GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject _col)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(_theblood,_col,_collisionEvent);
         
        int _eventNum=_collisionEvent.Count;

        for(int i=0;i<_eventNum;i++)
        {
          GameObject _theSplat=Instantiate(_splatterPrefab,_collisionEvent[i].intersection,Quaternion.identity) as GameObject;
          _theSplat.GetComponent<BloodSplatter>().Adjust();

          _theSplat.transform.SetParent(_col.transform,true);
        }
    }
}
