using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerSideView : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Collider2D floorBoxCollider;
    private bool _jumpRequested;

    [SerializeField]
    private bool active = true;
    [SerializeField]
    private Collider2D deathBoxCollider;
    private Vector2 _respawnPosition;

    public Animator animator;
    
    private bool facingRight = true;

    // --- Audio Sources and Clips ---
    public AudioSource loopingAudioSource;
    public AudioSource oneShotAudioSource;
    
    public AudioClip footstepSound;
    public AudioClip jumpSound;
    public AudioClip inAirSound;
    public AudioClip dieSound; // New death sound

    // --- Volume settings for looping sounds ---
    public float footstepVolume = 0.5f;
    public float inAirVolume = 0.5f;
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

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
    }
    
    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    // This method is called every time a new scene is loaded.
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset the player's movement
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;
    }
    
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
    
    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
    
    private bool IsGrounded()
    {
        var bounds = floorBoxCollider.bounds;
        int groundMask = LayerMask.GetMask("Ground");
        bool isGrounded = Physics2D.OverlapBox(bounds.center, bounds.size, 0f, groundMask);
        return isGrounded;
    }
    
    private void MiniJump()
    {
        rb.AddForce(new Vector2(0f, jumpForce / 2), ForceMode2D.Impulse);
    }
    
    public void SetRespawnPoint()
    {
        _respawnPosition = transform.position;
    }

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

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        transform.position = _respawnPosition;
        active = true;
        deathBoxCollider.enabled = true;
        MiniJump();
    }
    
    // Resets the jump animation when landing.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            animator.SetBool("IsJumping", false);
        }
    }

    public void ResetFacing()
    {
        facingRight = true;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    public void ResetAnimation()
    {
        animator.SetBool("IsJumping", false);
        animator.SetFloat("Speed", 0f);
    }


}
