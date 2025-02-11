using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

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
        Object.Destroy(_player);
        Object.Destroy(_door);
    }

    [UnityTest]
    // Test that next scene is loaded when player enters door
    public IEnumerator Player_EntersDoor_TransitionsToNextScene()
    {
        // Load Tutorial Scene
        SceneManager.LoadScene("Tutorial");
        yield return new WaitForSeconds(0.1f);
        
        string startingScene = SceneManager.GetActiveScene().name;
        Assert.AreEqual("Tutorial", startingScene, "Test didn't start in Tutorial scene");
        
        MethodInfo triggerMethod = typeof(DoorTransition).GetMethod("OnTriggerEnter2D", 
            BindingFlags.NonPublic | BindingFlags.Instance);
        
        triggerMethod.Invoke(_doorTransition, new object[] { _player.GetComponent<Collider2D>() });
        
        // Load next scene - should be Lobby from Tutorial
        yield return new WaitForSeconds(0.5f);
        
        string finalScene = SceneManager.GetActiveScene().name;
        Assert.AreEqual("Lobby", finalScene, "Scene did not transition to Lobby");
    }

    [UnityTest]
    // Test that player object is preserved when transitioning to next scene
    public IEnumerator DoorTransition_PreservesPlayerObject_OnNextScene()
    {
        SceneManager.LoadScene("Tutorial");
        yield return new WaitForSeconds(0.1f);
        
        Assert.IsTrue(_player.activeSelf, "Player should be active before transition");

        // Trigger the transition
        MethodInfo triggerMethod = typeof(DoorTransition).GetMethod("OnTriggerEnter2D", 
            BindingFlags.NonPublic | BindingFlags.Instance);
        triggerMethod.Invoke(_doorTransition, new object[] { _player.GetComponent<Collider2D>() });

        yield return new WaitForSeconds(0.5f);
        
        Assert.IsTrue(_player.activeSelf, "Player should remain active after transition");
        Assert.IsNotNull(GameObject.FindGameObjectWithTag("Player"), "Player object should exist after transition");
    }

    [UnityTest]
    // Test that the private sceneToLoad field is being assigned
    public IEnumerator SceneToLoadField_isBeingSet()
    {
        yield return null;

        var field = _doorTransition.GetType().GetField("sceneToLoad",
            BindingFlags.NonPublic | BindingFlags.Instance);
        string sceneToLoad = (string)field.GetValue(_doorTransition);
        
        Assert.AreEqual("Lobby", sceneToLoad, "Door's sceneToLoad field is not set to correctly");
    }
}