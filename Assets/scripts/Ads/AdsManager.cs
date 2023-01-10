using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public AdStuff adstuff;
    
    public void PlayAd() {
        if (IronSource.Agent.isRewardedVideoAvailable()) {
            IronSource.Agent.showRewardedVideo(); 
        }
    }

    public void RewardPlayer() {
        Debug.Log("uhAUISfh");
    }

}
