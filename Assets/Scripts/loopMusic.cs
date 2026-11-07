using UnityEngine;

public class loopMusic : MonoBehaviour
{
    private AudioSource music;
    public float startLoop;
    public float endLoop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        music = GetComponent<AudioSource>();
        music.loop = false;
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (music.time >= endLoop)
        {
            AudioSource previous = GetComponent<AudioSource>();
            music = gameObject.AddComponent<AudioSource>();

            music.clip = previous.clip;
            music.Play();
            Destroy(previous, previous.clip.length - previous.time);
        }
    }
}
