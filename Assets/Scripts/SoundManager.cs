using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource environmentAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioClip environmentClip;
    
    private const string PLAYER_PREFS_MASTER_VOLUME = "MasterVolume";
    private const string PLAYER_PREFS_ENVIRONMENT_VOLUME = "EnvironmentVolume";
    private const string PLAYER_PREFS_SFX_VOLUME = "SFXVolume";

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start() {
        environmentAudioSource.clip = environmentClip;
        environmentAudioSource.loop = true;
        environmentAudioSource.Play();
    }

    public void PlaySFX(AudioClip clip) {
        sfxAudioSource.clip = clip;
        sfxAudioSource.Play();
    }

    public void PlayPlayerSound(AudioClip clip) {
        playerAudioSource.clip = clip;
        playerAudioSource.Play();
    }

    public void SetPlayerMasterVolume(float value) {
        PlayerPrefs.SetFloat(PLAYER_PREFS_MASTER_VOLUME, value);
    }

    public void SetPlayerMusicVolume(float value) {
        PlayerPrefs.SetFloat(PLAYER_PREFS_ENVIRONMENT_VOLUME, value);
    }

    public void SetPlayerSFXVolume(float value) {
        PlayerPrefs.SetFloat(PLAYER_PREFS_SFX_VOLUME, value);
    }

    public float LoadPlayerMasterVolume() {
        return PlayerPrefs.GetFloat(PLAYER_PREFS_MASTER_VOLUME, 0.6f);
    }

    public float LoadPlayerMusicVolume() {
        return PlayerPrefs.GetFloat(PLAYER_PREFS_ENVIRONMENT_VOLUME, 0.6f);
    }

    public float LoadPlayerSFXVolume() {
        return PlayerPrefs.GetFloat(PLAYER_PREFS_SFX_VOLUME, 0.6f);
    }
}

public enum SoundEffect {
    None,
    HitTree,
    HitRock,
    GrabRock,
    GrabRockPile,
    HarvestShrub,
    HarvestShrubDead
}

public enum AudioMixerParams {
    None,
    MasterVolume,
    EnvironmentVolume,
    SFXVolume
}
