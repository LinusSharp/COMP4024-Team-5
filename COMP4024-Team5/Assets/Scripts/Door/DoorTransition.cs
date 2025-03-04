using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Handles scene transitions when the player enters a door.
/// Can transition to a regular scene or a winning screen.
/// </summary>
public class DoorTransition : MonoBehaviour
{
    
    /// <summary>
    /// The name of the scene to load when the player enters the door.
    /// </summary>
    [SerializeField] private string sceneToLoad = "Lobby";
    
    
    /// <summary>
    /// The name of the winning scene that loads when the player meets the  conditions.
    /// </summary>
    [SerializeField] private string winningScene = "WinningScreen"; // Name of your winning scene

    /// <summary>
    /// Detects when the player enters the door trigger area and transitions to the suitable scene.
    /// </summary>
    /// <param name="other">The collider that entered the trigger area.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Door trigger entered.");
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered door trigger.");
            // Get the player's PlayerController component
            PlayerController player = Object.FindFirstObjectByType<PlayerController>();
            // Check if the scene to load is "LevelSelector" (ignoring case) and if the player level is 4 or higher
            if (player != null && sceneToLoad.Equals("LevelSelector", System.StringComparison.OrdinalIgnoreCase) && player.level >= 5)
            {
                // Load the winning scene
                SceneManager.LoadScene(winningScene);
                Debug.Log("Player level is high enough; loading winning scene.");
                return;
            }

            // Otherwise, load the specified scene
            SceneManager.LoadScene(sceneToLoad);
            Debug.Log($"Loading scene: {sceneToLoad}");
        }
    }
}
