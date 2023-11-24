using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    private Camera _mainCam;
    [SerializeField] private GameObject container;
    [SerializeField] private TextMeshProUGUI promptText;

    private void Start()
    {
        _mainCam = Camera.main;
        container.SetActive(false);
    }

    private void LateUpdate()
    {
        var rotation = _mainCam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    public bool IsDisplayed = false;

    public void SetUp(string promptText)
    {
        this.promptText.text = promptText;
        container.SetActive(true);
        IsDisplayed = true;
    }

    public void Close()
    {
        if (gameObject != null)
        {
            IsDisplayed = false;
            container.SetActive(false);
        }
    }
}
