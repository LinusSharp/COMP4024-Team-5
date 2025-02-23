using System;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision detected");
        var player = other.collider.GetComponent<PlayerControllerSideView>();
        if (player != null)
        {
            Debug.Log("Player found");
            player.Die();
            
        }
    }
}
