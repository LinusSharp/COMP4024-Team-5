using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    public AudioSource backgroundMusic;
    public AudioClip musicClip; //  background music file

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
            return;
        }


        backgroundMusic = GetComponent<AudioSource>();

        PlayBackgroundMusic(); 
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && musicClip != null)
        {
            backgroundMusic.clip = musicClip;
            backgroundMusic.loop = true; // Ensures it loops
            backgroundMusic.Play();
        }
    }
}