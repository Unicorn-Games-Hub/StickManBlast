using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FacebookHandler : MonoBehaviour
{
   void Awake()
   {
       if(!FB.IsInitialized)
       {
           FB.Init(InitCallback,OnHideUnity);
       }
       else
       {
           FB.ActivateApp();
       }
   }

   private void InitCallback()
   {
       if(FB.IsInitialized)
       {
           FB.ActivateApp();
       }
       else
       {
           Debug.Log("Failed to initialize facebook SDK");
       }
   }

   private void OnHideUnity(bool isGameShown)
   {
       if(!isGameShown)
       {
           Time.timeScale=0;
       }
       else
       {
           Time.timeScale=1;
       }
   }
}
