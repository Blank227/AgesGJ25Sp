using UnityEngine;

public class EnemyPathNode : MonoBehaviour
{

    [SerializeField]
    public float NodeSpeed = 5f;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    public EnemyShootDataObject EnemyShootDataObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer.enabled = false;
    }

   
}
