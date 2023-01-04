using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThudScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip thud1;
    public AudioClip thud2;
    public AudioClip thud3;
    public AudioClip thud4;
    public AudioClip thud5;
    public AudioClip thud6;
    public AudioClip thud7;
    public AudioClip thud8;

    private AudioClip[] clips;

    public AudioClip rotateSound;
    void Start() {
        clips = new AudioClip[8];
        clips[0] = thud1;
        clips[1] = thud2;
        clips[2] = thud3;
        clips[3] = thud4;
        clips[4] = thud5;
        clips[5] = thud6;
        clips[6] = thud7;
        clips[7] = thud8;
    }

    public void thud() {
        int thudNumber = Random.Range(0, 7);
        audioSource.clip = clips[thudNumber];
        audioSource.Play();
    }

    public void rotate() {
        audioSource.clip = rotateSound;
        audioSource.Play();
    }
}
