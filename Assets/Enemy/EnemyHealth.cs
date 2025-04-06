using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHurt
{

    [SerializeField]
    float IFramesSeconds = 0;

    [SerializeField]
    int health;


    [SerializeField]
    int _score;

    EnemyGroupHandler _enemyGroupHandler;
    
    GameHandler _gameHandler;


    [SerializeField]
    EnemyMove enemyMove;
    [SerializeField]
    EnemyShoot enemyShoot;


    [SerializeField]
    EnemyInformationScript _enemyInformationScript;


    public void SetGameHandler(GameHandler gameHandler)
    {
        _gameHandler = gameHandler;
    }

    private void Start()
    {
        GameObject enemyHandlerObject = GameObject.FindGameObjectWithTag("EnemyHandlers");
        _enemyGroupHandler = enemyHandlerObject.GetComponent<EnemyGroupHandler>();
    }



    private void Update()
    {
        if (_gameHandler != null && _gameHandler.GameOver)
        {
            enemyMove.enabled = false;
            enemyShoot.enabled = false;
            this.enabled = false;
        }
    }


    public void Damage(int damage)
    {
        health -= damage;
        

        if (health < 0)
        {


            _gameHandler.UpdateScore(_score);
            _enemyGroupHandler.RemoveEnemyFromGroup(_enemyInformationScript.EnemyGroupId, _enemyInformationScript.EnemyId);
            Destroy(gameObject);
        }
    }


}
