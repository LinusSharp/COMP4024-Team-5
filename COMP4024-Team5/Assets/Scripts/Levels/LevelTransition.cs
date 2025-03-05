using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Handles level transitions through doors, checking if the player has unlocked the level.
/// </summary>
public class LevelTransition : MonoBehaviour
{
    
    
    /// <summary>
    /// The name of the scene to load when the player enters the unlocked level door.
    /// </summary>

    [SerializeField] private string sceneToLoad; 
    
    /// <summary>
    /// The level number associated with this door.
    /// </summary>
    [SerializeField] private int levelNumber; 
    
    
    /// <summary>
    /// Reference to the door's sprite renderer.
    /// </summary>
    [SerializeField] private SpriteRenderer doorSprite;
    
    
    /// <summary>
    /// Reference to the door's Collider2D.
    /// Used to prevent entry if the level is locked.
    /// </summary>
    [SerializeField] private Collider2D doorCollider;   

    
    
    /// <summary>
    /// Checks if the level is unlocked at the start.
    /// </summary>
    private void Start()
    {
        // If this door is not the player's current level, lock it
        if (!IsLevelUnlocked())
        {
            LockDoor();
        }
        else
        {
            UnlockDoor();
        }
    }

    
    /// <summary>
    /// Detects when the player enters the door trigger and transitions to the level.
    /// </summary>
    /// <param name="other">The collider that triggered the transition.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (IsLevelUnlocked())
            {
                // Load the scene assigned to this door
                SceneManager.LoadScene(sceneToLoad);
                Debug.Log($"Level {levelNumber} is unlocked and loading.");
            }
            else
            {
                Debug.Log($"Level {levelNumber} is locked.");
            }
        }
    }
    /// <summary>
    /// Checks if the player has unlocked this level.
    /// </summary>
    /// <returns>True if the player has unlocked the level, false otherwise.</returns>
    private bool IsLevelUnlocked()
    {
        
        // Find the PlayerController in the scene
        PlayerController player = Object.FindFirstObjectByType<PlayerController>();
        Debug.Log("player level " + player.level);
        if (player == null)
        {
            Debug.LogWarning("No PlayerController found in the scene.");
            return false;
        }
        
        // Only unlock the door that matches the player's current level
        return (player.level == levelNumber);
    }

    /// <summary>
    /// Locks the door.
    /// </summary>
    private void LockDoor()
    {
        // Disable the collider
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
            Debug.Log(doorCollider.enabled);
        }

        Debug.Log($"Level {levelNumber} is locked.");
    }
    
    private void UnlockDoor()
    {
        // Dim the door sprite
        if (doorSprite != null)
        {
            Color lockedColor = doorSprite.color;
            lockedColor.a = 0f; // Make it semi-transparent
            doorSprite.color = lockedColor;
        }
    }
}
