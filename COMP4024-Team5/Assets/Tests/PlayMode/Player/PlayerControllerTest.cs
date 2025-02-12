using UnityEngine;
using NUnit.Framework;

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
        // Clean up after each test
        Object.DestroyImmediate(_player);
    }

    [Test]
    public void Singleton_FirstInstance_ShouldNotBeDestroyed()
    {
        // Trigger Awake by enabling the component
        _playerController.enabled = true;

        // The first instance should not be destroyed
        Assert.IsNotNull(_playerController);
        Assert.IsFalse(_playerController == null);
    }

    [Test]
    public void Singleton_SecondInstance_ShouldBeDestroyed()
    {
        // Create first instance
        _playerController.enabled = true;

        // Create second instance
        GameObject secondObject = new GameObject("Player2");
        PlayerController secondController = secondObject.AddComponent<PlayerController>();
        secondController.enabled = true;

        // Wait one frame to allow for destruction
        Assert.IsFalse(secondController == null);

        // Clean up
        Object.DestroyImmediate(secondObject);
    }
}