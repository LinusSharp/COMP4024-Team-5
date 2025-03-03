using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Collections;

public class CameraControllerTests
{
    private GameObject _camera;
    private CameraController _cameraController;
    private GameObject _player;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        // Create camera object with controller and proper tag
        _camera = new GameObject("Camera");
        _camera.tag = "MainCamera"; 
        _cameraController = _camera.AddComponent<CameraController>();
        
        // Set up the follow offset and boundaries
        _cameraController.followOffset = new Vector3(0f, 0f, -10f);
        _cameraController.minX = -10f;
        _cameraController.maxX = 10f;

        // Create player object with required tag
        _player = new GameObject("Player");
        _player.tag = "Player";

        // Set initial positions
        _camera.transform.position = new Vector3(0f, 0f, -10f);
        _player.transform.position = Vector3.zero;
        
        yield return null;
    }

    [TearDown]
    public void Teardown()
    {
        // Properly clean up all created objects
        if (_camera != null)
            Object.DestroyImmediate(_camera);
        
        if (_player != null)
            Object.DestroyImmediate(_player);
            
        // Unload all scenes that might have been loaded during the test
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != "TestScene")
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
    }
    
    [OneTimeTearDown]
    public void OneTimeTeardown()
    {
        // This runs after all tests, ensure any leftover scenes are unloaded
        string[] scenesToUnload = new string[] {
            "Tutorial", "Level 1", "Level 2", "Level 3", "Level 4", "Lobby", "LevelSelector"
        };
        
        foreach (string sceneName in scenesToUnload)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name == sceneName)
                {
                    SceneManager.UnloadSceneAsync(sceneName);
                    break;
                }
            }
        }
    }

    private IEnumerator LoadTestScene(string sceneName)
    {
        // First unload any existing scenes except the test scene
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != "TestScene") // Don't unload the test runner scene
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
        
        // Wait a frame for unloading to complete
        yield return null;
        
        // First load the Tutorial scene as it contains necessary setup
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Tutorial");
        yield return new WaitForSeconds(0.2f);
        
        // If we're testing a different scene, load that now
        if (sceneName != "Tutorial")
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);
            yield return new WaitForSeconds(0.2f);
        }
        
        // Find the necessary objects in the scene
        _cameraController = Object.FindFirstObjectByType<CameraController>();
        _player = GameObject.FindGameObjectWithTag("Player");
        
        // Basic validation
        Assert.IsNotNull(_cameraController, $"CameraController not found in {sceneName} scene.");
        Assert.IsNotNull(_player, $"Player not found in {sceneName} scene.");
        
        yield return null;
    }

    [UnityTest]
    public IEnumerator CameraFollowsPlayerInTutorialScene()
    {
        yield return LoadTestScene("Tutorial");
        
        // Record initial position
        Vector3 initialCameraPos = _cameraController.transform.position;
        Assert.AreEqual(-10.7f, initialCameraPos.x, 0.1f, "Camera should be at initial position.");
        
        // Move player and check camera follows
        _player.transform.position = new Vector3(5f, 0f, 0f);
        yield return new WaitForFixedUpdate();
        yield return null; // Extra frame to ensure camera movement
        
        Assert.AreEqual(5f, _cameraController.transform.position.x, 0.1f, "Camera should have moved to follow player.");
    }
    
    [UnityTest]
    public IEnumerator CameraFollowsPlayerInLevel1Scene()
    {
        yield return LoadTestScene("Level 1");
        
        // Move player and check camera follows
        _player.transform.position = new Vector3(-1f, 0f, 0f);
        yield return new WaitForFixedUpdate();
        yield return null;
        
        Assert.AreEqual(-1f, _cameraController.transform.position.x, 0.1f, "Camera should be at initial position.");
        
        _player.transform.position = new Vector3(10f, 0f, 0f);
        yield return new WaitForFixedUpdate();
        yield return null;
        
        Assert.AreEqual(10f, _cameraController.transform.position.x, 0.1f, "Camera should have moved to follow player.");
    }
    
    [UnityTest]
    public IEnumerator CameraFollowsPlayerInLevel2Scene()
    {
        yield return LoadTestScene("Level 2");
        
        // Move player and check camera follows
        _player.transform.position = new Vector3(-1f, 0f, 0f);
        yield return new WaitForFixedUpdate();
        yield return null;
        
        Assert.AreEqual(-1f, _cameraController.transform.position.x, 0.1f, "Camera should be at initial position.");
        
        _player.transform.position = new Vector3(10f, 0f, 0f);
        yield return new WaitForFixedUpdate();
        yield return null;
        
        Assert.AreEqual(10f, _cameraController.transform.position.x, 0.1f, "Camera should have moved to follow player.");
    }
    
    [UnityTest]
    public IEnumerator CameraFollowsPlayerInLevel3Scene()
    {
        yield return LoadTestScene("Level 3");
        
        // Move player and check camera follows
        _player.transform.position = new Vector3(-1f, 0f, 0f);
        yield return new WaitForFixedUpdate();
        yield return null;
        
        Assert.AreEqual(-1f, _cameraController.transform.position.x, 0.1f, "Camera should be at initial position.");
        
        _player.transform.position = new Vector3(10f, 0f, 0f);
        yield return new WaitForFixedUpdate();
        yield return null;
        
        Assert.AreEqual(10f, _cameraController.transform.position.x, 0.1f, "Camera should have moved to follow player.");
    }
    
    [UnityTest]
    public IEnumerator CameraFollowsPlayerInLevel4Scene()
    {
        yield return LoadTestScene("Level 4");
        
        // Move player and check camera follows
        _player.transform.position = new Vector3(-1f, 0f, 0f);
        yield return new WaitForFixedUpdate();
        yield return null;
        
        Assert.AreEqual(-1f, _cameraController.transform.position.x, 0.1f, "Camera should be at initial position.");
        
        _player.transform.position = new Vector3(10f, 0f, 0f);
        yield return new WaitForFixedUpdate();
        yield return null;
        
        Assert.AreEqual(10f, _cameraController.transform.position.x, 0.1f, "Camera should have moved to follow player.");
    }

    [UnityTest]
    public IEnumerator CameraStaysStillInLobbyScene()
    {
        yield return LoadTestScene("Lobby");
        
        // Record initial camera position
        Vector3 initialCameraPos = _cameraController.transform.position;
        Assert.AreEqual(0f, initialCameraPos.x, 0.1f, "Camera should be at initial position.");
        
        // Move player and verify camera doesn't move
        _player.transform.position = new Vector3(-6f, 0f, 0f);
        yield return new WaitForFixedUpdate();
        yield return null;
        
        _player.transform.position = new Vector3(2f, 0f, 0f);
        yield return new WaitForFixedUpdate();
        yield return null;
        
        Assert.AreEqual(0f, _cameraController.transform.position.x, 0.1f, "Camera should not have moved.");
    }
    
    [UnityTest]
    public IEnumerator CameraStaysStillInLevelSelectorScene()
    {
        yield return LoadTestScene("LevelSelector");
        
        // Record initial camera position
        Vector3 initialCameraPos = _cameraController.transform.position;
        Assert.AreEqual(0f, initialCameraPos.x, 0.1f, "Camera should be at initial position.");
        
        // Move player and verify camera doesn't move
        _player.transform.position = new Vector3(-6f, 0f, 0f);
        yield return new WaitForFixedUpdate();
        yield return null;
        
        _player.transform.position = new Vector3(2f, 0f, 0f);
        yield return new WaitForFixedUpdate();
        yield return null;
        
        Assert.AreEqual(0f, _cameraController.transform.position.x, 0.1f, "Camera should not have moved.");
    }
}