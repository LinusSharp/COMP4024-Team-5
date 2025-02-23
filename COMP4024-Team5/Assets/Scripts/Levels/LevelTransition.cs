using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; 
    [SerializeField] private int levelNumber; // level number 
    [SerializeField] private SpriteRenderer doorSprite; // reference to the door sprite renderer
    [SerializeField] private Collider2D doorCollider; // reference to the door's collider

    private void Start()
    {
        if (!IsLevelUnlocked())
        {
            LockDoor();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (IsLevelUnlocked())
            {
                SceneManager.LoadScene(sceneToLoad);
                Debug.Log($" Level {levelNumber}");
            }
            else
            {  // debugging
                Debug.Log($"level {levelNumber} is locked");
            }
        }
    }

    private bool IsLevelUnlocked()
    {
        if (levelNumber == 1) return true; // level 1 always unlocked
        return PlayerPrefs.GetInt($"level_{levelNumber - 1}", 0) == 1;
    }

    private void LockDoor()
    {
        if (doorSprite != null)
        {
            Color lockedColor = doorSprite.color;
            lockedColor.a = 0.5f; // locking the door
            doorSprite.color = lockedColor;
        }

        if (doorCollider != null)
        {
            doorCollider.enabled = false; // desabling the collider 
        }

        // debug
        Debug.Log($"Level {levelNumber} is locked.");
    }
}