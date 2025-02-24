using UnityEngine;

public class PlayerControllerTopDown : MonoBehaviour
{
    public float speed = 5f;
    
    private Rigidbody2D rb;

    public Animator animator;
    private bool facingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // sets the gravity to 0
    private void OnEnable()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    private void Update()
    {
        // movement in all directions 
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

        animator.SetFloat("Speed", Mathf.Abs(moveX));

        Vector2 movement = new Vector2(moveX, moveY) * speed;
        rb.linearVelocity = movement;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
}
