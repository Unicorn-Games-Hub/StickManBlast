using UnityEngine;

public class TheGrenade : MonoBehaviour
{
    public GameObject _theGrenade;
    //there will be only two doors 0 for unbroken and 1 for broken
    public GameObject _theDoor;
   // public GameObject _brokenDoorPrefab;
    public GameObject _theExplosionParticle;

     public Transform _shatteringObjects;
     private float _dirX,_dirY,_theta;
     public float _explosionForce=10f;

     private Vector2 _mousePosition;

    // Update is called once per frame
    void Update()
    {
      if(Input.GetMouseButtonDown(0))
      {
          _mousePosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D _theRay=Physics2D.Raycast(_mousePosition,Vector2.zero,0f);
            if(_theRay.collider!=null)
            {
                if(_theRay.collider.gameObject.CompareTag("Grenade"))
                {
                    GameObject _theParticle=Instantiate(_theExplosionParticle)as GameObject;
                    _theParticle.transform.position=_mousePosition;
                    _theParticle.GetComponent<ParticleSystem>().Play();
                    startTheExplosion();
                }
            }
    
      }
    }


    void startTheExplosion()
    {
        _theGrenade.SetActive(false);
        _theDoor.SetActive(false);
       // Instantiate(_brokenDoorPrefab,_theDoor.transform.position,Quaternion.identity);

       for(int i=0;i<_shatteringObjects.childCount;i++)
       {
          _theta=_shatteringObjects.GetChild(i).transform.eulerAngles.z+90f;
          _dirX=Mathf.Cos(_theta*Mathf.Deg2Rad);
          _dirY=Mathf.Sin(_theta*Mathf.Deg2Rad);
          _shatteringObjects.GetChild(i).GetComponent<Rigidbody2D>().velocity=new Vector2(_dirX,_dirY)*_explosionForce;
       }
    }

}
