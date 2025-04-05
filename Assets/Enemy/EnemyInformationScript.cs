using UnityEngine;

public class EnemyInformationScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public bool First {get;set;} = false;
    public bool Dead { get; set; } = false;

    public int EnemyGroupId { get; set; }

    public int EnemyId { get; set; }

 
}
