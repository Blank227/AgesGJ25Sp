using TMPro;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] TextMeshProUGUI score;
    int minute = 2;
    int seconds = 9;
    void Start()
    {
        
        score.text = $"{0}";
    }

    
    void Update()
    {
        timer.text = minute + ":" + seconds;
    }
}
