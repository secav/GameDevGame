using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SecretRoomLogic : MonoBehaviour
{
    private Tilemap tilemap;
    public float transparentAlpha = 0.5f; // Alpha value for transparency (0.0 to 1.0)
    private float originalAlpha; // To store the original alpha of the Tilemap

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the tilemap is assigned, otherwise get the Tilemap component on this GameObject
        if (tilemap == null) 
            tilemap = GetComponentInParent<Tilemap>();

        // Store the original alpha of the Tilemap
        originalAlpha = tilemap.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player has entered the trigger
        if (other.CompareTag("Player"))
        {
            // Change the Tilemap's color to make it transparent
            Color color = tilemap.color;
            color.a = transparentAlpha;
            tilemap.color = color;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player has exited the trigger
        if (other.CompareTag("Player"))
        {
            // Revert the Tilemap's color to its original alpha
            Color color = tilemap.color;
            color.a = originalAlpha;
            tilemap.color = color;
        }
    }
}
