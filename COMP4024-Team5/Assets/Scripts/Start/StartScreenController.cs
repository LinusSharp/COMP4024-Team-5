using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Controls the start screen behaviour, and handles scene transitions.
/// </summary>
public class StartScreenController : MonoBehaviour
{
    
    /// <summary>
    /// Called when the start button is pressed and load the tutorial scene
    /// </summary
    public void OnStartButtonPressed()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
