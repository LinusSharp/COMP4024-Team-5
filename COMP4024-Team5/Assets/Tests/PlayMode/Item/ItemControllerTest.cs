using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Reflection;

// Tests the ItemController.cs script
public class ItemControllerTest
{
    private GameObject _player;
    private GameObject _item;
    private ItemController _itemController;
    private GameObject _playerParent;
    
    [SetUp]
    public void SetUp()
    {
        // Create a parent object first
        _playerParent = new GameObject("PlayerParent");
        Object.DontDestroyOnLoad(_playerParent);
        
        // Create a mock player as a child of the parent
        _player = new GameObject("Player");
        _player.tag = "Player";
        _player.AddComponent<BoxCollider2D>();
    
        // Add the PlayerController component to the player parent
        PlayerController playerController = _playerParent.AddComponent<PlayerController>();
    
        // Set the player's parent
        _player.transform.SetParent(_playerParent.transform);
        Object.DontDestroyOnLoad(_player);

        // Create a mock item
        _item = new GameObject("Item");
        var itemCollider = _item.AddComponent<BoxCollider2D>();
        itemCollider.isTrigger = true;
        _itemController = _item.AddComponent<ItemController>();
        Object.DontDestroyOnLoad(_item);
    
        // Set the next scene field on the ItemController if it's not already set
        var nextSceneField = typeof(ItemController).GetField("nextScene", 
            BindingFlags.NonPublic | BindingFlags.Instance);
        if (nextSceneField != null)
        {
            nextSceneField.SetValue(_itemController, "Lobby");
        }
    }

    [TearDown]
    public void TearDown()
    {
        // Load a new scene first to avoid "unloading last scene" error
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
        
        // Destroy all game objects created for the test
        if (_player != null) Object.Destroy(_player);
        if (_item != null) Object.Destroy(_item);
        if (_playerParent != null) Object.Destroy(_playerParent);
        
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != "InitTestScene") 
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
    }
    
    private IEnumerator LoadTestScene(string sceneName)
    {
        // load the Tutorial scene as it contains necessary setup
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
        
        yield return null;
    }
    
    // Test ID: 11
    [UnityTest]
    // Tests that the player transitions to the lobby scene when they collect an item
    public IEnumerator Player_CollectsItem_TransitionsToLobby()
    {
        // load Level 1
        yield return LoadTestScene("Level 1");

        // Make sure our objects are available
        Assert.IsNotNull(_player, "Player was not properly set up.");
        Assert.IsNotNull(_item, "Item was not properly set up.");
        Assert.IsNotNull(_itemController, "ItemController component is missing.");
        
        //set the itemCollected flag to true
        FieldInfo collectedField = typeof(ItemController).GetField("itemCollected", 
            BindingFlags.NonPublic | BindingFlags.Instance);
        collectedField.SetValue(_itemController, true);
        
        //call BackToLobby directly
        _itemController.BackToLobby();

        // Wait for scene transition
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Lobby");

        // Verify the scene transition
        string finalScene = SceneManager.GetActiveScene().name;
        Assert.AreEqual("Lobby", finalScene, "Scene did not transition to Lobby");
    }
}