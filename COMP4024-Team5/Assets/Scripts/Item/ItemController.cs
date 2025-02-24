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
        Debug.Log("Scene : " + SceneManager.GetActiveScene().name);

        // hiding the item if it is already collecting 
        if (CollectionController.Instance.IsItemCollected(itemKey))
        {
            Debug.Log($"Item {itemKey} already collected, hiding it.");
            gameObject.SetActive(false); 
            return; 
        }

        gameObject.SetActive(true);

        // hiding the UI before we collect the item
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

            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null) {
                player.IncreaseLevel();
                Destroy(gameObject);
                BackToLobby();
            }

            //  track collected items
            //CollectionController.Instance.CollectItem(itemKey); 
            
            // saving the collected item 
            //PlayerPrefs.SetInt(itemKey, 1);
            //PlayerPrefs.Save();
			
        

            //if (itemCollectedUI != null)
            //{
               // itemCollectedUI.SetActive(true);
            //}
        }
    }
    public void BackToLobby()
    {
        Debug.Log(" going to Lobby");
        SceneManager.LoadScene(nextScene);
    }

}