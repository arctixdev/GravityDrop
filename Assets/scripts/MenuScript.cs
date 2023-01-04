using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject SettingsButton;
    public GameObject ExitButton;
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject CreditsMenuObject;
    public TextMeshProUGUI DisplaySettingsText;

    public AudioClip clickSound;
    public AudioSource audioSource;

    void Start()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        CreditsMenuObject.SetActive(false);
        if (Screen.fullScreen == true) {
            DisplaySettingsText.text = "FullScreen: On";
        } else {
            DisplaySettingsText.text = "FullScreen: Off";
        }
    }

    public void click() {
        audioSource.clip = clickSound;
        audioSource.Play();
    }

    public void ExitGame() {
        Debug.Log("Exiting game");
        Application.Quit();
    }

    public void EnterSettings() {
        Debug.Log("Entering Settings");
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
        CreditsMenuObject.SetActive(false);
    }

    public void BackMain() {
        Debug.Log("Going back to main menu");
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        CreditsMenuObject.SetActive(false);
    }

    public void PlayGame() {
        Debug.Log("Starting game");
        SceneManager.LoadScene(1);
    }

    public void DisplaySettings() {
        Debug.Log(Screen.fullScreen);
        if (Screen.fullScreen == true) {
            Debug.Log("Turning off FullScreen");
            Screen.fullScreen = false;
            DisplaySettingsText.text = "FullScreen: Off";
        } else {
            Debug.Log("Turning on FullScreen");
            Screen.fullScreen = true;
            DisplaySettingsText.text = "FullScreen: On";
        }
    }

    public void CreditsMenu() {
        CreditsMenuObject.SetActive(true);
        SettingsMenu.SetActive(false);
        MainMenu.SetActive(false);
    }
}
