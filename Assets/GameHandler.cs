using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{


    int _score = 0;
    [SerializeField]
    float _gameTimeLengthSeconds;

    public bool GameOver { get; set; } = false;


    [SerializeField]
    UiHandler _uiHandler;

    [SerializeField]
    string LoadSceneIndex = "SampleScene";


    public void UpdateScore(int score)
    {
        _score += score; 
        _uiHandler.UpdateScoreText(_score);
    }


    





    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
       
        _uiHandler.UpdateTimeText(_gameTimeLengthSeconds);
        
    }


    public void RemoveGameTime(float seconds)
    {
        _gameTimeLengthSeconds -= seconds;

        _uiHandler.UpdateTimeText(_gameTimeLengthSeconds);

    }

    // Update is called once per frame
    void Update()
    {

        if (GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
            return;
        }

        _gameTimeLengthSeconds -= Time.deltaTime;
        


        _uiHandler.UpdateTimeText(_gameTimeLengthSeconds);

        if (_gameTimeLengthSeconds < 0)
        {
            GameOver = true;
            _uiHandler.StartGameOver();
            //GameOver Here
        }


       

    }
}
