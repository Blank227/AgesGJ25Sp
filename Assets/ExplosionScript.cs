using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float lifetime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        print("test");
        GetComponent<AudioSource>().Play();
        Destroy(gameObject, lifetime);
    }

}
