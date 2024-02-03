using UnityEngine;
using UnityEngine.Advertisements;
 
public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidGameId = "5533007";
    [SerializeField] string _iOSGameId = "5533006";
    [SerializeField] bool _testMode = false;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    private string _gameId;
    string _adUnitId = null;
 
    void Awake()
    {
        InitializeAds();
    }
 
    public void InitializeAds()
    {
    #if UNITY_IOS
            _gameId = _iOSGameId;
            _adUnitId = _iOSAdUnitId;
    #elif UNITY_ANDROID
            _gameId = _androidGameId;
            _adUnitId = _androidAdUnitId;
    #elif UNITY_EDITOR //Only for testing the functionality in the Editor
            _gameId = _androidGameId;
            _adUnitId = _androidAdUnitId;
    #endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        LoadAd();
    }
 
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
    }
 
    // Implement a method to execute when the user clicks the button:
    public void ShowAd()
    {
        // Then show the ad:
        Advertisement.Show(_adUnitId, this);
    }
 
    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            LoadAd();
            // Grant a reward.
        }
    }
 
    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
 
    void OnDestroy() { }
}
