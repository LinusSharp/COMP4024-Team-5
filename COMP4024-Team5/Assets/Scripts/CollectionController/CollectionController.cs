using UnityEngine;
using System.Collections.Generic;

public class CollectionController : MonoBehaviour
{
    // singleton to ensure only one instance is created to keep track of the collected items 
    public static CollectionController Instance;
    private HashSet<string> collectedItems = new HashSet<string>(); // collected items

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            
          PlayerPrefs.DeleteAll();  

            LoadCollectedItems();
        }
        else
        {
            Destroy(gameObject); // destroy duplicate instances if there are any 
        }
    }

    public void CollectItem(string itemKey)
    {
        if (!collectedItems.Contains(itemKey)) // avoinding multilple entries 
        {
            collectedItems.Add(itemKey); // storing item in the memory 
            PlayerPrefs.SetInt(itemKey, 1); // if collection status is 1 it means it is collected
            PlayerPrefs.Save(); // saving 
            Debug.Log($"saved item: {itemKey}");
        }
        else
        {
            Debug.Log($"Item '{itemKey}'   collected.");
        }
    }
 
    // checking if an item is collected , doing  it in a function for more maintainability 
    public bool IsItemCollected(string itemKey)
    {
        return collectedItems.Contains(itemKey) || PlayerPrefs.GetInt(itemKey, 0) == 1; // returns true if an item was collected 
    }

    
    // loading previous item keys 
    private void LoadCollectedItems()
    {
        // debug
        Debug.Log(" collected items from PlayerPrefs");
        string[] keys = { "level_1", "level_2", "level_3", "level_4" };

        foreach (string key in keys)
        {
            if (PlayerPrefs.GetInt(key, 0) == 1)
            {
                collectedItems.Add(key);
                Debug.Log($"Loaded: {key}"); // debug 
            }
        }
    }
}