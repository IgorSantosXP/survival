using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject mainMenuButtonsContainer;
    [SerializeField] private GameObject soundOptionsContainer;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider environmentSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider playerSFXSlider;

    private void Start() {
        SetDefaultContainers();
        SetDefaultListeners();
        LoadDefaultVolumes();
    }

    private void SetDefaultContainers() {
        mainMenuButtonsContainer.SetActive(true);
        soundOptionsContainer.SetActive(false);
    }

    private void SetDefaultListeners() {
        startButton.onClick.AddListener(() => {
            SceneManager.LoadScene(Scene.GameScene.ToString());
        });

        soundButton.onClick.AddListener(() => {
            mainMenuButtonsContainer.SetActive(false);
            soundOptionsContainer.SetActive(true);
        });

        quitButton.onClick.AddListener(() => {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif 
        });

        backButton.onClick.AddListener(() => {
            mainMenuButtonsContainer.SetActive(true);
            soundOptionsContainer.SetActive(false);
        });

        masterSlider.onValueChanged.AddListener((float value) => {
            SetMasterVolume(value);
        });

        environmentSlider.onValueChanged.AddListener((float value) => {
            SetEnvironmentVolume(value);
        });

        sfxSlider.onValueChanged.AddListener((float value) => {
            SetSFXVolume(value);
        });

        playerSFXSlider.onValueChanged.AddListener((float value) => {
            SetPlayerSFXVolume(value);
        });
    }

    private void LoadDefaultVolumes() {
        masterSlider.value = SoundManager.Instance.LoadMasterVolume();
        environmentSlider.value = SoundManager.Instance.LoadEnvironmentVolume();
        sfxSlider.value = SoundManager.Instance.LoadSFXVolume();
        playerSFXSlider.value = SoundManager.Instance.LoadPlayerSFXVolume();
    }

    private void SetMasterVolume(float value) {
        audioMixer.SetFloat(AudioMixerParams.MasterVolume.ToString(), Mathf.Log10(value) * 20);
        SoundManager.Instance.SetMasterVolume(value);
    }

    private void SetEnvironmentVolume(float value) {
        audioMixer.SetFloat(AudioMixerParams.EnvironmentVolume.ToString(), Mathf.Log10(value) * 20);
        SoundManager.Instance.SetEnvironmentVolume(value);
    }

    private void SetSFXVolume(float value) {
        audioMixer.SetFloat(AudioMixerParams.SFXVolume.ToString(), Mathf.Log10(value) * 20);
        SoundManager.Instance.SetSFXVolume(value);
    }

    private void SetPlayerSFXVolume(float value) {
        audioMixer.SetFloat(AudioMixerParams.PlayerSFXVolume.ToString(), Mathf.Log10(value) * 20);
        SoundManager.Instance.SetPlayerSFXVolume(value);
    }
}

public enum Scene {
    None,
    MainMenuScene,
    GameScene
}
