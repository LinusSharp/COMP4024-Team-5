using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Set the player's position to the spawn position.
            player.transform.position = transform.position;

            // Reset the player's physics so it is no longer moving.
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }

            PlayerControllerSideView sideView = player.GetComponent<PlayerControllerSideView>();
            if (sideView != null)
            {
                sideView.ResetFacing();
            }
        }
    }
}
