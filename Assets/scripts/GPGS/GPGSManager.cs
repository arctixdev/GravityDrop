using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class GPGSManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    // Update is called once per frame
    void Update(){}

    internal void ProcessAuthentication(SignInStatus status) {
      if (status == SignInStatus.Success) {
        // Continue with Play Games Services
        Debug.Log("Auth sucess");
      } else {
        Debug.LogError("FAILED WITH ERROR:" + status);
        // Disable your integration with Play Games Services or show a login button
        // to ask users to sign-in. Clicking it should call
        // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
      }
    }
}
