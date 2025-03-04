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
