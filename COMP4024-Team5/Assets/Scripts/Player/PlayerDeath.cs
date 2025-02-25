using System;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.collider.GetComponent<PlayerControllerSideView>();
        if (player != null)
        {
            player.Die();
        }
    }
}
