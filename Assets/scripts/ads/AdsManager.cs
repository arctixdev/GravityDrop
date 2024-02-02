using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    const string appKey = "1d468a775";

    void Start()
    {
        //Validate Integration
        IronSource.Agent.validateIntegration();
        
        //IronSource Initialization - Rewarded Video
        IronSource.Agent.init(appKey, IronSourceAdUnits.REWARDED_VIDEO);

        //Add AdInfo Rewarded Video Events
        IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailableEvent;

        //Load Rewarded Video
        IronSource.Agent.loadRewardedVideo();
    }


    /************* RewardedVideo AdInfo Delegates *************/

    // The Rewarded Video ad unit has loaded.
    void RewardedVideoOnAdAvailableEvent(IronSourceAdInfo adInfo){
        Console.WriteLine("RewardedVideoOnAdAvailableEvent");
    }

    // The Rewarded Video ad view has opened. Your activity will loose focus.
    void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo){
        Console.WriteLine("RewardedVideoOnAdOpenedEvent");
    }
    // The Rewarded Video ad view is about to be closed. Your activity will regain its focus.
    void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo){
        Console.WriteLine("RewardedVideoOnAdClosedEvent");
    }
    // The user completed to watch the video, and should be rewarded.
    // The placement parameter will include the reward data.
    // When using server-to-server callbacks, you may ignore this event and wait for the ironSource server callback.
    void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo){
        Console.WriteLine(placement.getRewardAmount());

        // load another ad
        IronSource.Agent.loadRewardedVideo();
    }

    // The rewarded video ad was failed to show.
    void RewardedVideoOnAdShowFailedEvent(IronSourceError error, IronSourceAdInfo adInfo){
        Console.WriteLine(error.getDescription());
    }

    public void PlayAd() {
        Console.WriteLine("PlayAd");
        if (IronSource.Agent.isRewardedVideoAvailable()) {
            IronSource.Agent.showRewardedVideo();
        }
    }
}
