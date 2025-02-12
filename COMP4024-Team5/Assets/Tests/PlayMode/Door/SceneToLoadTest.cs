using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

// Tests that the sceneToLoad field in DoorTransition is set correctly
public class SceneToLoadTest
{
    // Dictionary of scene transitions
    private readonly Dictionary<string, string> expectedTransitions = new Dictionary<string, string>
    {
        { "Tutorial", "Lobby" },
        { "Lobby", "LevelSelector" },
        { "LevelSelector", "Level" },
        { "Level", "Lobby" }
    };

    [UnityTest]
    // Test that the sceneToLoad field is set correctly in all scenes
    public IEnumerator DoorTransitions_InAllScenes_HaveCorrectSceneToLoad()
    {
        foreach (var sceneTransition in expectedTransitions)
        {
            // Load the scene
            SceneManager.LoadScene(sceneTransition.Key);
            yield return new WaitForSeconds(0.1f); // Wait for scene to load

            // Find all doors in the scene
            // var doors = GameObject.FindObjectsOfType<DoorTransition>();
            var doors = GameObject.FindObjectsByType<DoorTransition>(FindObjectsSortMode.None);
            Assert.That(doors, Is.Not.Empty, $"No doors found in scene: {sceneTransition.Key}");

            foreach (var door in doors)
            {
                // Get the private sceneToLoad field using reflection
                var sceneToLoadField = typeof(DoorTransition).GetField("sceneToLoad", 
                    System.Reflection.BindingFlags.NonPublic | 
                    System.Reflection.BindingFlags.Instance);
                
                string actualSceneToLoad = (string)sceneToLoadField.GetValue(door);
                
                Assert.That(actualSceneToLoad, Is.EqualTo(sceneTransition.Value), 
                    $"Door in scene '{sceneTransition.Key}' should transition to '{sceneTransition.Value}' " +
                    $"but was set to '{actualSceneToLoad}'");
            }
        }
    }

    [UnityTest]
    // Test that all scene names in transitions exist in build settings
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
        foreach (var sceneTransition in expectedTransitions)
        {
            Assert.That(availableScenes.Contains(sceneTransition.Key), 
                $"Source scene '{sceneTransition.Key}' is not in build settings");
            Assert.That(availableScenes.Contains(sceneTransition.Value), 
                $"Target scene '{sceneTransition.Value}' is not in build settings");
        }

        yield return null;
    }
}