using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    private Transform player;
    
    public Vector3 followOffset = new Vector3(0f, 0f, -10f);
    
    // these are the scenes where the camera stays still
    [SerializeField] private string[] followScenes = { "Tutorial", "Level" };
    private bool isFollowing = false;

    // these are the variables to stop the cam going off teh edge of the screen
    public float minX = -10f;
    public float maxX = 10f;

    void Start() 
    {
        // gets the player
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player with tag 'Player' not found!");
        }

        // is the current scene a scene where the cam needs to move
        string currentScene = SceneManager.GetActiveScene().name;
        foreach (string sceneName in followScenes)
        {
            if (currentScene.Equals(sceneName))
            {
                isFollowing = true;
                break;
            }
        }
    }

    void LateUpdate()
    {
        if (isFollowing && player != null)
        {
            float targetX = player.position.x + followOffset.x;
            targetX = Mathf.Clamp(targetX, minX, maxX);
            transform.position = new Vector3(targetX, transform.position.y, followOffset.z);
        }
    }
}
