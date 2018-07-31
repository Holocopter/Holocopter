using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOverManager : MonoBehaviour {

    [SerializeField]
    AudioClip StartUpVoiceOver;
    [SerializeField]
    AudioClip MultiplayerVoiceOver;
    [SerializeField]
    AudioClip EngineStartUpSound;
    [SerializeField]
    AudioClip EngineLoopSound;

    AudioSource audioSource_EngineSound;
    AudioSource audioSource_VoiceOver;
    // Use this for initialization

    void Awake() {
        audioSource_VoiceOver = GameObject.Find("VoiceOverSpeaker").GetComponent<AudioSource>();
        audioSource_EngineSound = GameObject.Find("EngineSoundSpeaker").GetComponent<AudioSource>();
        StartCoroutine(PlayEngineLoopSound());
    }

    public void PlayFirstAudioClip() {
        audioSource_VoiceOver.clip = StartUpVoiceOver;
        audioSource_VoiceOver.Play();
    }

    public void PlaySecondAudioClip() {
        audioSource_VoiceOver.clip = MultiplayerVoiceOver;
        audioSource_VoiceOver.Play();
    }

    public void PlayThirdAudioClip() {
        audioSource_EngineSound.clip = EngineStartUpSound;
        audioSource_EngineSound.Play();
    }

    public void PlayFourthAudioClip() {
        audioSource_EngineSound.clip = EngineLoopSound;
        audioSource_EngineSound.loop = true;
        audioSource_EngineSound.Play();
    }

    IEnumerator PlayEngineLoopSound() {
        yield return new WaitForSeconds(EngineStartUpSound.length-1.5f);
        PlayFourthAudioClip();
    }
}
