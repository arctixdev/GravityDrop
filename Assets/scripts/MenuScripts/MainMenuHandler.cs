using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    private Button startButton;
    private Button settingsButton;
    private Button backButton;
    private VisualElement mainMenu;
    private VisualElement root;
    private VisualElement settingsMenu;

    public string gameScene = "map builder level";

    void Start()
    {
        // Get root element
        root = GetComponent<UIDocument>().rootVisualElement;

        // Get everything else
        settingsMenu = root.Q<VisualElement>("SettingsMenu");
        mainMenu = root.Q<VisualElement>("MainMenu");
        startButton = root.Q<Button>("start-button");
        settingsButton = root.Q<Button>("settings-button");
        backButton = root.Q<Button>("back-button");

        // Register click events
        startButton.clicked += StartButtonPressed;
        settingsButton.clicked += SettingsButtonPressed;
        backButton.clicked += BackButtonPressed;

        // Hide settings
        settingsMenu.AddToClassList("goneDown");
    }

    // Update is unused
    void Update() {}

    // When start button is pressed
    void StartButtonPressed() {
        MainJumpOut();
        mainMenu.schedule.Execute(() => SceneManager.LoadScene(gameScene)).StartingIn(200);
        //SceneManager.LoadScene(gameScene);
    }

    void BackButtonPressed() {
        SettingsJumpOut();
        MainJumpIn();
    }

    // When settings button is pressed
    void SettingsButtonPressed() {
        MainJumpOut();
        SettingsJumpIn();
    }

    //Add jumpOut class to main menu buttons so they animate out.
    void MainJumpOut() {
        mainMenu.AddToClassList("jumpOut"); 
        mainMenu.schedule.Execute(() => mainMenu.AddToClassList("goneUp") ).StartingIn(150); 
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
        settingsMenu.schedule.Execute(() => settingsMenu.AddToClassList("goneDown") ).StartingIn(30); 
    }
}
