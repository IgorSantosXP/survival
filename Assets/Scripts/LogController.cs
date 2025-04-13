using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogController : MonoBehaviour
{
    public static LogController Instance;
    [SerializeField] private TextMeshProUGUI logText;

    private void Awake() {
        Instance = this;
    }

    public void Information(string message)
    {
        logText.text += message + "\n";
    }
}
