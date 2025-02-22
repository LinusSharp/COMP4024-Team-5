using UnityEngine;
using UnityEngine.SceneManagement;
public class ItemController : MonoBehaviour
{
    [SerializeField] private string nextScene = "Lobby";
    private void Start()
    {
        Debug.Log("CollectableItem Loaded in Scene: " + SceneManager.GetActiveScene().name);
        gameObject.SetActive(true);
    }
    
    
    private void OnDestroy()
    {
        Debug.Log("CollectableItem has been destroyed!");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collected Item! Loading Lobby...");

           
            SceneManager.LoadScene(nextScene);
        }
    }

    
}
