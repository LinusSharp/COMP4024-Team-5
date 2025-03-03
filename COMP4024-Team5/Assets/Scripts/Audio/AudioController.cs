using UnityEngine;
/// <summary>
/// Controls the background music in the game, ensuring it loads across  all scenes.
/// </summary>
public class AudioController : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the AudioController.
    /// </summary>
    public static AudioController instance;

    /// <summary>
    /// The AudioSource component responsible for playing the background music.
    /// </summary>
    public AudioSource backgroundMusic;
    
    /// <summary>
    /// It contains the background music file.
    /// </summary>
    public AudioClip musicClip; 

    /// <summary>
    /// Called when the  instance is being loaded.
    /// Ensures that only one instance of AudioController exists and loads  across scenes.
    /// </summary>
    private void Awake()
    {
        
        // Implement Singleton pattern 
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps the music playing across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances 
            return;
        }

        // Get the AudioSource component attached to the game object
        backgroundMusic = GetComponent<AudioSource>();
        // Start playing the background music

        PlayBackgroundMusic(); 
    }

    /// <summary>
    /// Plays the background music.
    /// </summary>
    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && musicClip != null)
        {
            backgroundMusic.clip = musicClip;
            backgroundMusic.loop = true;  // Ensures the music loops across all scenes. 
            backgroundMusic.Play();
        }
    }
}