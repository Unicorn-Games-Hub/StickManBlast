using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using com.adjust.sdk;

public class GameAnalyticsHandler : MonoBehaviour
{
    public static GameAnalyticsHandler _instance{set;get;}

   void Awake()
   {
        if(_instance!=null)
        {
            return;
        }
        else
        {
            _instance=this;
        }
   }

   void Start()
   {
       GameAnalytics.Initialize();
   }

   public void TrackTheLevelStart(int _lvlNum)
   {
       GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start,Application.version,_lvlNum.ToString("00000"));
   }

   public void TrackTheLevelComplete(int _completedLvlNum, int _theScore)
   {
       GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete,Application.version,_completedLvlNum.ToString("00000"),_theScore);
   }

   void InitializingAdjustSDK()
   {
       string _appToken="{ikyu94hnkao0}";
       AdjustEnvironment _environment=AdjustEnvironment.Production;
       AdjustConfig _config=new AdjustConfig(_appToken,_environment,true);
       _config.setLogLevel(AdjustLogLevel.Info);
       Adjust.start(_config);
   }

   public void Level5Token()
   {
       AdjustEvent _adjustEvent=new AdjustEvent("q1f101");
        Adjust.trackEvent(_adjustEvent);
   }

   public void Level15Token()
   {
        AdjustEvent _adjustEvent=new AdjustEvent("gy38ga");
        Adjust.trackEvent(_adjustEvent);
   }
}
