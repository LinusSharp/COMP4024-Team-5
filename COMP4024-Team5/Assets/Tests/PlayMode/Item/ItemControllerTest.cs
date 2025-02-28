using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

using System.Reflection;

// Tests the DoorTransition.cs script
public class ItemControllerTest
{
    private GameObject _player;
    private GameObject _item;
    private ItemController _itemController;

    [SetUp]
    public void SetUp()
    {
        // Create a mock player
        _player = new GameObject("Player");
        _player.tag = "Player";
        _player.AddComponent<BoxCollider2D>();
        Object.DontDestroyOnLoad(_player);

        // Create a mock door
        _item = new GameObject("Item");
        var itemCollider = _item.AddComponent<BoxCollider2D>();
        itemCollider.isTrigger = true;
        _itemController = _item.AddComponent<ItemController>();

        Object.DontDestroyOnLoad(_item);
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(_player);
        Object.Destroy(_item);
        
    }

    [UnityTest]
    // Test that next scene is loaded when player enters door
    public IEnumerator Player_CollectsItem_TransitionsToLobby()
    {
        // Load Tutorial Scene
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
        SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
        
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Level 1");
        
        MethodInfo triggerMethod = typeof(ItemController).GetMethod("OnTriggerEnter2D", 
            BindingFlags.NonPublic | BindingFlags.Instance);
        
        triggerMethod.Invoke(_itemController, new object[] { _player.GetComponent<Collider2D>() });
        
        // Load next scene - should be Lobby from Tutorial
        yield return new WaitForSeconds(0.5f);
        
        string finalScene = SceneManager.GetActiveScene().name;
        Assert.AreEqual("Lobby", finalScene, "Scene did not transition to Lobby");
    }
}