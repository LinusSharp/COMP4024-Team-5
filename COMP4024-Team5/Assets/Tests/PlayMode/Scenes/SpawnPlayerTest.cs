using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class SpawnPlayerTest
{
    private GameObject _player;
    
    [UnitySetUp]
    public IEnumerator Setup()
    {
        // Create a test player prefab with the "Player" tag
        _player = new GameObject("Player");
        _player.tag = "Player";
        _player.AddComponent<Rigidbody2D>();
        
        // Make this object persist between scene loads
        Object.DontDestroyOnLoad(_player);
        
        yield return null;
    }
    
    [TearDown]
    public void Teardown()
    {
        // Clean up the player prefab
        if (_player != null)
        {
            Object.Destroy(_player);
        }
    }
    
    [UnityTest]
    public IEnumerator Test_PlayerIsSpawnedAtSpawnPointInLevel1()
    {
        // Load the Level 1 scene
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
        SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
        
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Level 1");
        
        // Find the spawn point in the scene
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn");
        Assert.IsNotNull(spawnPoint, "Spawn point should exist in Level 1");
        
        double spawnPointX = spawnPoint.transform.position.x;
        double spawnPointY = spawnPoint.transform.position.y;
        
        double playerX = _player.transform.position.x;
        double playerY = _player.transform.position.y;
        
        // Check if the player's position matches the spawn point position
        Assert.AreEqual(spawnPointX,playerX, 0.1f, "Player should be at spawn point position  X");
        Assert.AreEqual(spawnPointY,playerY, 0.1f, "Player should be at spawn point position  Y");
    }
    
    [UnityTest]
    public IEnumerator Test_PhysicsIsReset()
    {
        // Apply some velocity to the player before loading the scene
        Rigidbody2D rb = _player.GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(10f, 5f);
        rb.angularVelocity = 2f;
        
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
        SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
        
        yield return new WaitForSeconds(0.1f);
        
        
        
        // Check if the velocities were reset
        float velocityMagnitude = rb.linearVelocity.magnitude;
        Assert.IsTrue(velocityMagnitude < 1f, 
            $"Player velocity should be reset. Current magnitude: {velocityMagnitude}");
    
        // Check x and y components separately with tolerance
        Assert.AreEqual(0f, rb.linearVelocity.x, 1f, "Player velocity.x should be reset");
        Assert.AreEqual(0f, rb.linearVelocity.y, 1f, "Player velocity.y should be reset");
    
        // For angular velocity (which is just a float), we can use regular AreEqual with tolerance
        Assert.AreEqual(0f, rb.angularVelocity, 1f, "Player angular velocity should be reset");
    }
}