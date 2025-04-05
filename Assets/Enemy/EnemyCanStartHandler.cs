using System.Linq;
using UnityEngine;

public class EnemyCanStartHandler : MonoBehaviour
{
    [SerializeField]
    EnemyGroupHandler _enemyGroupHandler;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool CanStart(int groupId)
    {
        if (_enemyGroupHandler.GroupIdToEnemyGroupData.ContainsKey(groupId))
        {
           return _enemyGroupHandler.GroupIdToEnemyGroupData[groupId].Enemies.All(x => x.ReachedTheEnd == true);
        }
        return false;
    }



    public void RemoveEnemyFromGroup(int groupId, int enemyId)
    {
        _enemyGroupHandler.RemoveEnemyFromGroup(groupId, enemyId);
    }

}
