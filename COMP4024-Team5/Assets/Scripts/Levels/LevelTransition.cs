using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; 
    [SerializeField] private int levelNumber; // The level this door represents
    [SerializeField] private SpriteRenderer doorSprite; // Reference to the door's SpriteRenderer
    [SerializeField] private Collider2D doorCollider;   // Reference to the door's Collider2D

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
