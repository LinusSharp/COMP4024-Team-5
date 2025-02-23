using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTransition : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "Lobby";
   
    // this is the transition into the different scenes
    
    private Collider2D doorCollider;
    private SpriteRenderer doorSprite;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
