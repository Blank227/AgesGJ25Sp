using UnityEngine;
using TMPro;
using System;

public class UiHandler : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _scoreText;
    [SerializeField]
    TextMeshProUGUI _timeText;
    [SerializeField]
    TextMeshProUGUI _gameOverTxt;



    public void UpdateScoreText(int score)
    {
        _scoreText.text = $"Score:{score}";
    }


    public void UpdateTimeText(float seconds)
    {
        int minutes = (int)seconds / 60;

        int secondsInt = (int) seconds % 60;
       

        string secondText = secondsInt == 0? "00": secondsInt.ToString();

        if (seconds > 0)
        {

            _timeText.text = $"Time:{minutes}:{secondText}";
        }
        else
        {
            _timeText.text = $"Time:{"00"}:{"00"}";
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreText(0);
    }

 
}
