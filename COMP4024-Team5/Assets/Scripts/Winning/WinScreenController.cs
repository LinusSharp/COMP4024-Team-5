using UnityEngine;
/// <summary>
/// Controls the win screen behaviour..
/// </summary>
public class WinScreenController : MonoBehaviour
{ 
    /// <summary>
    /// Called on script start. Destroys the player object if it already  exists.
    /// </summary>
    private void Start()
    {
        PlayerController player = Object.FindFirstObjectByType<PlayerController>();
        if (player != null)
        {
            Destroy(player.gameObject);
            Debug.Log("Destroyed the Player object in the winning screen.");
        }
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
