using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    List<Transform> _path;
    int goalNodeIndex = 1;
    int currentNodeIndex = 0;
    [SerializeField]
    float startDelay;
    float startTimer;
    [SerializeField]
    float _moveSpeed;

    bool _wait = true;

    Vector2 _currentNodePosition;

    Vector2 _goalNodePosition;


    float _distanceBetweenCurrentAndGoalNodeSqr = 0;





    Vector2 direction;

    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setPath(_path);
    }


    void setPath(List<Transform> newPath)
    {
        _path = newPath;
        MoveToNextPoint();
  
        startTimer = startDelay;
      
      

    }


    public void MoveToNextPoint()
    {
       
        if (goalNodeIndex >= _path.Count)
        {
            //set start position as first node in path and rest
            _wait = true;
            currentNodeIndex = 0;
            goalNodeIndex = 1;
            startTimer = startDelay;
        }
     

        transform.position = _path[currentNodeIndex].position;
        _currentNodePosition = _path[currentNodeIndex].position;
        _goalNodePosition = _path[goalNodeIndex].position;
        _distanceBetweenCurrentAndGoalNodeSqr = (_currentNodePosition - _goalNodePosition).sqrMagnitude;
        direction = (_goalNodePosition - _currentNodePosition).normalized;


        currentNodeIndex = goalNodeIndex;
        goalNodeIndex++;


    }

    // Update is called once per frame
    void Update()
    {
        if (_wait)
        {
            if (goalNodeIndex < _path.Count && startTimer > 0)
            {
                startTimer -= Time.deltaTime;
                if (startTimer <= 0) 
                {
                    _wait = false;

                    

                }
            }
        }
    }



    private void FixedUpdate()
    {
        if (_wait) return;

        Vector2 curerentPosition = transform.position;

        Vector2 newDesiredPosition = curerentPosition + direction *  _moveSpeed *Time.fixedDeltaTime;


        float distanceFromDiserdToCurrent = (newDesiredPosition - _currentNodePosition).sqrMagnitude;
        if (_distanceBetweenCurrentAndGoalNodeSqr < distanceFromDiserdToCurrent)
        {
            transform.position = _goalNodePosition;
            MoveToNextPoint();
        }
        else 
        {
            transform.position = newDesiredPosition;
        }




    }

}
