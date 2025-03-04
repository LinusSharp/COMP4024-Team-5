using UnityEngine;

public class PlayerControllerTopDown : MonoBehaviour
{
    public float speed = 5f;
    
    private Rigidbody2D rb;
    public Animator animator;
    private bool facingRight = true;

    // --- Running Sound Variables ---
    public AudioSource loopingAudioSource;
    public AudioClip footstepSound;
    public float footstepVolume = 0.5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Sets gravity to 0 and ensures an AudioSource is available.
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

    private void Update()
    {
        // Get movement input from Horizontal and Vertical axes.
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveX < 0 && facingRight)
        {
            Flip();
        }

        Vector2 movement = new Vector2(moveX, moveY);

        animator.SetFloat("Speed", movement.magnitude);

        rb.linearVelocity = movement * speed;

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

    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    public void ResetAnimation()
    {
        animator.SetFloat("Speed", 0f);
    }
}
