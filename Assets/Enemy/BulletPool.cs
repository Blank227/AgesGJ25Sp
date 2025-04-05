using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{

    [SerializeField]
    GameObject bulletPrefab;


    List<GameObject> Bullets = new List<GameObject>();

    [SerializeField]
    int bulletamount = 1000;

    int currentBulletIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < bulletamount; i++) 
        {
            GameObject bulletObject = GameObject.Instantiate(bulletPrefab);
            Bullets.Add(bulletObject);
            bulletObject.SetActive(false);
        }
    }


    public GameObject GetNextBullet()
    {
        GameObject resultBullet = Bullets[currentBulletIndex];
        resultBullet.SetActive(true);
        currentBulletIndex++;
        currentBulletIndex = currentBulletIndex % Bullets.Count;
        return resultBullet;

    }

}
