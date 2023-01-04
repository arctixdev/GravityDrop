using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip theme;
    public AudioClip theme_loopable;
    public AudioClip main_theme;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying) {
            audioSource.clip = theme_loopable;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlayMainTheme() {
        audioSource.clip = main_theme;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void PlayStartTheme() {
        audioSource.clip = theme;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void PlayLoop() {
        audioSource.clip = theme_loopable;
        audioSource.loop = true;
        audioSource.Play();
    }
}
