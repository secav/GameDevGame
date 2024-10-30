using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupLight : MonoBehaviour
{
    public float powerUpDuration = 5f; // Duration of the speed boost

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ActivateLightBoost();
                Destroy(gameObject); // Destroy the power-up after collection
            }
        }
    }
}
