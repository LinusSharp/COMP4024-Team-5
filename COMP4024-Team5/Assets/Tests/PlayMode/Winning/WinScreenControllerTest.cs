using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class WinScreenControllerTests
{
    private GameObject _playerObject;
    private GameObject _winScreenObject;
    private WinScreenController _winScreenController;
    private PlayerController _playerController;

    [SetUp]
    public void Setup()
    {
        // Create a player object with PlayerController
        _playerObject = new GameObject("Player");
        _playerController = _playerObject.AddComponent<PlayerController>();

        // Create the win screen object with WinScreenController
        _winScreenObject = new GameObject("WinScreen");
        _winScreenController = _winScreenObject.AddComponent<WinScreenController>();
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up
        if (_playerObject != null)
            Object.Destroy(_playerObject);
        
        if (_winScreenObject != null)
            Object.Destroy(_winScreenObject);
        
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene != SceneManager.GetActiveScene())
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
    }

    // Test ID: 26
    [UnityTest]
    // Test to check if the WinScreenController is enabled
    public IEnumerator WinScreenController_Start_DestroysPlayer()
    {
        // trigger the Start() method
        _winScreenController.enabled = false;
        _winScreenController.enabled = true;
        
        // Wait a frame for the Start method to complete
        yield return null;
        
        // Assert - verify player has been destroyed
        Assert.IsTrue(_playerObject == null || !_playerObject.activeInHierarchy, 
            "Player should be destroyed when win screen starts");
    }
}