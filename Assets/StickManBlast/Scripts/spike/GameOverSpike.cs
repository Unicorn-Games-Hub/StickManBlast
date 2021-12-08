using UnityEngine;

public class GameOverSpike : MonoBehaviour
{
    private bool _canCollide=true;

   void OnCollisionEnter2D(Collision2D _col)
   {
      if(_col.gameObject!=null&&_canCollide)
      {
          _col.gameObject.transform.parent.root.GetComponent<Character>().ScatterPlayerBodyParts();
          _canCollide=false;
      }
   }
}
