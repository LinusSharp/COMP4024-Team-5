using UnityEngine;

public class WinScreenController : MonoBehaviour
{
    private void Start()
    {
        PlayerController player = Object.FindFirstObjectByType<PlayerController>();
        if (player != null)
        {
            Destroy(player.gameObject);
            Debug.Log("Destroyed the Player object in the winning screen.");
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
