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
    List<EnemySpawnConditions> EnemySpawnConditions = new List<EnemySpawnConditions>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemy(EnemySpawnConditions.First());
    }

    public void SpawnEnemy(EnemySpawnConditions spawnConditions)
    {
        EnemyGroupData enemyGroupData = new EnemyGroupData();
        enemyGroupData.GroupId = _enemyGroupId;
        var path = spawnConditions.EnemyPathParent.GetComponentsInChildren<EnemyPathNode>().ToList();
        
        float startOffsset = 0f;
        for (int i = 0; i < spawnConditions.SpawnAmount; i++)
        {
            GameObject enemyobject = Instantiate(spawnConditions.EnemyPrefab);
            var enemyShoot = enemyobject.GetComponent<EnemyShoot>();
            enemyShoot.SetGroupShootTimers(spawnConditions.groupShootTimer);
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
        
    }
}
