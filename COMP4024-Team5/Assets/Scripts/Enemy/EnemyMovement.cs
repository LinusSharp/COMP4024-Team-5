using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;   // Movement speed
    public float minX = -5f;   // Left boundary
    public float maxX = 5f;    // Right boundary

    private int direction = 1; // 1 means moving right, -1 means moving left

    void Update()
    {
        // Move the enemy horizontally
        transform.Translate(Vector2.right * speed * direction * Time.deltaTime);

        // Check boundaries and flip if necessary
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
    }

    // This method flips the enemy's sprite by inverting its localScale.x value
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
