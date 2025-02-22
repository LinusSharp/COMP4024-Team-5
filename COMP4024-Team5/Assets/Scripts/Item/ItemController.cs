using UnityEngine;
using UnityEngine.SceneManagement;
public class ItemController : MonoBehaviour
{
    [SerializeField] private string nextScene = "Lobby";
    [SerializeField] private GameObject itemCollectedUI;
    
    [SerializeField] private string itemKey; 
    
    private bool itemCollected = false; // boolean variable to avoid mutliple triggers. 
   
    private void Start() // debugging 
    {
        Debug.Log("CollectableItem Loaded in Scene: " + SceneManager.GetActiveScene().name);
        gameObject.SetActive(true);
        
        // hiding the ui before the item collection 
        if (itemCollectedUI != null)
        {
            itemCollectedUI.SetActive(false);
        }
    }
    
    
    
    private void OnDestroy() // debugging
    {
        Debug.Log("CollectableItem has been destroyed");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !itemCollected)
        {
            itemCollected = true;
            Debug.Log("Collected Item");

            gameObject.SetActive(false); // avoiding multiple triggers




            if (itemCollectedUI != null)
            {
                itemCollectedUI.SetActive(true);
            }
        }
    }
    public void BackToLobby()
    {
        SceneManager.LoadScene(nextScene);
    }
}
