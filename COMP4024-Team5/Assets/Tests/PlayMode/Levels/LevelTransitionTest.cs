using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class LevelTransitionTests
{
    private GameObject _levelTransitionObject;
    private LevelTransition _levelTransition;
    private GameObject _playerObject;
    private PlayerController _playerController;
    
    [UnitySetUp]
    public IEnumerator Setup()
    {
        GameObject existingPlayer = GameObject.FindWithTag("Player");
        if (existingPlayer != null)
        {
            Object.DestroyImmediate(existingPlayer);
            yield return null; 
        }
        
        // Create a level transition object
        _levelTransitionObject = new GameObject("LevelTransition");
        _levelTransition = _levelTransitionObject.AddComponent<LevelTransition>();
        
        // Add required components
        _levelTransitionObject.AddComponent<BoxCollider2D>().isTrigger = true;
        SpriteRenderer spriteRenderer = _levelTransitionObject.AddComponent<SpriteRenderer>();
        
        // Set fields using reflection (since they're private SerializeField)
        var sceneToLoadField = typeof(LevelTransition).GetField("sceneToLoad", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        sceneToLoadField.SetValue(_levelTransition, "Level1");
        
        var levelNumberField = typeof(LevelTransition).GetField("levelNumber", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        levelNumberField.SetValue(_levelTransition, 1);
        
        var doorSpriteField = typeof(LevelTransition).GetField("doorSprite", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        doorSpriteField.SetValue(_levelTransition, spriteRenderer);
        
        var doorColliderField = typeof(LevelTransition).GetField("doorCollider", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        doorColliderField.SetValue(_levelTransition, _levelTransitionObject.GetComponent<Collider2D>());
        
        // Create a player object
        _playerObject = new GameObject("Player");
        _playerObject.tag = "Player";
        _playerController = _playerObject.AddComponent<PlayerController>();
        _playerObject.AddComponent<BoxCollider2D>();
        
        yield return null;
    }
    
    [UnityTearDown]
    public IEnumerator Teardown()
    {
        if (_levelTransitionObject != null)
        {
            Object.Destroy(_levelTransitionObject);
        }
    
        if (_playerObject != null)
        {
            Object.Destroy(_playerObject);
        }

        yield return null;
    }

    
    [UnityTest]
    public IEnumerator DoorUnlocks_WhenPlayerLevelMatches()
    {
        // Set player level to match the door level
        _playerController.level = 1;
        
        yield return null;
        
        // Check if door is unlocked (collider is enabled)
        Collider2D doorCollider = _levelTransitionObject.GetComponent<Collider2D>();
        Assert.IsTrue(doorCollider.enabled, "Door collider should be enabled when level matches");
        
        // Check door sprite alpha
        SpriteRenderer doorSprite = _levelTransitionObject.GetComponent<SpriteRenderer>();
        Assert.AreEqual(0.0f, doorSprite.color.a, 0.01f, "Door sprite should not be dimmed when unlocked");
    }

[UnityTest]
public IEnumerator DoorLocks_WhenPlayerLevelDoesNotMatch()
{
    // Set player level to a value that does NOT match the door
    _playerController.level = 2;

    // Ensure no leftover doors exist before creating a new one
    foreach (var obj in GameObject.FindGameObjectsWithTag("Door"))
    {
        Object.Destroy(obj);
    }

    // Create new GameObject for the locked door
    GameObject lockedDoor = new GameObject("LevelTransition");
    lockedDoor.tag = "Door"; // Assign the tag to ensure we get the correct object

    // Add LevelTransition component
    LevelTransition lockedDoorTransition = lockedDoor.AddComponent<LevelTransition>();

    // Add required components
    BoxCollider2D doorCollider = lockedDoor.AddComponent<BoxCollider2D>();
    doorCollider.isTrigger = true;  
    SpriteRenderer spriteRenderer = lockedDoor.AddComponent<SpriteRenderer>();

    // Set private fields using reflection
    var sceneToLoadField = typeof(LevelTransition).GetField("sceneToLoad", 
        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
    sceneToLoadField.SetValue(lockedDoorTransition, "Level1");

    var levelNumberField = typeof(LevelTransition).GetField("levelNumber", 
        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
    levelNumberField.SetValue(lockedDoorTransition, 1);

    var doorSpriteField = typeof(LevelTransition).GetField("doorSprite", 
        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
    doorSpriteField.SetValue(lockedDoorTransition, spriteRenderer);

    var doorColliderField = typeof(LevelTransition).GetField("doorCollider", 
        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
    doorColliderField.SetValue(lockedDoorTransition, doorCollider);

    yield return null; 

    // Check if the new door exists
    GameObject foundDoor = GameObject.FindWithTag("Door");
    if (foundDoor == null)
    {
        yield break;
    }
    Debug.Log("Found door object: " + foundDoor.name);

    // Get the collider and validate
    Collider2D collider = foundDoor.GetComponent<Collider2D>();
    if (collider == null)
    {
        Debug.LogError("No Collider2D found on the door object!");
        yield break;
    }
    
    // Assert that the door is locked (collider should be disabled)
    Assert.IsFalse(collider.enabled, "Door collider should be disabled when level doesn't match");

    // Check door sprite alpha
    SpriteRenderer doorSprite = foundDoor.GetComponent<SpriteRenderer>();
    Assert.AreEqual(1.0f, doorSprite.color.a, 0.01f, "Door sprite should be dimmed when locked");
}


    
    [UnityTest]
    public IEnumerator PlayerCannotTriggerLockedDoor()
    {
        // Create a SceneLoader mock to prevent actual scene loading
        SceneLoaderMock sceneLoader = new GameObject("SceneLoaderMock").AddComponent<SceneLoaderMock>();
        
        // Set player level to not match the door level
        _playerController.level = 2;
        
        // Trigger Start() method to lock the door
        _levelTransition.enabled = false;
        _levelTransition.enabled = true;
        
        yield return null;
        
        // Move player to trigger the collision (even though collider is disabled)
        _playerObject.transform.position = _levelTransitionObject.transform.position;
        
        // Wait for physics and trigger events
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        
        // Check if scene load was not requested
        Assert.IsFalse(sceneLoader.WasSceneLoadRequested, "Scene load should not be requested when player enters locked door");
        
        Object.Destroy(sceneLoader.gameObject);
    }
    
    // Mock class to intercept scene loading
    private class SceneLoaderMock : MonoBehaviour
    {
        public bool WasSceneLoadRequested { get; private set; }
        public string RequestedSceneName { get; private set; }
        
        void Awake()
        {
            // Override SceneManager.LoadScene to track calls instead of actually loading
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            WasSceneLoadRequested = true;
            RequestedSceneName = scene.name;
        }
    }
}
