using UnityEngine;
/// <summary>
/// Handles the player's properties,speed and current level.
/// Ensures the player persists across scenes and handles level-up.

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// The speed of the player.
    /// </summary>
    public float moveSpeed = 20f;
    
    /// <summary>
    /// Singleton instance of the <see cref="PlayerController"/>.
    /// </summary>
    private static PlayerController instance;
    /// <summary>
    /// The current level of the player.
    /// </summary>
    public int level = 1;
    
    /// <summary>
    /// The audio source used to play one shot sound effects.
    /// </summary>

    public AudioSource oneShotAudioSource;
    
    
    /// <summary>
    /// The sound effect played when the player levels up.
    /// </summary>
    public AudioClip levelUpSound;
    
    /// <summary>
    /// The volume at which the level-up sound effect is played.
    /// </summary>
    public float levelUpVolume = 0.5f;
    /// <summary>
    /// Called when the script instance is being loaded.
    /// </summary
    private void Awake()
    {
        // destroys the object if it already exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        // marks it as a singlton
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        // Assign the audio source if not set

        if (oneShotAudioSource == null)
        {
            oneShotAudioSource = GetComponent<AudioSource>();
        }
    }
    
    /// <summary>
    /// Increases the player's level and plays a level up sound.
    /// </summary>

    public void IncreaseLevel() {
        level++;
        Debug.Log("Player level increased to: " + level);
        
        // Play the level-up sound effect

        if (oneShotAudioSource != null && levelUpSound != null)
        {
            oneShotAudioSource.PlayOneShot(levelUpSound, levelUpVolume);
        }
    }
    
    /// <summary>
    /// Resets the player's position when a new scene loads.
    /// </summary>
    private void OnEnable()
    {
        transform.position = Vector3.zero; 
    }
}
