using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    public float powerUpDuration = 5f; // Duration of the speed boost

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ActivateSpeedBoost();
                Destroy(gameObject); // Destroy the power-up after collection
            }
        }
    }
}
