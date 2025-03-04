using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Handles switching between player movement controllers based on the current scene.
/// Enables the appropriate movement script and resets physics as needed.
/// </summary>
public class PlayerMovementSwitcher : MonoBehaviour
{
    /// <summary>
    /// Reference to the side-view platformer-style movement controller.
    /// </summary>
    public PlayerControllerSideView sideViewController;
    
    
    /// <summary>
    /// Reference to the top-down movement controller.
    /// </summary>
    public PlayerControllerTopDown topDownController;

    
    /// <summary>
    /// Subscribes to the sceneLoaded event when the object is enabled.
    /// </summary>
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// Unsubscribes from the sceneLoaded event when the object is disabled to prevent memory leaks.
    /// </summary>
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// Called whenever a new scene is loaded.
    /// Determines which movement controller should be enabled based on the scene name
    /// and resets the player's physics state.
    /// </summary>
    /// <param name="scene">The scene that was loaded.</param>
    /// <param name="mode">The mode in which the scene was loaded.</param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Stop all AudioSources on this GameObject and its children.
        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            if (source.loop)
            {
                source.Stop();
            }
        }

        if (scene.name == "Tutorial" || scene.name == "Level 1" || 
            scene.name == "Level 2" || scene.name == "Level 3" || 
            scene.name == "Level 4")
        {
            sideViewController.enabled = true;
            topDownController.enabled = false;
            ResetPlayerPhysics(sideViewController);
        }
        else if (scene.name == "Lobby" || scene.name == "LevelSelector")
        {
            sideViewController.enabled = false;
            topDownController.enabled = true;
            ResetPlayerPhysics(topDownController);
        }
    }
    /// <summary>
    /// Resets the player's physics by stopping movement and rotation.
    /// </summary>
    /// <param name="controller">The movement controller whose Rigidbody2D will be reset.</param>
    private void ResetPlayerPhysics(MonoBehaviour controller)
    {
        Rigidbody2D rb = controller.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
