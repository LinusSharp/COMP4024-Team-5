using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 20f;
    private static PlayerController instance;

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
    
    private void OnEnable()
    {
        transform.position = Vector3.zero; // Reset position when loading a new scene
    }
}
