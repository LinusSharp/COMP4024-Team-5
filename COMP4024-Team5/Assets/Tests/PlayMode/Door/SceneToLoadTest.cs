using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

// Tests that the sceneToLoad field in DoorTransition is set correctly
public class SceneToLoadTest
{
    // Dictionary of scene transitions (allowing multiple destinations)
    private readonly Dictionary<string, List<string>> _expectedTransitions = new Dictionary<string, List<string>>
    {
        { "Tutorial", new List<string> { "Lobby" } },
        { "Lobby", new List<string> { "LevelSelector" } },
    };
    
    private readonly Dictionary<string, List<string>> _levelTransitions = new Dictionary<string, List<string>>
    {
        { "LevelSelector", new List<string> { "Level 1", "Level 2", "Level 3", "Level 4" } },
    };

    private readonly Dictionary<string, List<string>> _itemTransitions = new Dictionary<string, List<string>>
    {
        { "Level 1", new List<string> { "Lobby" } },
        { "Level 2", new List<string> { "Lobby" } },
        { "Level 3", new List<string> { "Lobby" } },
        { "Level 4", new List<string> { "Lobby" } }
    };
    

    // Test ID: 21
    [UnityTest]
    // Test that the sceneToLoad field for doors in each scene are set correctly
    public IEnumerator DoorTransitions_InAllScenes_HaveCorrectSceneToLoad()
    {
        foreach (var sceneTransition in _expectedTransitions)
        {
            string currentScene = sceneTransition.Key;
            List<string> expectedNextScenes = sceneTransition.Value;

            // Load the scene
            SceneManager.LoadScene(currentScene);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneTransition.Key);
            yield return null;
            
            // Find all doors in the scene
            var doors = Resources.FindObjectsOfTypeAll<DoorTransition>();
            Assert.That(doors, Is.Not.Empty, $"No doors found in scene: {currentScene}");

            foreach (var door in doors)
            {
                // Get the private sceneToLoad field using reflection
                var sceneToLoadField = typeof(DoorTransition).GetField("sceneToLoad",
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance);

                string actualSceneToLoad = (string)sceneToLoadField.GetValue(door);

                Assert.That(expectedNextScenes, Contains.Item(actualSceneToLoad),
                    $"Door in scene '{currentScene}' should transition to one of '{string.Join(", ", expectedNextScenes)}' " +
                    $"but was set to '{actualSceneToLoad}'");
            }
        }
    }
    
    // Test ID: 22
    [UnityTest]
    // Test that all scene names referenced in transitions exist in build settings
    public IEnumerator DoorTransitions_SceneNamesExist()
    {
        // Get all scene names in build settings
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        HashSet<string> availableScenes = new HashSet<string>();

        for (int i = 0; i < sceneCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            availableScenes.Add(sceneName);
        }

        // Check that all scenes referenced in transitions exist
        foreach (var sceneTransition in _expectedTransitions)
        {
            string sourceScene = sceneTransition.Key;
            List<string> targetScenes = sceneTransition.Value;

            Assert.That(availableScenes.Contains(sourceScene), 
                $"Source scene '{sourceScene}' is not in build settings");

            foreach (string targetScene in targetScenes)
            {
                Assert.That(availableScenes.Contains(targetScene), 
                    $"Target scene '{targetScene}' is not in build settings");
            }
        }

        yield return null;
    }
    
    // Test ID: 23
    [UnityTest]
    // Test that the sceneToLoad field for items in each scene are set correctly
    public IEnumerator ItemTransitions_InLevel_LoadLobby()
    {
        foreach (var sceneTransition in _itemTransitions)
        {
            string currentScene = sceneTransition.Key;
            List<string> expectedNextScenes = sceneTransition.Value;

            // Load the scene
            SceneManager.LoadScene(currentScene);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneTransition.Key);
            yield return null;
            
            // Find all doors in the scene
            var items = Resources.FindObjectsOfTypeAll<ItemController>();
            Assert.That(items, Is.Not.Empty, $"No items found in scene: {currentScene}");

            foreach (var item in items)
            {
                // Get the private sceneToLoad field using reflection
                var sceneToLoadField = typeof(ItemController).GetField("nextScene",
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance);

                string actualSceneToLoad = (string)sceneToLoadField.GetValue(item);

                Assert.That(expectedNextScenes, Contains.Item(actualSceneToLoad),
                    $"Item in scene '{currentScene}' should transition to one of '{string.Join(", ", expectedNextScenes)}' " +
                    $"but was set to '{actualSceneToLoad}'");
            }
        }
    }
    
    // Test ID: 24
    [UnityTest]
    // Test that the sceneToLoad field for doors in each scene are set correctly
    public IEnumerator LevelTransitions_InLevelSelector_HaveCorrectSceneToLoad()
    {
        foreach (var sceneTransition in _levelTransitions)
        {
            string currentScene = sceneTransition.Key;
            List<string> expectedNextScenes = sceneTransition.Value;

            // Load the scene
            SceneManager.LoadScene(currentScene);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneTransition.Key);
            yield return null;
            
            // Find all doors in the scene
            var doors = Resources.FindObjectsOfTypeAll<LevelTransition>();
            Assert.That(doors, Is.Not.Empty, $"No doors found in scene: {currentScene}");

            foreach (var door in doors)
            {
                // Get the private sceneToLoad field using reflection
                var sceneToLoadField = typeof(LevelTransition).GetField("sceneToLoad",
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance);

                string actualSceneToLoad = (string)sceneToLoadField.GetValue(door);

                Assert.That(expectedNextScenes, Contains.Item(actualSceneToLoad),
                    $"Door in scene '{currentScene}' should transition to one of '{string.Join(", ", expectedNextScenes)}' " +
                    $"but was set to '{actualSceneToLoad}'");
            }
        }
    }
    
}