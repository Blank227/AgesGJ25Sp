using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{


    BulletPool EnemyBulletPool;

    [SerializeField]
    IList<GroupShootTimer> groupShootTimers = new List<GroupShootTimer>();


    float groupShootTimer = 0;
    int currentGorupShootIndex = 0;

    public void SetGroupShootTimers(List<GroupShootTimer> shootTimers)
    {
        groupShootTimers = shootTimers;

        currentGorupShootIndex = 0;
        var currentShootInformation = groupShootTimers[currentGorupShootIndex];
        groupShootTimer = groupShootTimers[currentGorupShootIndex].TimeToShoot;

    }






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject enemyBulletPoolGameObject = GameObject.FindWithTag("EnemyBulletPool");
        EnemyBulletPool = enemyBulletPoolGameObject.GetComponent<BulletPool>();

    }

    // Update is called once per frame
    void Update()
    {
            groupShootTimer -= Time.deltaTime;
        if (groupShootTimers.Any() && currentGorupShootIndex < groupShootTimers.Count)
        {
            if (groupShootTimer <= 0)
            {
                var currentShootInformation = groupShootTimers[currentGorupShootIndex];
                Shoot(currentShootInformation.enemyShootDataObject);
                currentGorupShootIndex++;
                currentGorupShootIndex = currentGorupShootIndex < groupShootTimers.Count ? currentGorupShootIndex : 0;
                groupShootTimer = groupShootTimers[currentGorupShootIndex].TimeToShoot;
                
            }


        }
    }



    public void ResetShootTimers()
    {
      

    }



    public void Shoot(EnemyShootDataObject enemyShootDataObject)
    {
        var bulletObject = EnemyBulletPool.GetNextBullet();

        var bullet = bulletObject.GetComponent<Bullet>();
        bullet.ShootBullet(transform.position, enemyShootDataObject.ShootDirection,enemyShootDataObject.ShootSpeed);

    }
}
