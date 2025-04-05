using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class EnemyGroupData 
{
    public List<EnemyInformationScript> Enemies { get; set; } = new List<EnemyInformationScript>();
    public int GroupId { get; set; } = 0;
    public int ReachedGoal { get; set; } = 0;
} 

public class EnemyGroupHandler : MonoBehaviour
{

   


    public Dictionary<int, EnemyGroupData> GroupIdToEnemyGroupData { get; set; } = new Dictionary<int, EnemyGroupData>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public void AddGroup( EnemyGroupData groupData)
    {
        GroupIdToEnemyGroupData.Add(groupData.GroupId, groupData);
    }



    public void RemoveEnemyFromGroup(int groupId, int enemyId)
    {
        var enemies = GroupIdToEnemyGroupData[groupId].Enemies;
        var enemy = enemies.FirstOrDefault(x => x.EnemyId == enemyId);
        if (enemy != null)
        {
            enemies.Remove(enemy);
            if (!enemies.Any())
            {
                GroupIdToEnemyGroupData.Remove(groupId);
            }
        }
    }
}
