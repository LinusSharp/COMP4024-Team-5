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
}
