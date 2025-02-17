using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    private Transform player;
    
    public Vector3 followOffset = new Vector3(0f, 0f, -10f);
    
    // these are the scenes where the camera stays still
    [SerializeField] private string[] followScenes = { "Tutorial", "Level" };
    private bool isFollowing = false;

    // these are the smoothing vairiables
    public float smoothTime = 0.3f;
    private float xVelocity = 0f;

    // this is the deadzone buffer - cam will only move when player goes out of this
    public float buffer = 0.5f;

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
            // calculates the target x position
            float targetX = player.position.x + followOffset.x;
            
            // is the player out of the buffer
            if (Mathf.Abs(transform.position.x - targetX) > buffer)
            {
                float newX = Mathf.SmoothDamp(transform.position.x, targetX, ref xVelocity, smoothTime);
                transform.position = new Vector3(newX, transform.position.y, followOffset.z);
            }
        }
    }
}
