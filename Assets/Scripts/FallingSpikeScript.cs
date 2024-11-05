using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikeScript : MonoBehaviour
{
    public Rigidbody2D rb; // The Rigidbody2D component of the GameObject to drop
    public float fallGravityScale = 5f; // Gravity scale for faster falling
    public float detectionDistance = 5f; // Distance of each ray
    public float rayOffset = 0.5f; // Horizontal offset from the center for each ray
    public LayerMask playerLayer; // Layer for the player
    private Vector3 startingPos;

    private void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        rb.isKinematic = true;
        startingPos = transform.position;
    }

    private void Update()
    {
        // Define the positions of the two rays
        Vector2 leftRayOrigin = new Vector2(transform.position.x - rayOffset, transform.position.y);
        Vector2 rightRayOrigin = new Vector2(transform.position.x + rayOffset, transform.position.y);

        // Cast the rays downward
        RaycastHit2D leftRayHit = Physics2D.Raycast(leftRayOrigin, Vector2.down, detectionDistance, playerLayer);
        RaycastHit2D rightRayHit = Physics2D.Raycast(rightRayOrigin, Vector2.down, detectionDistance, playerLayer);

        // Check if either ray detects the player
        if ((leftRayHit.collider != null && leftRayHit.collider.CompareTag("Player")) ||
            (rightRayHit.collider != null && rightRayHit.collider.CompareTag("Player")))
        {
            // Make the object fall
            rb.isKinematic = false;
            rb.gravityScale = fallGravityScale;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the rays in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x - rayOffset, transform.position.y),
                        new Vector3(transform.position.x - rayOffset, transform.position.y - detectionDistance));
        Gizmos.DrawLine(new Vector3(transform.position.x + rayOffset, transform.position.y),
                        new Vector3(transform.position.x + rayOffset, transform.position.y - detectionDistance));
    }

    public void ResetSpike()
    {
        transform.position = startingPos;
        rb.isKinematic = true;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
    }
}
