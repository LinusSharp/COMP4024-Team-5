using UnityEngine;
using UnityEngine.SceneManagement;
public class ItemController : MonoBehaviour
{
    [SerializeField] private string nextScene = "Lobby";
    private bool itemCollected = false; // boolean variable to avoid mutliple triggers. 
   
    private void Start() // debugging 
    {
        Debug.Log("CollectableItem Loaded in Scene: " + SceneManager.GetActiveScene().name);
        gameObject.SetActive(true);
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
            SceneManager.LoadScene(nextScene);
        }
    }

    
}
