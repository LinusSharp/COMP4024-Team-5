using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 20f;
    private static PlayerController instance;
    public int level = 1;

    public AudioSource oneShotAudioSource;
    public AudioClip levelUpSound;
    public float levelUpVolume = 0.5f;

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

        if (oneShotAudioSource == null)
        {
            oneShotAudioSource = GetComponent<AudioSource>();
        }
    }

    public void IncreaseLevel() {
        level++;
        Debug.Log("Player level increased to: " + level);

        if (oneShotAudioSource != null && levelUpSound != null)
        {
            oneShotAudioSource.PlayOneShot(levelUpSound, levelUpVolume);
        }
    }
    
    private void OnEnable()
    {
        transform.position = Vector3.zero; // Reset position when loading a new scene
    }
}
