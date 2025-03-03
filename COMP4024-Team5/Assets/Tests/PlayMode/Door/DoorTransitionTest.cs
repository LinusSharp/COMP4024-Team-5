using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Reflection;

// Tests the DoorTransition.cs script
public class DoorTransitionTest
{
    private GameObject _player;
    private GameObject _door;
    private DoorTransition _doorTransition;

    [SetUp]
    public void SetUp()
    {
        // Create a mock player
        _player = new GameObject("Player");
        _player.tag = "Player";
        _player.AddComponent<BoxCollider2D>();
        Object.DontDestroyOnLoad(_player);

        // Create a mock door
        _door = new GameObject("Door");
        var doorCollider = _door.AddComponent<BoxCollider2D>();
        doorCollider.isTrigger = true;
        _doorTransition = _door.AddComponent<DoorTransition>();

        // Set private sceneToLoad field
        var sceneToLoadField = _doorTransition.GetType().GetField("sceneToLoad",
            BindingFlags.NonPublic | BindingFlags.Instance);
        sceneToLoadField.SetValue(_doorTransition, "Lobby");

        Object.DontDestroyOnLoad(_door);
    }

    [TearDown]
    public void TearDown()
    {
        // Properly destroy test objects
        if (_player != null)
            Object.DestroyImmediate(_player);
            
        if (_door != null)
            Object.DestroyImmediate(_door);
            
        // Unload all scenes except the test scene
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != "InitTestScene") // Don't unload the test runner scene
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
    }
    
    [OneTimeTearDown]
    public void OneTimeTeardown()
    {
        // Ensure all test scenes are unloaded after all tests
        string[] scenesToUnload = new string[] { "Tutorial", "Lobby" };
        
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
    
    private IEnumerator LoadSceneAndWait(string sceneName)
    {
        // Unload any previous scenes except test scene
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != "InitTestScene") // Don't unload the test runner scene
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
        
        // Wait for unloading to complete
        yield return null;
        
        // Load the requested scene
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        
        // Wait until the scene is fully loaded
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);
        yield return new WaitForSeconds(0.2f); // Extra time for scene to initialize
    }

    [UnityTest]
    // Test that next scene is loaded when player enters door
    public IEnumerator Player_EntersDoor_TransitionsToNextScene()
    {
        // Load Tutorial Scene
        yield return LoadSceneAndWait("Tutorial");
        
        string startingScene = SceneManager.GetActiveScene().name;
        Assert.AreEqual("Tutorial", startingScene, "Test didn't start in Tutorial scene");
        
        // Get the OnTriggerEnter2D method using reflection
        MethodInfo triggerMethod = typeof(DoorTransition).GetMethod("OnTriggerEnter2D", 
            BindingFlags.NonPublic | BindingFlags.Instance);
        
        // Trigger the door transition
        triggerMethod.Invoke(_doorTransition, new object[] { _player.GetComponent<Collider2D>() });
        
        // Wait for scene transition
        yield return new WaitForSeconds(0.5f);
        
        // Verify we're in the correct scene
        string finalScene = SceneManager.GetActiveScene().name;
        Assert.AreEqual("Lobby", finalScene, "Scene did not transition to Lobby");
    }

    [UnityTest]
    // Test that player object is preserved when transitioning to next scene
    public IEnumerator DoorTransition_PreservesPlayerObject_OnNextScene()
    {
        yield return LoadSceneAndWait("Tutorial");
        
        Assert.IsTrue(_player.activeSelf, "Player should be active before transition");

        // Trigger the transition
        MethodInfo triggerMethod = typeof(DoorTransition).GetMethod("OnTriggerEnter2D", 
            BindingFlags.NonPublic | BindingFlags.Instance);
        triggerMethod.Invoke(_doorTransition, new object[] { _player.GetComponent<Collider2D>() });

        // Wait for scene transition
        yield return new WaitForSeconds(0.5f);
        
        Assert.IsTrue(_player.activeSelf, "Player should remain active after transition");
        Assert.IsNotNull(GameObject.FindGameObjectWithTag("Player"), "Player object should exist after transition");
    }

    [UnityTest]
    // Test that the private sceneToLoad field is being assigned
    public IEnumerator SceneToLoad_Field_IsBeingSet()
    {
        yield return null;

        var field = _doorTransition.GetType().GetField("sceneToLoad",
            BindingFlags.NonPublic | BindingFlags.Instance);
        string sceneToLoad = (string)field.GetValue(_doorTransition);
        
        Assert.AreEqual("Lobby", sceneToLoad, "Door's sceneToLoad field is not set correctly");
    }
}