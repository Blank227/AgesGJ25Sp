using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{


    BulletPool EnemyBulletPool;
    GameObject _player;
    [SerializeField]
    IList<GroupShootTimer> _groupShootTimers = new List<GroupShootTimer>();

    public AudioSource enemyAttackAudio;

    float groupShootTimer = 0;
    int currentGorupShootIndex = 0;


    public void SetGroupShootTimers(List<GroupShootTimer> shootTimers)
    {
        if (shootTimers != null)
            return;

        if (!shootTimers.Any())
            return;
        _groupShootTimers = shootTimers;

        currentGorupShootIndex = 0;
        var currentShootInformation = _groupShootTimers[currentGorupShootIndex];
        groupShootTimer = _groupShootTimers[currentGorupShootIndex].TimeToShoot;

    }



    public void SetPlayerObject(GameObject player)
    {
        _player = player;
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

        if (enemyShootDataObject == null ||EnemyBulletPool == null)
            return;

        var bulletObject = EnemyBulletPool.GetNextBullet();

        var bullet = bulletObject.GetComponent<Bullet>();

        enemyAttackAudio.Play();

        if (enemyShootDataObject.ShootType == ShootTypeEnum.AimAtPlayer)
        {
            Vector2 direction = (_player.transform.position - gameObject.transform.position).normalized;
            bullet.ShootBullet(transform.position, direction, enemyShootDataObject.ShootSpeed);
        }
        else
        {
            bullet.ShootBullet(transform.position, enemyShootDataObject.ShootDirection,enemyShootDataObject.ShootSpeed);
        }



    }
}
