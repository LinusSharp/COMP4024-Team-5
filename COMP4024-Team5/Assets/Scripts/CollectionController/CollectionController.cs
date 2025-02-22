using UnityEngine;
using System.Collections.Generic;

public class CollectionController : MonoBehaviour
{
    public static CollectionController Instance;
    private HashSet<string> collectedItems = new HashSet<string>(); // Store collected items

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
            Destroy(gameObject);
        }
    }

    public void CollectItem(string itemKey)
    {
        if (!collectedItems.Contains(itemKey))
        {
            collectedItems.Add(itemKey);
            PlayerPrefs.SetInt(itemKey, 1);
            PlayerPrefs.Save();
            Debug.Log($"Collected and saved item: {itemKey}");
        }
        else
        {
            Debug.Log($"Item '{itemKey}' was  collected.");
        }
    }

    public bool IsItemCollected(string itemKey)
    {
        return collectedItems.Contains(itemKey) || PlayerPrefs.GetInt(itemKey, 0) == 1;
    }

    private void LoadCollectedItems()
    {
        Debug.Log("Loading collected items from PlayerPrefs...");
        string[] keys = { "level_1", "level_2", "level_3", "level_4" };

        foreach (string key in keys)
        {
            if (PlayerPrefs.GetInt(key, 0) == 1)
            {
                collectedItems.Add(key);
                Debug.Log($"Loaded: {key}");
            }
        }
    }
}