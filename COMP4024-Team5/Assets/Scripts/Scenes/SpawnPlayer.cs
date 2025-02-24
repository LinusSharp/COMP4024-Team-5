using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPlayer : MonoBehaviour
{
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = transform.position;
        }
        
        Scene scene = gameObject.scene;
        // rename the if to the level names
        if (scene.name == "Tutorial" || scene.name == "Level")
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
