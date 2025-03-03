using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the background music in the game.
/// Supports different music for the different screens.
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
    /// The default background music for the game levels.
    /// </summary>
    public AudioClip mainGameMusic;

    /// <summary>
    /// The background music when starting the game.
    /// </summary>
    public AudioClip startScreenMusic;

    /// <summary>
    /// Called when the instance is being loaded.
    /// </summary>
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

        // Get the AudioSource
        backgroundMusic = GetComponent<AudioSource>();

        // Subscribe to scene load events to change music dynamically
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Play the correct background music for the starting scene
        UpdateMusic(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Called whenever a new scene loads to check and update the background music.
    /// </summary>
    /// <param name="scene">The new loaded scene.</param>
    /// <param name="mode">The scene load mode.</param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateMusic(scene.name);
    }

    /// <summary>
    /// Plays the appropriate background music based on the scene name.
    /// </summary>
    /// <param name="sceneName">The name of the active scene.</param>
    private void UpdateMusic(string sceneName)
    {
        if (backgroundMusic == null) return;

        // Choose the correct music for the scene
        AudioClip newMusic = sceneName == "Start" ? startScreenMusic : mainGameMusic;

      
        if (backgroundMusic.clip != newMusic)
        {
            backgroundMusic.clip = newMusic;
            backgroundMusic.loop = true;
            backgroundMusic.Play();
        }
    }
}
