using UnityEngine;

public class CamFollower : MonoBehaviour
{
    public GameObject cameraPrefab; // Assign this in the Inspector

    private void Awake()
    {
        if (cameraPrefab == null)
        {
            Debug.LogError("CamFollower: cameraPrefab is NOT assigned! Assign it in the Inspector.");
            return; // Stop execution if missing
        }

        if (GameObject.Find("cam") == null)
        {
            GameObject newCamera = Instantiate(cameraPrefab);
            newCamera.name = "cam"; // Ensure the spawned camera is named correctly
            Debug.Log("CamFollower: Spawned a new 'cam' successfully!");
        }
        else
        {
            Debug.Log("CamFollower: 'cam' already exists, no need to spawn.");
        }
    }
}