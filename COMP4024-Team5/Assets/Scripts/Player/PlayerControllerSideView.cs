using System.Collections;
using UnityEngine;

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
    private bool _active = true ;
    private Collider2D deathBoxCollider;
    private Vector2 _respawnPosition;
    
    private void Start()
    {
        deathBoxCollider = GetComponent<Collider2D>();
        _active = true;
        
        SetRespawnPoint(transform.position);
    }
    
    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
    }
    
    private void Update()
    {
        if (!_active)
        {
            return;
        }
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && IsGrounded())
        {
            _jumpRequested = true; // Flag for jumping, applied in FixedUpdate
        }
    }

    private void FixedUpdate()
    {
        if (_active)
        {
            rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.linearVelocity.y);

            if (_jumpRequested)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                _jumpRequested = false;
            }
        }
    }

    private bool IsGrounded()
    {
        var bounds = floorBoxCollider.bounds;
        return Physics2D.OverlapBox(bounds.center, bounds.size, 0f, LayerMask.GetMask("Ground"));
    }
    
    private void MiniJump()
    {
        rb.AddForce(new Vector2(0f, jumpForce / 2), ForceMode2D.Impulse);
    }
    
    public void SetRespawnPoint(Vector2 position)
    {
        _respawnPosition = position;
    }

    public void Die()
    {
        _active = false;
        deathBoxCollider.enabled = false;
        MiniJump();
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        transform.position = _respawnPosition;
        _active = true;
        deathBoxCollider.enabled = true;
        MiniJump();
    }
}