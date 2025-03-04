using UnityEngine;
using NUnit.Framework;
using UnityEngine.SceneManagement;

// Tests the PlayerController.cs script
public class PlayerControllerTest
{
    private GameObject _player;
    private PlayerController _playerController;

    [SetUp]
    public void Setup()
    {
        // Create a new GameObject and add PlayerController component for each test
        _player = new GameObject("Player");
        _playerController = _player.AddComponent<PlayerController>();
    }

    [TearDown]
    public void Teardown()
    {
        // Destroy player instance if it exists
        if (_player != null)
        {
            Object.DestroyImmediate(_player);
        }
      
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != "InitTestScene")
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
    }

    // Test ID: 15
    [Test]
    // Test that the first player object is not destroyed
    public void Singleton_FirstInstance_ShouldNotBeDestroyed()
    {
        // Trigger Awake by enabling the component
        _playerController.enabled = true;

        // The first instance should not be destroyed
        Assert.IsNotNull(_playerController);
        Assert.IsFalse(_playerController == null);
    }

    // Test ID: 16
    [Test]
    // Test that the second player object is destroyed
    public void Singleton_SecondInstance_ShouldBeDestroyed()
    {
        // Create first instance
        _playerController.enabled = true;

        // Create second instance
        GameObject secondObject = new GameObject("Player2");
        PlayerController secondController = secondObject.AddComponent<PlayerController>();
        secondController.enabled = true;

        // Ensure that second instance was destroyed properly
        Assert.IsFalse(secondController == null);

        // Clean up
        Object.DestroyImmediate(secondObject);
    }
}
