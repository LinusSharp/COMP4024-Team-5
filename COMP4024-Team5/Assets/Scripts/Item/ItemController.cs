using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemController : MonoBehaviour
{
    [SerializeField] private string nextScene = "Lobby";
    [SerializeField] private GameObject itemCollectedUI;
    [SerializeField] private string itemKey; 
    
    private bool itemCollected = false;

    private void Start() 
    {
        Debug.Log("Scene : " + SceneManager.GetActiveScene().name);

        gameObject.SetActive(true);
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
            Debug.Log("Collected Item: " + itemKey);
            GameObject parentObject = other.transform.parent.gameObject;
            PlayerController player = parentObject.GetComponent<PlayerController>();
            Debug.Log(player);
            if (player != null) {
                Debug.Log("Player collected item and player not null");
                player.IncreaseLevel();
                Destroy(gameObject);
                BackToLobby();
            }
            gameObject.SetActive(false);
        }
    }
    public void BackToLobby()
    {
        Debug.Log(" going to Lobby");
        SceneManager.LoadScene(nextScene);
    }

}