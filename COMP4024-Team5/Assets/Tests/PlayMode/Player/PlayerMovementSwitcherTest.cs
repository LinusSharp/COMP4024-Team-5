using UnityEngine;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using System.Reflection;
using System.Collections;
using UnityEngine.TestTools;

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
        _player.AddComponent<Rigidbody2D>();
        _player.AddComponent<BoxCollider2D>();

        _movementSwitcher = _player.AddComponent<PlayerMovementSwitcher>();
        _sideViewController = _player.AddComponent<PlayerControllerSideView>();
        _topDownController = _player.AddComponent<PlayerControllerTopDown>();

        // Assign references
        _movementSwitcher.sideViewController = _sideViewController;
        _movementSwitcher.topDownController = _topDownController;
    }

    [TearDown]
    public void Teardown()
    {
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
    
    private IEnumerator LoadTestScene(string sceneName)
    {
        // unload any existing scenes except the test scene
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != "TestScene")
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
        
        // Wait a frame for unloading to complete
        yield return null;
        
        // load the Tutorial scene as it contains necessary setup
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Tutorial");
        yield return new WaitForSeconds(0.2f);
        
        // load alternate scene
        if (sceneName != "Tutorial")
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);
            yield return new WaitForSeconds(0.2f);
        }
        
        _player = GameObject.FindGameObjectWithTag("Player");
        
        Assert.IsNotNull(_player, $"Player not found in {sceneName} scene.");
        
        yield return null;
    }
    
    // Test ID: 17
    [Test]
    // Test to check if the OnSceneLoaded method enables the SideView and disables the TopDown controller when the Level 1 scene is loaded
    public void OnSceneLoaded_Level_DisablesTopDown_EnablesSideView()
    {
        if (SceneManager.GetSceneByName("Level 1").isLoaded)
        {
            SceneManager.LoadScene("Level 1");

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
            Scene mockScene = SceneManager.CreateScene("Level 1");

            // Invoke OnSceneLoaded via reflection
            method.Invoke(_movementSwitcher, new object[] { mockScene, LoadSceneMode.Single });
        }

        // Assert correct controller state
        Assert.IsTrue(_sideViewController.enabled, "Side View should be enabled in Level 1.");
        Assert.IsFalse(_topDownController.enabled, "Top Down should be disabled in Level 1.");
    }
    
    // Test ID: 18
    [Test]
    // Test to check if the OnSceneLoaded method enables the TopDown and disables the SideView controller when the LevelSelector scene is loaded
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
    
    // Test ID: 19
    [Test]
    // Test to check if the OnSceneLoaded method enables the TopDown and disables the SideView controller when the Lobby scene is loaded
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
        
        LoadTestScene("Lobby");

        // Assert correct controller state
        Assert.IsFalse(_sideViewController.enabled, "Side View should be disabled in Lobby.");
        Assert.IsTrue(_topDownController.enabled, "Top Down should be enabled in Lobby.");
    }
    
    // Test ID: 20
    [Test]
    // Test to check if the OnSceneLoaded method disables the TopDown and enables the SideView controller when the Tutorial scene is loaded
    public void OnSceneLoaded_Tutorial_DisablesTopDown_EnablesSideView()
    {
        if (SceneManager.GetSceneByName("Tutorial").isLoaded)
        {
            SceneManager.LoadScene("Tutorial");

            // Get the private method
            MethodInfo method = typeof(PlayerMovementSwitcher)
                .GetMethod("OnSceneLoaded", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.NotNull(method, "Could not find OnSceneLoaded method");

            // Invoke OnSceneLoaded manually
            method.Invoke(_movementSwitcher, new object[] { SceneManager.GetActiveScene(), LoadSceneMode.Single });
            
            // Assert correct controller state
            Assert.IsTrue(_sideViewController.enabled, "Side View should be enabled in Tutorial.");
            Assert.IsFalse(_topDownController.enabled, "Top Down should be disabled in Tutorial.");
        }
    }
    

}