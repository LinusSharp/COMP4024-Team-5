using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Controls the lobby background based on the player's current level.
/// Changes the background image dynamically according to the player's progress.
/// </summary>
public class LobbyController : MonoBehaviour
{ /// <summary>
    /// The Imag that displays the background.
    /// </summary>
    [SerializeField] private Image backgroundImage;
    
    
    /// <summary>
    /// Array of background images corresponding to different levels.
    /// </summary>
    [SerializeField] private Sprite[] levelBackgrounds;

    
    /// <summary>
    /// Called when the scene starts.
    /// Retrieves the player's current level and updates the background image.
    /// </summary>
    private void Start()
    {
        // Find the PlayerController
        PlayerController player = Object.FindFirstObjectByType<PlayerController>();
        if (player == null)
        {
            Debug.LogError("Player object not found!");
            return;
        }
        // Determine the index for the background array based on the player's level
        int level = player.level;
        int index = level - 1;

        Debug.Log(index);
        // If a corresponding background exists, update the background image
        if (index >= 0 && index < levelBackgrounds.Length)
        {
            backgroundImage.sprite = levelBackgrounds[index];
            Debug.Log($"Loaded background for level {level}");
        }
        else
        {
            Debug.LogWarning($"No background defined for level {level}");
        }
    }
}
