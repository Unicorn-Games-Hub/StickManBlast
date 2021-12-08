using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatter : MonoBehaviour
{
    private SpriteRenderer _theRenderer;
    public Sprite[] _splatters;
    private int _selector=0;

    private float _zRot;

   public void Adjust()
    {
        _theRenderer=GetComponent<SpriteRenderer>();

        //adjusting the sprite
        _selector=Random.Range(0,_splatters.Length-1);
       _theRenderer.sprite=_splatters[_selector];
       _theRenderer.sortingOrder=4;
        _theRenderer.maskInteraction=SpriteMaskInteraction.VisibleInsideMask;
        //adjusting the rotation
        _zRot=Random.Range(-360f,360f);
        transform.localRotation=Quaternion.Euler(0f,0f,_zRot);
    }
  
}
