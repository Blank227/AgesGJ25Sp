using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHurt
{

    [SerializeField]
    float IFramesSeconds = 0;

    [SerializeField]
    int health;
    public void Damage(int damage)
    {
        health -= damage;

        if (health < 0)
        {
            Destroy(gameObject);
        }
    }


}
