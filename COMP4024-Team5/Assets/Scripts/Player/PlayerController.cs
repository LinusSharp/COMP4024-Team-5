using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 20f;
    private static PlayerController instance;
    public int level = 1;

    private void Awake()
    {
        // destroys the object if it already exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        // marks it as a singlton
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void IncreaseLevel() {
        level++;
        Debug.Log("Player level increased to: " + level);
    }
    
    private void OnEnable()
    {
        transform.position = Vector3.zero; // Reset position when loading a new scene
    }
}
