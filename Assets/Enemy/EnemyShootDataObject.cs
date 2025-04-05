using UnityEngine;




public enum ShootTypeEnum { Straight, AimAtPlayer }


[CreateAssetMenu(fileName = "EnemyShootDataObject", menuName = "Scriptable Objects/EnemyShootDataObject")]
public class EnemyShootDataObject : ScriptableObject
{
    public ShootTypeEnum ShootTypeEnum;

    public Vector2 ShootDirection;

    public float ShootSpeed;

}
