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

    // swaps out the player movement
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Tutorial" || scene.name == "Level 1" || scene.name == "Level 2"|| scene.name == "Level 3"|| scene.name == "Level 4")
        {
            sideViewController.enabled = true;
            topDownController.enabled = false;
        }
        else if (scene.name == "Lobby" || scene.name == "LevelSelector")
        {
            sideViewController.enabled = false;
            topDownController.enabled = true;
        }
    }
}
