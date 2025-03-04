using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;   // Movement speed
    public float minX = -5f;   // Left boundary
    public float maxX = 5f;    // Right boundary

    private int direction = 1; // 1 means moving right, -1 means moving left

    // --- Audio Variables ---
    public AudioSource enemyAudioSource;
    public AudioClip enemySound;
    public float minSoundDistance = 2f;
    public float maxSoundDistance = 10f;

    private Transform playerTransform;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        
        // Ensure the AudioSource is assigned
        if (enemyAudioSource == null)
        {
            enemyAudioSource = GetComponent<AudioSource>();
        }

        // makes sure the sound is omnidirectional
        enemyAudioSource.spatialBlend = 1f;
        enemyAudioSource.spread = 360f;

        enemyAudioSource.clip = enemySound;
        enemyAudioSource.loop = true;
        enemyAudioSource.Play();
    }

    void Update()
    {
        // Move the enemy horizontally.
        transform.Translate(Vector2.right * speed * direction * Time.deltaTime);

        // Check boundaries and flip if necessary.
        if (transform.position.x >= maxX && direction == 1)
        {
            direction = -1;
            Flip();
        }
        else if (transform.position.x <= minX && direction == -1)
        {
            direction = 1;
            Flip();
        }

        // Update the audio volume based on distance to the player.
        if (playerTransform != null)
        {
            float distance = Vector2.Distance(transform.position, playerTransform.position);
            // Calculate volume: max at or below minSoundDistance, 0 at or above maxSoundDistance.
            float volume = Mathf.Clamp01(1 - (distance - minSoundDistance) / (maxSoundDistance - minSoundDistance));
            enemyAudioSource.volume = volume;
        }
    }

    // Flips the enemy's sprite by inverting its localScale.x value.
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
