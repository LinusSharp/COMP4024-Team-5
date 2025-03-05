using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Controls the camera movement to follow the player. 
/// </summary>
public class CameraController : MonoBehaviour
{
    
    /// <summary>
    /// Reference to the player's transform to follow.
    /// </summary>
    ///
    /// 
    private Transform player;
    
    /// <summary>
    /// Offset to position the camera relative to the player.
    /// </summary>

    public Vector3 followOffset = new Vector3(0f, 0f, -10f);
    
       /// <summary>
    //  /// List of scenes where the camera stays still 
    //  /// </summary>
    [SerializeField] private string[] followScenes = { "Tutorial", "Level 1" , "Level 2", "Level 3", "Level 4"};
    
          
    /// <summary>
    /// The variables to stop the camera going off the edge of the screen
    /// </summary>
       private bool isFollowing = false;
    
    
    /// <summary>
    /// Minimum x-coordinate the camera can move to.
    /// Prevents the camera from going too far left.
    /// </summary>
    public float minX = -10f;
    
    /// <summary>
    /// Maximum x-coordinate the camera can move to.
    /// Prevents the camera from going too far right.
    /// </summary>
    public float maxX = 10f;
    
    
    /// <summary>
    /// Initialises the camera settings and determines if it should follow the player.
    /// </summary>

    void Start() 
    {
        // gets the player
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player with tag 'Player' not found!");
        }

        // Check if the current scene is one where the camera should follow the player
        string currentScene = SceneManager.GetActiveScene().name;
        foreach (string sceneName in followScenes)
        {
            if (currentScene.Equals(sceneName))
            {
                isFollowing = true;
                break;
            }
        }
    }
    
    /// <summary>
    /// Updates the camera position.
    /// </summary>

    void LateUpdate()
    {
        if (isFollowing && player != null)
            
        {
            // Calculate the x-position based on the player's position and offset
            float targetX = player.position.x + followOffset.x;
            // Clamp the x-position to stay within boundaries
            targetX = Mathf.Clamp(targetX, minX, maxX);
            // Set the camera's new position.
            transform.position = new Vector3(targetX, transform.position.y, followOffset.z);
        }
    }
}
