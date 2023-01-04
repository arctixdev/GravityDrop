using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class IngameMenuScript : MonoBehaviour
{
    public GameObject menuObject;

    public AudioClip clickSound;
    public AudioSource audioSource;

    public AudioSource audioSourceMusic;

    public AudioClip theme;
    public AudioClip theme_loopable;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI timerText;

    void Start()
    {
        audioSourceMusic.clip = theme;
        audioSourceMusic.loop = false;
        audioSourceMusic.Play();
        menuObject.SetActive(false);
        click();
        levelText.text = "Level: 0";
    }

    void Update() {
        if (!audioSourceMusic.isPlaying) {
            audioSourceMusic.clip = theme_loopable;
            audioSourceMusic.loop = true;
            audioSourceMusic.Play();
        }
        timerText.text = "Time: " + Mathf.FloorToInt(Time.timeSinceLevelLoad).ToString();
    }

    public void click() {
        audioSource.clip = clickSound;
        audioSource.Play();
    }

    public void Restart() {
        Debug.Log("Restarting");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EnterMenu() {
        click();
        Debug.Log("Entering Menu");
        SceneManager.LoadScene(0);
    }

    public void ToggleMenu() {
        menuObject.SetActive(!menuObject.activeSelf);
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void level(int level) {
        levelText.text = "Level: " + level.ToString();
    }
}
