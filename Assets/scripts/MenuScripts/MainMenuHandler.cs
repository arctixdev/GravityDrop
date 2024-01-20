using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif
public class MainMenuHandler : MonoBehaviour
{
    private Button startButton;
    private Button settingsButton;
    private Button backButton;
    private VisualElement mainMenu;
    private VisualElement root;
    private VisualElement settingsMenu;
    private Label nameText;
    private Slider soundSlider;
    private Button huhButton;

    public string gameScene = "map builder level";
    public string username = "Guest";
    public string message = "Hi {0}!";
    public AdsManager AdsManager;

    void Start()
    {
        Application.targetFrameRate = 60;
        // Get root element
        root = GetComponent<UIDocument>().rootVisualElement;

        // Get everything else
        settingsMenu = root.Q<VisualElement>("SettingsMenu");
        mainMenu = root.Q<VisualElement>("MainMenu");
        startButton = root.Q<Button>("start-button");
        settingsButton = root.Q<Button>("settings-button");
        backButton = root.Q<Button>("back-button");
        nameText = root.Q<Label>("name-text");
        soundSlider = root.Q<Slider>("sound-slider");
        huhButton = root.Q<Button>("huh-button");

        // Register click events
        startButton.clicked += StartButtonPressed;
        settingsButton.clicked += SettingsButtonPressed;
        backButton.clicked += BackButtonPressed;
        huhButton.clicked += huhButtonPressed;

        // Hide settings
        settingsMenu.AddToClassList("goneDown");

        // Register slider change func
        soundSlider.RegisterValueChangedCallback(soundChange);

        // PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    // Update is unused
    void Update() {}

    void huhButtonPressed() {
        AdsManager.PlayAd();
    }

    void soundChange(ChangeEvent<float> evt) {
        PlayerPrefs.SetFloat("soundLevel", evt.newValue);
        PlayerPrefs.Save();
    }

    // When start button is pressed
    void StartButtonPressed() {
        MainJumpOut();
        mainMenu.schedule.Execute(() => SceneManager.LoadSceneAsync(gameScene)).StartingIn(200);
    }

#if UNITY_ANDROID

    internal void ProcessAuthentication(SignInStatus status) {
      if (status == SignInStatus.Success) {
        // username = PlayGamesPlatform.Instance.GetUserDisplayName();
      } else {
        username = "Guest";
      }
      nameText.text = string.Format(message, username);
    }
#endif
    void BackButtonPressed() {
        SettingsJumpOut();
        mainMenu.schedule.Execute(() => MainJumpIn()).StartingIn(0);
    }

    // When settings button is pressed
    void SettingsButtonPressed() {
        MainJumpOut();
        mainMenu.schedule.Execute(() => SettingsJumpIn()).StartingIn(0);
    }

    //Add jumpOut class to main menu buttons so they animate out.
    void MainJumpOut() {
        mainMenu.AddToClassList("jumpOut"); 
        mainMenu.schedule.Execute(() => mainMenu.AddToClassList("goneUp") ).StartingIn(0); 
    }

    void MainJumpIn() {
        mainMenu.AddToClassList("jumpIn");
        mainMenu.RemoveFromClassList("goneUp");
    }

    void SettingsJumpIn() {
        settingsMenu.AddToClassList("jumpIn");
        settingsMenu.RemoveFromClassList("goneDown");
    }

    void SettingsJumpOut() {
        settingsMenu.AddToClassList("jumpOut");
        settingsMenu.schedule.Execute(() => settingsMenu.AddToClassList("goneDown") ).StartingIn(0); 
    }
}
