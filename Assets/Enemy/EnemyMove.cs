using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    [SerializeField]
    EnemyShoot enemyShoot;

    [SerializeField]
    List<EnemyPathNode> _path;
    int _goalNodeIndex = 1;
    int _currentNodeIndex = 0;
    [SerializeField]
    float startDelay;
    float startTimer;
    [SerializeField]
    float _moveSpeed;

    bool _wait = true;

    bool _canStart = false;

    Vector2 _currentNodePosition;

    Vector2 _goalNodePosition;


    float _distanceBetweenCurrentAndGoalNodeSqr = 0;

    [SerializeField]
    EnemyInformationScript _enemyInformation;

    EnemyCanStartHandler _canStartHandler;



    Vector2 direction;

    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject enemyBulletPoolGameObject = GameObject.FindWithTag("EnemyHandlers");

        _canStartHandler = enemyBulletPoolGameObject.GetComponent<EnemyCanStartHandler>();



    }


    public  void setPath(List<EnemyPathNode> newPath, float moveDelay)
    {
        _path = newPath;
        MoveToNextPoint();
        startDelay = moveDelay;
        startTimer = startDelay;
        print(startDelay);
      
      

    }


    public void MoveToNextPoint()
    {
       
        if (_goalNodeIndex >= _path.Count)
        {
            //set start position as first node in path and rest
            _wait = true;
            _currentNodeIndex = 0;
            _goalNodeIndex = 1;
            startTimer = startDelay;
            enemyShoot.ResetShootTimers();
        }
     

       

        transform.position = _path[_currentNodeIndex].gameObject.transform.position;
        _moveSpeed = _path[_currentNodeIndex].NodeSpeed;
        _currentNodePosition = _path[_currentNodeIndex].gameObject.transform.position;
        _goalNodePosition = _path[_goalNodeIndex].gameObject.transform.position;
        _distanceBetweenCurrentAndGoalNodeSqr = (_currentNodePosition - _goalNodePosition).sqrMagnitude;
        direction = (_goalNodePosition - _currentNodePosition).normalized;
        _currentNodeIndex = _goalNodeIndex;
        _goalNodeIndex++;


        if (_currentNodeIndex < _path.Count)
        {
            var currentNode = _path[_currentNodeIndex];
            if (currentNode.EnemyShootDataObject != null)
            {
                enemyShoot.Shoot(currentNode.EnemyShootDataObject);
            }
        }

      


    }

    // Update is called once per frame
    void Update()
    {
        if (_wait)
        {
            if (!_canStart)
            {
                _canStart = _canStartHandler.CanStart(_enemyInformation.EnemyGroupId);
            }


            if (_goalNodeIndex < _path.Count && startTimer >= 0 && _canStartHandler != null && _canStart)
            {
               
                startTimer -= Time.deltaTime;



                if (startTimer < 0)
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
            _enemyInformation.ReachedTheEnd = _currentNodeIndex == 1? true : false;
            _canStart = !_enemyInformation.ReachedTheEnd;


        }
        else 
        {
            transform.position = newDesiredPosition;
        }

      



    }

}
