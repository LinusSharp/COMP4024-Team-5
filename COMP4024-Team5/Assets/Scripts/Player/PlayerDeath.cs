using System;
using UnityEngine;

/// <summary>
/// Handles player death when colliding with certain objects.
/// Detects collisions and triggers the player's death sequence.
/// </summary>

public class PlayerDeath : MonoBehaviour
{
    /// <summary>
    /// Called when the player collides with another object.
    /// Checks if the collided object is the player and triggers the death sequence if so.
    /// </summary>
    /// <param name="other">The collision data of the object the player collided with.</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Check if the collided object has a PlayerControllerSideView component
        Debug.Log("Collision detected");
        PlayerControllerSideView player = other.gameObject.GetComponent<PlayerControllerSideView>();
        if (player != null)
        {
            Debug.Log("Player died");
            player.Die();
        }
    }
}
