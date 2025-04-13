using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    private void Start() {
        startButton.onClick.AddListener(() => {
            SceneManager.LoadScene(Scene.GameScene.ToString());
        });

        quitButton.onClick.AddListener(() => {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif 
        });
    }
}

public enum Scene {
    None,
    MainMenuScene,
    GameScene
}
