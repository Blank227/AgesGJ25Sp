using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class GroupShootTimer
{
    public float TimeToShoot;
    public EnemyShootDataObject enemyShootDataObject;
}



[System.Serializable]
public class EnemySpawnConditions
{
    public GameObject EnemyPrefab;
    public Transform EnemyPathParent;
    public int SpawnAmount;
    public float MoveDelay = 0.5f;
    public List<GroupShootTimer> groupShootTimer;
}


public class EnemySpawner : MonoBehaviour
{
    int _enemyGroupId = 0;
    int _enemyId = 0;

    [SerializeField]
    EnemyGroupHandler _enemyGroupHandler;

    [SerializeField]
    List<EnemySpawnConditions> EnemySpawnConditionsLeft = new List<EnemySpawnConditions>();

    [SerializeField]
    List<EnemySpawnConditions> EnemySpawnConditionsRight = new List<EnemySpawnConditions>();


    [SerializeField]
    float SpawnMinTime;

    [SerializeField]
    float SpawnMaxTime;

    float spawnTimerLeft;

    float spawnTimerRight;


    GameObject _leftPlayer;

    GameObject _rightPlayer;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTimerLeft = Random.Range(SpawnMinTime, SpawnMaxTime);
        spawnTimerRight = Random.Range(SpawnMinTime, SpawnMaxTime);
        _leftPlayer = GameObject.FindWithTag("LeftPlayer");
        _rightPlayer = GameObject.FindWithTag("RightPlayer");
    }

    public void SpawnEnemy(EnemySpawnConditions spawnConditions, GameObject playerObject)
    {
        EnemyGroupData enemyGroupData = new EnemyGroupData();
        enemyGroupData.GroupId = _enemyGroupId;
        var path = spawnConditions.EnemyPathParent.GetComponentsInChildren<EnemyPathNode>().ToList();
        
        float startOffsset = 0f;
        for (int i = 0; i < spawnConditions.SpawnAmount; i++)
        {
            GameObject enemyobject = Instantiate(spawnConditions.EnemyPrefab, gameObject.transform);
            var enemyShoot = enemyobject.GetComponent<EnemyShoot>();
            enemyShoot.SetGroupShootTimers(spawnConditions.groupShootTimer);
            enemyShoot.SetPlayerObject(playerObject);
            var enemyMove = enemyobject.GetComponent<EnemyMove>();
            enemyMove.setPath(path, startOffsset);

            var enemyInformation = enemyobject.GetComponent<EnemyInformationScript>();
            enemyInformation.EnemyGroupId = _enemyGroupId;
            enemyInformation.EnemyId = _enemyId;
            enemyInformation.ReachedTheEnd = true;
            startOffsset += spawnConditions.MoveDelay;


            enemyGroupData.Enemies.Add(enemyInformation);



            _enemyId++;
        }
        _enemyGroupId++;
        _enemyGroupHandler.AddGroup(enemyGroupData);
        
    }


    // Update is called once per frame
    void Update()
    {
        spawnTimerLeft -= Time.deltaTime;

        if (spawnTimerLeft <= 0) 
        {
            int randomIndex = Random.Range(0, EnemySpawnConditionsLeft.Count);


            var spawnCondition = EnemySpawnConditionsLeft[randomIndex];


            SpawnEnemy(spawnCondition,_leftPlayer);



            spawnTimerLeft = Random.Range(SpawnMinTime, SpawnMaxTime);
     



        }

        spawnTimerRight -= Time.deltaTime;




        if (spawnTimerRight <= 0)
        {
            int randomIndex = Random.Range(0, EnemySpawnConditionsRight.Count);


            var spawnCondition = EnemySpawnConditionsRight[randomIndex];


            SpawnEnemy(spawnCondition, _rightPlayer);

            spawnTimerRight = Random.Range(SpawnMinTime, SpawnMaxTime);
        }
    }
}
