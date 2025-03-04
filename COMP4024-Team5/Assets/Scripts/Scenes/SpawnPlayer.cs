using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Handles spawning and resetting the player's positions.
/// </summary>
public class SpawnPlayer : MonoBehaviour
{
    /// <summary>
    /// Initialises the player's spawn position and state.
    /// </summary>
    private void Start()
    {
        InitSpawn();
    }
    /// <summary>
    /// Finds the player object, resets its position, state, and animations. and ensures the correct controller settings based on the scene.
    /// </summary>
    public void InitSpawn()
    
    {
        // Find the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Set the player's position to the spawn position.
            player.transform.position = transform.position;

            // Reset the player's physics so it is no longer moving.
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
            
            // Reset animations and facing direction for side-view controller

            PlayerControllerSideView sideView = player.GetComponent<PlayerControllerSideView>();
            if (sideView != null)
            {
                sideView.ResetFacing();
                sideView.ResetAnimation();
            }
        // Reset animations and facing direction for top-down controller
            PlayerControllerTopDown topView = player.GetComponent<PlayerControllerTopDown>();
            if (topView != null)
            {
                topView.ResetAnimation();
                topView.ResetFacing();
            }
        }
        // Get the current scene
        Scene scene = gameObject.scene;
        // rename the if to the level names
        if (scene.name == "Tutorial" || scene.name == "Level 1" || scene.name == "Level 2" || scene.name == "Level 3" || scene.name == "Level 4")
        {
            var playerController = player.GetComponent<PlayerControllerSideView>();
            if (playerController != null)
            {
                Debug.Log("PlayerController found");
                playerController.SetRespawnPoint();
            }
        }
    }
}
