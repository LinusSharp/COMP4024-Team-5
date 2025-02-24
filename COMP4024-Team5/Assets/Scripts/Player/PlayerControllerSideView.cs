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
    private bool active = true ;
    private Collider2D _deathBoxCollider;
    private Vector2 _respawnPosition;
    
    private void Start()
    {
        _deathBoxCollider = GetComponent<Collider2D>();
        active = true;
        SetRespawnPoint();
    }
    
    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
    }
    
    private void Update()
    {
        if (!active)
        {
            return;
        }
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && IsGrounded())
        {
            _jumpRequested = true;
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
                Debug.Log("jump 1 ");
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
    
    public void SetRespawnPoint()
    {
        _respawnPosition = transform.position;
    }

    public void Die()
    {
        active = false;
        _deathBoxCollider.enabled = false;
        
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
        _deathBoxCollider.enabled = true;
        MiniJump();
    }
    
    private void OnEnable()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
    }
}