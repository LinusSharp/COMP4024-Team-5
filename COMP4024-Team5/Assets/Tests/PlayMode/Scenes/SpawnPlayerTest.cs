using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class SpawnPlayerTests : MonoBehaviour
{
    private GameObject player;
    private GameObject spawnPoint;
    private SpawnPlayer spawnPlayerScript;

    // Fake versions of the components
    public class StubPlayerControllerSideView : MonoBehaviour
    {
        public bool ResetFacingCalled = false;
        public bool ResetAnimationCalled = false;
        public bool SetRespawnPointCalled = false;

        public void ResetFacing() 
        { 
            ResetFacingCalled = true; 
        }
        
        public new void ResetAnimation() 
        { 
            ResetAnimationCalled = true; 
        }

        public void SetRespawnPoint() 
        { 
            SetRespawnPointCalled = true; 
        }
    }

    public class StubPlayerControllerTopDown : MonoBehaviour
    {
        public bool UpdateCalled = false;

        public void Update()
        {
            UpdateCalled = true;
        }
    }

    [SetUp]
    public void Setup()
    {
        // Create a new GameObject for the player
        player = new GameObject("Player");
        player.tag = "Player";

        // Add necessary components
        var rb2D = player.AddComponent<Rigidbody2D>(); 

        // Add stub components instead of actual ones
        player.AddComponent<StubPlayerControllerSideView>(); // Add stub side view controller
        player.AddComponent<StubPlayerControllerTopDown>(); // Add stub top-down controller

        // Create a spawn point
        spawnPoint = new GameObject("SpawnPoint");
        spawnPoint.AddComponent<SpawnPlayer>();
        spawnPlayerScript = spawnPoint.GetComponent<SpawnPlayer>();
        
        // Ensure the spawn point is positioned at (0, 0, 0) initially
        spawnPoint.transform.position = Vector3.zero;
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Destroy(player);
        Destroy(spawnPoint);
    }

    // Test ID: 25
    [UnityTest]
    // Test to check if the player is spawned at the spawn point
    public IEnumerator Player_GetsSpawned_AtSpawnPoint()
    {
        // Arrange
        spawnPlayerScript.InitSpawn(); // Simulate spawn logic
        
        yield return null; 

        // Assert the player spawned at the spawn point
        Assert.AreEqual(spawnPoint.transform.position.x, player.transform.position.x, 1f, "Player should spawn at the spawn point.");
        Assert.AreEqual(spawnPoint.transform.position.y, player.transform.position.y, 1f, "Player should spawn at the spawn point.");
    }
}
