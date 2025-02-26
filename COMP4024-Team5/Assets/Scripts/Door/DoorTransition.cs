using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTransition : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "Lobby";
    [SerializeField] private string winningScene = "WinningScreen"; // Name of your winning scene

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Door trigger entered.");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered door trigger.");
            // Get the player's PlayerController component
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            
            // Check if the scene to load is "LevelSelector" (ignoring case) and if the player level is 4 or higher
            if (player != null && sceneToLoad.Equals("LevelSelector", System.StringComparison.OrdinalIgnoreCase) && player.level >= 5)
            {
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
