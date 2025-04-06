using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _scoreText;
    [SerializeField]
    TextMeshProUGUI _timeText;
    [SerializeField]
    Image _gameOverImage;

    bool _gameOver = false;


    Vector3 gameOverStartPosition = Vector3.zero;
    Vector3 gameOverGoalPosition = Vector3.zero;


    [SerializeField]
    float GameOverLerpTime = 0;

    float _gameOverLerpTimer = 0;


    float _gameOverFloatTimer = 0;
    [SerializeField]
    float gameOverSinusMaxHeight;
    [SerializeField]
    float gameOverSinusSpeed;
    


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

            _timeText.text = $"0{minutes}:{secondText}";
        }
        else
        {
            _timeText.text = $"{"00"}:{"00"}";
        }

    }


    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreText(0);
        gameOverStartPosition = _gameOverImage.transform.localPosition;
    }


    public void StartGameOver()
    {
        _gameOver = true;
    }

    private void Update()
    {
        if (_gameOver) 
        {
            if (_gameOverLerpTimer <= GameOverLerpTime)
            {
                _gameOverLerpTimer += Time.deltaTime;
                _gameOverImage.transform.localPosition= Vector3.Slerp(gameOverStartPosition, gameOverGoalPosition, _gameOverLerpTimer / GameOverLerpTime);
            }
            else
            {
                _gameOverFloatTimer += Time.deltaTime;
                Vector3 newPostion = new Vector3(gameOverGoalPosition.x, gameOverGoalPosition.y + gameOverSinusMaxHeight * MathF.Sin(_gameOverFloatTimer * gameOverSinusSpeed), gameOverGoalPosition.z);
                _gameOverImage.transform.localPosition = newPostion;


            }



          
        }
    }

}
