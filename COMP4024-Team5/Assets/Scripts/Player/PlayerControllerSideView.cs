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
    public Animator animator;
    
    private bool _jumpRequested;
    private bool facingRight = true;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
    }

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
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
        }
    }

    private void FixedUpdate()
    {
        // Apply horizontal movement.
        rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.linearVelocity.y);

        if (_jumpRequested)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            _jumpRequested = false;
        }
    }
    
    // flips the player
    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    // Uses the floorBoxCollider to check if the player is on the ground.
    private bool IsGrounded()
    {
        var bounds = floorBoxCollider.bounds;
        int groundMask = LayerMask.GetMask("Ground");
        return Physics2D.OverlapBox(bounds.center, bounds.size, 0f, groundMask);
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

}
