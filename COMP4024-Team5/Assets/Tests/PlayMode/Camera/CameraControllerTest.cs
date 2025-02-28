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
        if (_camera != null)
            Object.DestroyImmediate(_camera);
        if (_player != null)
            Object.DestroyImmediate(_player);
    }

    [UnityTest]
    public IEnumerator CameraFollowsPlayerInTutorialScene()
    {
        SceneManager.LoadScene("Tutorial");
        yield return new WaitForSeconds(1f);
        
        _cameraController = GameObject.FindFirstObjectByType<CameraController>();
        _player = GameObject.FindGameObjectWithTag("Player");
    
        Assert.IsNotNull( _cameraController, "CameraController not found in scene.");
        Assert.IsNotNull(_player, "Player not found in scene.");
        Assert.AreEqual(-10.7f,  _cameraController.transform.position.x, 0.1f, "Camera in initial position.");
    
        _player.transform.position = new Vector3(5f, 0f, 0f);
        yield return new WaitForFixedUpdate();
    
        Assert.AreEqual(5f,  _cameraController.transform.position.x, 0.1f, "Camera should have moved.");
    
    }
    
    [UnityTest]
    public IEnumerator CameraFollowsPlayerInLevel1Scene()
    {
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
        SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
        
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Level 1");
    
        // Check if player exists (since it's spawned dynamically in normal gameplay)
        _player = GameObject.FindGameObjectWithTag("Player");
    
        // Find CameraController
        _cameraController = GameObject.FindFirstObjectByType<CameraController>();
        Assert.IsNotNull( _cameraController, "CameraController not found in scene.");
        Assert.IsNotNull(_player, "Player not found in scene.");
        
        // Move player and check camera follows
        _player.transform.position = new Vector3(-1, 0f, 0f);
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(-1f,  _cameraController.transform.position.x, 0.1f, "Camera in initial position.");
        // yield return new WaitForSeconds(1f);
        _player.transform.position = new Vector3(10, 0f, 0f);
        yield return new WaitForFixedUpdate();
        // yield return new WaitForSeconds(1f);
    
        Assert.AreEqual(10,  _cameraController.transform.position.x, 0.1f, "Camera should have moved.");
    }
    
    [UnityTest]
    public IEnumerator CameraFollowsPlayerInLevel2Scene()
    {
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
        SceneManager.LoadScene("Level 2", LoadSceneMode.Single);
        
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Level 2");
    
        // Check if player exists (since it's spawned dynamically in normal gameplay)
        _player = GameObject.FindGameObjectWithTag("Player");
    
        // Find CameraController
        _cameraController = GameObject.FindFirstObjectByType<CameraController>();
        Assert.IsNotNull( _cameraController, "CameraController not found in scene.");
        Assert.IsNotNull(_player, "Player not found in scene.");
        
        // Move player and check camera follows
        _player.transform.position = new Vector3(-1, 0f, 0f);
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(-1f,  _cameraController.transform.position.x, 0.1f, "Camera in initial position.");
        // yield return new WaitForSeconds(1f);
        _player.transform.position = new Vector3(10, 0f, 0f);
        yield return new WaitForFixedUpdate();
        // yield return new WaitForSeconds(1f);
    
        Assert.AreEqual(10,  _cameraController.transform.position.x, 0.1f, "Camera should have moved.");
    }
    
    [UnityTest]
    public IEnumerator CameraFollowsPlayerInLevel3Scene()
    {
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
        SceneManager.LoadScene("Level 3", LoadSceneMode.Single);
        
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Level 3");
    
        // Check if player exists (since it's spawned dynamically in normal gameplay)
        _player = GameObject.FindGameObjectWithTag("Player");
    
        // Find CameraController
        _cameraController = GameObject.FindFirstObjectByType<CameraController>();
        Assert.IsNotNull( _cameraController, "CameraController not found in scene.");
        Assert.IsNotNull(_player, "Player not found in scene.");
        
        // Move player and check camera follows
        _player.transform.position = new Vector3(-1, 0f, 0f);
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(-1f,  _cameraController.transform.position.x, 0.1f, "Camera in initial position.");
        // yield return new WaitForSeconds(1f);
        _player.transform.position = new Vector3(10, 0f, 0f);
        yield return new WaitForFixedUpdate();
        // yield return new WaitForSeconds(1f);
    
        Assert.AreEqual(10,  _cameraController.transform.position.x, 0.1f, "Camera should have moved.");
    }
    
    [UnityTest]
    public IEnumerator CameraFollowsPlayerInLevel4Scene()
    {
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
        SceneManager.LoadScene("Level 4", LoadSceneMode.Single);
        
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Level 4");
    
        // Check if player exists (since it's spawned dynamically in normal gameplay)
        _player = GameObject.FindGameObjectWithTag("Player");
    
        // Find CameraController
        _cameraController = GameObject.FindFirstObjectByType<CameraController>();
        Assert.IsNotNull( _cameraController, "CameraController not found in scene.");
        Assert.IsNotNull(_player, "Player not found in scene.");
        
        // Move player and check camera follows
        _player.transform.position = new Vector3(-1, 0f, 0f);
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(-1f,  _cameraController.transform.position.x, 0.1f, "Camera in initial position.");
        // yield return new WaitForSeconds(1f);
        _player.transform.position = new Vector3(10, 0f, 0f);
        yield return new WaitForFixedUpdate();
        // yield return new WaitForSeconds(1f);
    
        Assert.AreEqual(10,  _cameraController.transform.position.x, 0.1f, "Camera should have moved.");
    }

    [UnityTest]
    public IEnumerator CameraStaysStillInLobbyScene()
    {
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
        SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
        
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Lobby");
    
        // Check if player exists (since it's spawned dynamically in normal gameplay)
        _player = GameObject.FindGameObjectWithTag("Player");
    
        // Find CameraController
        _cameraController = GameObject.FindFirstObjectByType<CameraController>();
        Assert.IsNotNull( _cameraController, "CameraController not found in scene.");
        Assert.IsNotNull(_player, "Player not found in scene.");
        Assert.AreEqual(0,  _cameraController.transform.position.x, 0.1f, "Camera in initial position.");
        
        // Move player and check camera follows
        _player.transform.position = new Vector3(-6, 0f, 0f);
        yield return new WaitForFixedUpdate();
        // yield return new WaitForSeconds(1f);
        _player.transform.position = new Vector3(2, 0f, 0f);
        yield return new WaitForFixedUpdate();
        // yield return new WaitForSeconds(1f);
    
        Assert.AreEqual(0,  _cameraController.transform.position.x, 0.1f, "Camera should have moved.");
    }
    
    [UnityTest]
    public IEnumerator CameraStaysStillInLevelSelectorScene()
    {
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
        SceneManager.LoadScene("LevelSelector", LoadSceneMode.Single);
        
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "LevelSelector");
    
        // Check if player exists (since it's spawned dynamically in normal gameplay)
        _player = GameObject.FindGameObjectWithTag("Player");
    
        // Find CameraController
        _cameraController = GameObject.FindFirstObjectByType<CameraController>();
        Assert.IsNotNull( _cameraController, "CameraController not found in scene.");
        Assert.IsNotNull(_player, "Player not found in scene.");
        Assert.AreEqual(0f,  _cameraController.transform.position.x, 0.1f, "Camera in initial position.");
        
        // Move player and check camera follows
        _player.transform.position = new Vector3(-6, 0f, 0f);
        yield return new WaitForFixedUpdate();
        // yield return new WaitForSeconds(1f);
        _player.transform.position = new Vector3(2, 0f, 0f);
        yield return new WaitForFixedUpdate();
        // yield return new WaitForSeconds(1f);
    
        Assert.AreEqual(0,  _cameraController.transform.position.x, 0.1f, "Camera should have moved.");
    }
}