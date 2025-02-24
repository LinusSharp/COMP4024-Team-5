using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementSwitcher : MonoBehaviour
{
    public PlayerControllerSideView sideViewController;
    public PlayerControllerTopDown topDownController;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Swaps out the player movement and resets physics.
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Tutorial" || scene.name == "Level")
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
