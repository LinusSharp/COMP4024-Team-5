using UnityEngine;
/// <summary>
/// Controls the player's movement and animations in a top-down.
/// Handles movement, animations, sound effects, and player direction.
/// </summary>
public class PlayerControllerTopDown : MonoBehaviour
{
    
    /// <summary>
    /// The  speed of the player.
    /// </summary>
    public float speed = 5f;
    
    
    /// <summary>
    /// The Rigidbody2D component used for movement physics.
    /// </summary>
    private Rigidbody2D rb;
    
    /// <summary>
    /// The component used for handling animations.
    /// </summary>
    public Animator animator;
    
    /// <summary>
    /// Indicates if  the player is facing right.
    /// </summary>
    private bool facingRight = true;

    // --- Running Sound Variables ---
    
    /// <summary>
    /// The audio used for playing looping sounds like footsteps.
    /// </summary>
    public AudioSource loopingAudioSource;
    
    
    /// <summary>
    /// The audio used for  footsteps.
    /// </summary>
    public AudioClip footstepSound;
    
    /// <summary>
    /// The volume used   footsteps audio.
    /// </summary>
    public float footstepVolume = 0.5f;
    /// <summary>
    /// Called when the script instance is being loaded.
    /// Initialises the Rigidbody2D.
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    /// <summary>
    // Sets gravity to 0 and ensures an AudioSource is available.
    /// </summary>
    
    private void OnEnable()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        
        // Ensure there's a loopingAudioSource.
        if (loopingAudioSource == null)
        {
            loopingAudioSource = GetComponent<AudioSource>();
        }
    }

    /// <summary>
    /// Updates player movement, animations, and sound effects each frame.
    /// </summary>
    private void Update()
    {
        // Get movement input from Horizontal and Vertical axes.
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        // Flip player direction based on movement.
        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveX < 0 && facingRight)
        {
            Flip();
        }

        Vector2 movement = new Vector2(moveX, moveY);
        // Update animation speed based on movement magnitude.

        animator.SetFloat("Speed", movement.magnitude);
        // Apply movement to the player.
        rb.linearVelocity = movement * speed;
        // Play footstep sound.
        if (movement.magnitude > 0.1f)
        {
            if (loopingAudioSource.clip != footstepSound || !loopingAudioSource.isPlaying)
            {
                loopingAudioSource.Stop();
                loopingAudioSource.clip = footstepSound;
                loopingAudioSource.loop = true;
                loopingAudioSource.volume = footstepVolume;
                loopingAudioSource.Play();
            }
        }
        else
        {
            // Stop the running sound when idle.
            if (loopingAudioSource.isPlaying && loopingAudioSource.clip == footstepSound)
            {
                loopingAudioSource.Stop();
            }
        }
    }
    /// <summary>
    /// Flips the player to face the correct direction.
    /// </summary>
    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
    /// <summary>
    /// Resets the player animation state.
    /// </summary>
    public void ResetAnimation()
    {
        animator.SetFloat("Speed", 0f);
    }
    
    
    /// <summary>
    /// Resets the player's facing direction to default.
    /// </summary>

    public void ResetFacing()
    {
        facingRight = true;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}
