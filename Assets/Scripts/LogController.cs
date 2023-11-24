using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI logText;
    
    public void Information(string message)
    {
        logText.text += message + "\n";
    }
}
