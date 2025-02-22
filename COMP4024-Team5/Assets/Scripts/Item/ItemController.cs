using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemController : MonoBehaviour
{
    [SerializeField] private string nextScene = "Lobby";
    [SerializeField] private GameObject itemCollectedUI;
    [SerializeField] private string itemKey; 
    
    private bool itemCollected = false; // Boolean variable to avoid multiple triggers.

    private void Start() 
    {
        Debug.Log("CollectableItem Loaded in Scene: " + SceneManager.GetActiveScene().name);
        gameObject.SetActive(true);
        
        // Hiding the UI before the item collection.
        if (itemCollectedUI != null)
        {
            itemCollectedUI.SetActive(false);
        }
    }

    private void OnDestroy() 
    {
        Debug.Log("CollectableItem has been destroyed");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !itemCollected)
        {
            itemCollected = true;
            Debug.Log("Collected Item: " + itemKey); // Debugging

            gameObject.SetActive(false); // avoiding multiple triggers and hiding the item after collection.

            //  track collected items
            CollectionController.Instance.CollectItem(itemKey); 
            
            // saving the collected item 
            PlayerPrefs.SetInt(itemKey, 1);
            PlayerPrefs.Save();
			
        

            if (itemCollectedUI != null)
            {
                itemCollectedUI.SetActive(true);
            }
        }
    }
    public void BackToLobby()
    {
        Debug.Log(" going to Lobby");
        SceneManager.LoadScene(nextScene);
    }

}