using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance;
    [SerializeField] private GameObject inventoryScreen;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject soundOptionsMenu;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button quitToMenuButton;
    [SerializeField] private Tooltip tooltip;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider environmentSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider playerSFXSlider;

    private bool isInventoryOpen = false;
    private bool isOptionsOpen = false;

    private void Awake() {
        Instance = this;
    }

    void Start()
    {
        DisableScreens();
        SetDefaultListeners();
        LoadDefaultVolumes();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isInventoryOpen) {
                inventoryScreen.SetActive(false);
                isInventoryOpen = false;
                tooltip.HideTooltip();
                return;
            }
            if (soundOptionsMenu.activeSelf) {
                BackToOptions();
                return;
            }
            if (!isOptionsOpen) {
                optionsMenu.SetActive(true);
                isOptionsOpen = true;
                return;
            }
            CloseOptions();
            isOptionsOpen = false;
        }
        if (isOptionsOpen) return;
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isInventoryOpen)
            {
                inventoryScreen.SetActive(true);
                isInventoryOpen = true;
            } else {
                inventoryScreen.SetActive(false);
                isInventoryOpen = false;
                tooltip.HideTooltip();
            }
            
        }
    }

    private void SetDefaultListeners() {
        soundButton.onClick.AddListener(() => {
            optionsMenu.SetActive(false);
            soundOptionsMenu.SetActive(true);
        });

        quitToMenuButton.onClick.AddListener(() => {
            SceneManager.LoadScene(Scene.MainMenuScene.ToString());
        });

        backButton.onClick.AddListener(() => {
            BackToOptions();
        });

        closeButton.onClick.AddListener(() => {
            CloseOptions();
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

    private void BackToOptions() {
        optionsMenu.SetActive(true);
        soundOptionsMenu.SetActive(false);
    }

    private void CloseOptions() {
        optionsMenu.SetActive(false);
        soundOptionsMenu.SetActive(false);
        isOptionsOpen = false;
    }

    private void LoadDefaultVolumes() {
        masterSlider.value = SoundManager.Instance.LoadMasterVolume();
        environmentSlider.value = SoundManager.Instance.LoadEnvironmentVolume();
        sfxSlider.value = SoundManager.Instance.LoadSFXVolume();
        playerSFXSlider.value = SoundManager.Instance.LoadPlayerSFXVolume();
    }

    private void DisableScreens()
    {
        optionsMenu.SetActive(false);
        soundOptionsMenu.SetActive(false);
        inventoryScreen.SetActive(false);
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

    public bool IsOptionsOpen() {
        return isOptionsOpen;
    }
}
