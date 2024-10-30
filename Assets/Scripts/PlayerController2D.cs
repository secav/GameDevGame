using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public float normalSpeed = 3f;
    public float jumpHeight = 5f;
    public float speedBoost = 30f;
    public float speedBoostDuration = 10f;
    public float lightBoostMultiplier = 2f;
    public float lightBoostDuration = 10f;
    public float jumpBoostDuration = 10f;

    private float moveSpeed;
    private bool isSpeedBoosted = false;
    private bool isLightBoosted = false;
    private bool isJumpBoosted = false;
    private bool canDoubleJump = false;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Light2D currentLight;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        currentLight = GetComponent<Light2D>();
        moveSpeed = normalSpeed;
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y); // Keep the y velocity unchanged

        if (moveInput > 0)
        {
            sr.flipX = false;
        }
        else if (moveInput < 0)
        {
            sr.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                // Normal jump
                Debug.Log(IsGrounded().ToString());
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                canDoubleJump = isJumpBoosted; // Enable double jump if jump boosted
            }
            else if (canDoubleJump)
            {
                // Double jump
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                canDoubleJump = false; // Disable double jump after use
            }
        }
    }

    //private bool IsGrounded()
    //{
    //    // Cast a ray downwards to check if the player is on the ground
    //    return Physics2D.Raycast(transform.position, Vector2.down, 0f);
    //}

    private bool IsGrounded()
    {
        // Get the player's collider
        Collider2D collider = GetComponent<Collider2D>();

        // Check if there are any colliders directly below the player
        return Physics2D.OverlapBox(collider.bounds.center - new Vector3(0, collider.bounds.extents.y, 0),
                                     new Vector2(collider.bounds.size.x, 0.1f),
                                     0,
                                     LayerMask.GetMask("Ground")) != null; // Replace "Ground" with your ground layer
    }

    public void ActivateSpeedBoost()
    {
        if (!isSpeedBoosted)
        {
            isSpeedBoosted = true;
            moveSpeed = speedBoost;
            currentLight.color = Color.red;
            Invoke("DeactivateSpeedBoost", speedBoostDuration);
        }
    }

    void DeactivateSpeedBoost()
    {
        isSpeedBoosted = false;
        currentLight.color = Color.white;
        moveSpeed = normalSpeed;
    }

    public void ActivateLightBoost()
    {
        isLightBoosted = true;
        currentLight.pointLightOuterRadius *= lightBoostMultiplier;
        currentLight.color = Color.yellow;
        Invoke("DeactivateLightBoost", lightBoostDuration);
    }

    void DeactivateLightBoost()
    {
        isLightBoosted = false;
        currentLight.pointLightOuterRadius /= lightBoostMultiplier;
        currentLight.color = Color.white;
    }

    public void ActivateJumpBoost()
    {
        isJumpBoosted = true;
        currentLight.color = Color.green;
        Invoke("DeactivateJumpBoost", jumpBoostDuration);
    }

    void DeactivateJumpBoost()
    {
        isJumpBoosted = false;
        currentLight.color = Color.white;
    }
}
