using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public float musicTimer = 10;
    public AudioSource audioSource;
    public bool startedPlaying = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    
    
    // Update is called once per frame
    void Update()
    {
        if (musicTimer > 0)
        {
            musicTimer -= Time.deltaTime;
        }

        else
        {
            if (startedPlaying == false)
            {
                audioSource.Play();
                musicTimer = 0;
                startedPlaying = true;
            }
        }
    }
}
