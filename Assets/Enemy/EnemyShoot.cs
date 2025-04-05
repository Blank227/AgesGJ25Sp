using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{


    BulletPool EnemyBulletPool;

    [SerializeField]
    IList<GroupShootTimer> _groupShootTimers = new List<GroupShootTimer>();


    float groupShootTimer = 0;
    int currentGorupShootIndex = 0;

    public void SetGroupShootTimers(List<GroupShootTimer> shootTimers)
    {
        _groupShootTimers = shootTimers;

        currentGorupShootIndex = 0;
        var currentShootInformation = _groupShootTimers[currentGorupShootIndex];
        groupShootTimer = _groupShootTimers[currentGorupShootIndex].TimeToShoot;

    }






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject enemyBulletPoolGameObject = GameObject.FindWithTag("EnemyHandlers");
        EnemyBulletPool = enemyBulletPoolGameObject.GetComponent<BulletPool>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_groupShootTimers.Any() && currentGorupShootIndex < _groupShootTimers.Count)
        {
            groupShootTimer -= Time.deltaTime;
            if (groupShootTimer <= 0)
            {
                var currentShootInformation = _groupShootTimers[currentGorupShootIndex];
                Shoot(currentShootInformation.enemyShootDataObject);
                currentGorupShootIndex++;
                if (currentGorupShootIndex > _groupShootTimers.Count)
                {
                    groupShootTimer = _groupShootTimers[currentGorupShootIndex].TimeToShoot;
                }
                
            }


        }
    }



    public void ResetShootTimers()
    {

        if (!_groupShootTimers.Any())
            return;

        currentGorupShootIndex = 0;
        var currentShootInformation = _groupShootTimers[currentGorupShootIndex];
    }



    public void Shoot(EnemyShootDataObject enemyShootDataObject)
    {
        var bulletObject = EnemyBulletPool.GetNextBullet();

        var bullet = bulletObject.GetComponent<Bullet>();
        bullet.ShootBullet(transform.position, enemyShootDataObject.ShootDirection,enemyShootDataObject.ShootSpeed);

    }
}
