using UnityEngine;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using System.Reflection;

public class PlayerMovementSwitcherTests
{
    private GameObject _player;
    private PlayerMovementSwitcher _movementSwitcher;
    private PlayerControllerSideView _sideViewController;
    private PlayerControllerTopDown _topDownController;

    [SetUp]
    public void SetUp()
    {
        // Create a mock player object
        _player = new GameObject("Player");

        // Add necessary components
        _player.AddComponent<Rigidbody2D>();  // Ensuring dependencies
        _player.AddComponent<BoxCollider2D>(); // If needed

        _movementSwitcher = _player.AddComponent<PlayerMovementSwitcher>();
        _sideViewController = _player.AddComponent<PlayerControllerSideView>();
        _topDownController = _player.AddComponent<PlayerControllerTopDown>();

        // Assign references
        _movementSwitcher.sideViewController = _sideViewController;
        _movementSwitcher.topDownController = _topDownController;
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up objects
        Object.Destroy(_player);
    }

    [Test]
    public void OnSceneLoaded_Lobby_EnablesTopDown_DisablesSideView()
    {

        if (SceneManager.GetSceneByName("Lobby").isLoaded)
        {
            SceneManager.LoadScene("Lobby");

            // Get the private method
            MethodInfo method = typeof(PlayerMovementSwitcher)
                .GetMethod("OnSceneLoaded", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.NotNull(method, "Could not find OnSceneLoaded method");

            // Invoke OnSceneLoaded manually
            method.Invoke(_movementSwitcher, new object[] { SceneManager.GetActiveScene(), LoadSceneMode.Single });
        }
        else
        {
            // Get the private method
            MethodInfo method = typeof(PlayerMovementSwitcher)
                .GetMethod("OnSceneLoaded", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.NotNull(method, "Could not find OnSceneLoaded method");

            // Create a mock scene
            Scene mockScene = SceneManager.CreateScene("Lobby");

            // Invoke OnSceneLoaded via reflection
            method.Invoke(_movementSwitcher, new object[] { mockScene, LoadSceneMode.Single });
        }

        // Assert correct controller state
        Assert.IsFalse(_sideViewController.enabled, "Side View should be disabled in Lobby.");
        Assert.IsTrue(_topDownController.enabled, "Top Down should be enabled in Lobby.");
    }
    
    [Test]
    public void OnSceneLoaded_LevelSelector_EnablesTopDown_DisablesSideView()
    {
        // Get the private method
        MethodInfo method = typeof(PlayerMovementSwitcher)
            .GetMethod("OnSceneLoaded", BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.NotNull(method, "Could not find OnSceneLoaded method");

        // Create a mock scene
        Scene mockScene = SceneManager.CreateScene("LevelSelector");

        // Invoke OnSceneLoaded via reflection
        method.Invoke(_movementSwitcher, new object[] { mockScene, LoadSceneMode.Single });

        // Assert correct controller state
        Assert.IsFalse(_sideViewController.enabled, "Side View should be disabled in LevelSelector.");
        Assert.IsTrue(_topDownController.enabled, "Top Down should be enabled in LevelSelector.");
    }
    
    [Test]
    public void OnSceneLoaded_Tutorial_EnablesTopDown_DisablesSideView()
    {
        // Get the private method
        MethodInfo method = typeof(PlayerMovementSwitcher)
            .GetMethod("OnSceneLoaded", BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.NotNull(method, "Could not find OnSceneLoaded method");

        // Create a mock scene
        Scene mockScene = SceneManager.CreateScene("Tutorial");

        // Invoke OnSceneLoaded via reflection
        method.Invoke(_movementSwitcher, new object[] { mockScene, LoadSceneMode.Single });

        // Assert correct controller state
        Assert.IsTrue(_sideViewController.enabled, "Side View should be enabled in Tutorial.");
        Assert.IsFalse(_topDownController.enabled, "Top Down should be disabled in Tutorial.");
    }
    
    [Test]
    public void OnSceneLoaded_Level_EnablesTopDown_DisablesSideView()
    {
        // Get the private method
        MethodInfo method = typeof(PlayerMovementSwitcher)
            .GetMethod("OnSceneLoaded", BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.NotNull(method, "Could not find OnSceneLoaded method");

        // Create a mock scene
        Scene mockScene = SceneManager.CreateScene("Level 1");

        // Invoke OnSceneLoaded via reflection
        method.Invoke(_movementSwitcher, new object[] { mockScene, LoadSceneMode.Single });

        // Assert correct controller state
        Assert.IsTrue(_sideViewController.enabled, "Side View should be enabled in Level 1.");
        Assert.IsFalse(_topDownController.enabled, "Top Down should be disabled in Level 1.");
    }

}
