using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOverManager : MonoBehaviour {

    [SerializeField]
    AudioClip audioClip_1;
    [SerializeField]
    AudioClip audioClip_2;
    [SerializeField]
    AudioClip audioClip_3;

    [SerializeField]
    AudioSource audioSource;
    // Use this for initialization

    void Awake() {
        audioSource = GetComponentInChildren<AudioSource>();
    }
    public void PlayFirstAudioClip() {
        audioSource.clip = audioClip_1;
        audioSource.Play();
    }

    public void PlaySecondAudioClip() {
        audioSource.clip = audioClip_2;
        audioSource.Play();
    }

    public void PlayThirdAudioClip() {
        audioSource.clip = audioClip_3;
        audioSource.Play();
    }
}
