using System;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision detected");
        PlayerControllerSideView player = other.gameObject.GetComponent<PlayerControllerSideView>();
        if (player != null)
        {
            Debug.Log("Player died");
            player.Die();
        }
    }
}
