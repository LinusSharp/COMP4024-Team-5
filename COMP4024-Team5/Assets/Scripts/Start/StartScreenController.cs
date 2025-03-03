using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour
{
    public void OnStartButtonPressed()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
