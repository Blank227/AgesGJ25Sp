using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHurt
{

    [SerializeField]
    float IFramesSeconds = 0;

    [SerializeField]
    int health;

    EnemyGroupHandler _enemyGroupHandler;


    [SerializeField]
    EnemyInformationScript _enemyInformationScript;

    private void Start()
    {
        GameObject enemyHandlerObject = GameObject.FindGameObjectWithTag("EnemyHandler");
        _enemyGroupHandler = enemyHandlerObject.GetComponent<EnemyGroupHandler>();
    }


    public void Damage(int damage)
    {
        health -= damage;

        if (health < 0)
        {
            _enemyGroupHandler.RemoveEnemyFromGroup(_enemyInformationScript.EnemyGroupId, _enemyInformationScript.EnemyId);
         Destroy(gameObject);
        }
    }


}
