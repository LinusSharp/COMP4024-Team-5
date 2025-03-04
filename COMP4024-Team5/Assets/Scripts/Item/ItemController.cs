using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles collectible item interactions, level progression, and scene transitions when collected.
/// </summary>
public class ItemController : MonoBehaviour
{
    
    /// <summary>
    /// The name of the scene to load after the item is collected.
    /// It  is set to "Lobby".
    /// </summary>
    [SerializeField] private string nextScene = "Lobby";
    
    /// <summary>
    /// UI element to display when the item is collected.
    /// </summary> 
    [SerializeField] private GameObject itemCollectedUI;
    
    
    /// <summary>
    /// Unique key  item that is being collected.
    /// </summary>
   
    [SerializeField] private string itemKey; 
    
    /// <summary>
    /// Tracks if the item has already been collected to prevent multiple triggers.
    /// </summary>
    private bool itemCollected = false;
    
    
    /// <summary>
    /// Initialises the item and ensures it is active when the scene starts.
    /// </summary>
    private void Start() 
    {
        Debug.Log("Scene : " + SceneManager.GetActiveScene().name);

        gameObject.SetActive(true);
    }

    /// <summary>
    /// Called when the item is destroyed.
    /// </summary>
    private void OnDestroy() 
    {
        Debug.Log("CollectableItem has been destroyed");
    }
    
    /// <summary>
    /// Handles the logic when the player collides with the  item.
    /// Increases player level and transitions to the next scene.
    /// </summary>
    /// <param name="other">The collider that triggered the item collection.</param>

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ensure the colliding object is the player and the item hasn't been collected yet
        if (other.CompareTag("Player") && !itemCollected)
        {
            itemCollected = true;
            Debug.Log("Collected Item: " + itemKey);
            // Get the player's PlayerController component
            GameObject parentObject = other.transform.parent.gameObject;
            PlayerController player = parentObject.GetComponent<PlayerController>();
            Debug.Log(player);
            if (player != null) {
                Debug.Log("Player collected item and player not null");
                // Increase the player's level
                player.IncreaseLevel();
                // Destroy the item
                Destroy(gameObject);
                
                // Transition to the Lobby
                BackToLobby();
            }
            // Deactivate the item.
            gameObject.SetActive(false);
        }
    }
    
    /// <summary>
    /// Transitions to the lobby scene after the item is collected.
    /// </summary>
    public void BackToLobby()
    {
        Debug.Log(" going to Lobby");
        SceneManager.LoadScene(nextScene);
    }

}