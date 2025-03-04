using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Controls the player's movement, jumping, animations, and audio in a side-view platformer.
/// </summary>
public class PlayerControllerSideView : MonoBehaviour
{
    /// <summary>
    /// The speed  of the player.
    /// </summary>
    public float moveSpeed = 5f;
    
    /// <summary>
    /// The force applied to the player when jumping.
    /// </summary>
    
    public float jumpForce = 5f;
    
    /// <summary>
    /// The Rigidbody2D component attached to the player for  interactions.
    /// </summary>
    
    [SerializeField]
    private Rigidbody2D rb;
    
    /// <summary>
    /// The collider used to detect the floor for grounding checks.
    /// </summary>

    [SerializeField]
    private Collider2D floorBoxCollider;
    
    /// <summary>
    /// Tracks if the player want to jump.
    /// </summary>
    private bool _jumpRequested;
    
    
    /// <summary>
    /// Indicates if the player can move and interact.
    /// </summary>
    [SerializeField]
    private bool active = true;
    
    /// <summary>
    /// The collider used to detect when the player dies.
    /// </summary>
    [SerializeField]
    private Collider2D deathBoxCollider;
    
    /// <summary>
    /// Stores the player's last respawn positions.
    /// </summary>
    private Vector2 _respawnPosition;

    /// <summary>
    /// The  component used to control the player's animations.
    /// </summary>
    public Animator animator;
    
    
    /// <summary>
    /// Checks if  the player is facing right.
    /// </summary>
    private bool facingRight = true;

    // --- Audio Sources and Clips ---
    
    /// <summary>
    /// The audio  used sounds for looping like footsteps.
    /// </summary>
    public AudioSource loopingAudioSource;
    
    /// <summary>
    /// The audio used for playing one shot sounds.
    /// </summary>
    public AudioSource oneShotAudioSource;
    
    /// <summary>
    /// The sound effect used  when the player walks.
    /// </summary>
    public AudioClip footstepSound;
    
    /// <summary>
    /// The sound effect used  when the player jumps.
    /// </summary>
    public AudioClip jumpSound;
    
    /// <summary>
    /// The sound effect used  when the player is in the air. 
    /// </summary>
    public AudioClip inAirSound;
    
    
    /// <summary>
    /// The sound effect used  when the player dies. 
    /// </summary>
    public AudioClip dieSound; 

    // --- Volume settings for looping sounds ---
    
    /// <summary>
    /// Footstep sound volume.
    /// </summary>
    public float footstepVolume = 0.5f;
    
    
    /// <summary>
    /// In Air sound volume.
    /// </summary>
    public float inAirVolume = 0.5f;
    
    /// <summary>
    /// Initialises the player.
    /// </summary>
    
    private void Start()
    {
        active = true;
        SetRespawnPoint();

        if (loopingAudioSource == null)
        {
            loopingAudioSource = GetComponent<AudioSource>();
        }
        if (oneShotAudioSource == null)
        {
            GameObject oneShotObj = new GameObject("OneShotAudio");
            oneShotObj.transform.parent = transform;
            oneShotAudioSource = oneShotObj.AddComponent<AudioSource>();
        }
    }

    /// <summary>
    /// Called when the object is initialszed.
    /// </summary>
    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
    }
    /// <summary>
    /// Subscribes to the sceneLoaded event and ensures the Rigidbody2D component is set up.
    /// </summary>
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
    }
    
    /// <summary>
    /// Unsubscribes from the sceneLoaded event to prevent memory leaks.
    /// </summary>
    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    /// <summary>
    /// Resets the player's movement when a new scene is loaded.
    /// </summary>
    /// <param name="scene">The scene that was loaded.</param>
    /// <param name="mode">The mode in which the scene was loaded.</param>
    
 
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset the player's movement
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;
    }
    
    /// <summary>
    /// Handles player input, movement, jumping, and sound effects.
    /// </summary>
    private void Update()
    {

        if (!active)
        {
            return;
        }

        // Handle horizontal movement and animations.
        float moveX = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(moveX));
        
        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveX < 0 && facingRight)
        {
            Flip();
        }
        
        // Check for jump input with multiple keys and only if grounded.
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && IsGrounded())
        {
            _jumpRequested = true;
            animator.SetBool("IsJumping", true);
            oneShotAudioSource.PlayOneShot(jumpSound, 0.5f);
        }

        // --- Looping Sound Effect Logic ---
        if (!IsGrounded())
        {
            if (loopingAudioSource.clip != inAirSound || !loopingAudioSource.isPlaying)
            {
                loopingAudioSource.Stop();
                loopingAudioSource.clip = inAirSound;
                loopingAudioSource.loop = true;
                loopingAudioSource.volume = inAirVolume;
                loopingAudioSource.Play();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) ||
                Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
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
                if (loopingAudioSource.isPlaying && 
                    (loopingAudioSource.clip == footstepSound || loopingAudioSource.clip == inAirSound))
                {
                    loopingAudioSource.Stop();
                }
            }
        }
    }
    /// <summary>
    /// Handles the player's movement and jumping.
    /// </summary>
    private void FixedUpdate()
    {
        if (active)
        {
            rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.linearVelocity.y);

            if (_jumpRequested)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                _jumpRequested = false;
            }
        }
    }
    
    
    /// <summary>
    /// Flips the player's facing direction.
    /// </summary>
    
    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
    /// <summary>
    /// Checks if the player is touching the ground.
    /// </summary>
    /// <returns>Returns true if the player is grounded, false otherwise.</returns>
    private bool IsGrounded()
    {
        var bounds = floorBoxCollider.bounds;
        int groundMask = LayerMask.GetMask("Ground");
        bool isGrounded = Physics2D.OverlapBox(bounds.center, bounds.size, 0f, groundMask);
        return isGrounded;
    }
    
    /// <summary>
    /// Performs a small jump.
    /// </summary>
    private void MiniJump()
    {
        rb.AddForce(new Vector2(0f, jumpForce / 2), ForceMode2D.Impulse);
    }
    
    /// <summary>
    /// Sets the current player position as the respawn point.
    /// </summary>
    
    public void SetRespawnPoint()
    {
        _respawnPosition = transform.position;
    }
    
    
    /// <summary>
    /// Handles the player's death by disabling movement and playing a death sound.
    /// </summary>

    public void Die()
    {
        active = false;
        deathBoxCollider.enabled = false;
        
        // Play the death sound using oneShotAudioSource.
        oneShotAudioSource.PlayOneShot(dieSound, 0.5f);

        Vector3 position = transform.position;
        position.z -= 1;
        transform.position = position;
        
        MiniJump();
        StartCoroutine(Respawn());
    }

    /// <summary>
    /// Respawns the player at their last set respawn position after a delay.
    /// </summary>
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        transform.position = _respawnPosition;
        active = true;
        deathBoxCollider.enabled = true;
        MiniJump();
    }
    
    /// <summary>
    /// Resets the jump animation when landing.
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            animator.SetBool("IsJumping", false);
        }
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

    
    /// <summary>
    /// Resets the player's animation states.
    /// </summary>
    public void ResetAnimation()
    {
        animator.SetBool("IsJumping", false);
        animator.SetFloat("Speed", 0f);
    }


}
