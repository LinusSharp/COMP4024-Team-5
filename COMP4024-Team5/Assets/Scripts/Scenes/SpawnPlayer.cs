using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPlayer : MonoBehaviour
{
    private void Start()
    {
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

            PlayerControllerSideView sideView = player.GetComponent<PlayerControllerSideView>();
            if (sideView != null)
            {
                sideView.ResetFacing();
                sideView.ResetAnimation();
            }

            PlayerControllerTopDown topView = player.GetComponent<PlayerControllerTopDown>();
            if (sideView != null)
            {
                sideView.ResetAnimation();
            }
        }
        
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
