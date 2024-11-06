using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public bool demoMode = false;
    public float normalSpeed = 3f;
    public float jumpHeight = 5f;

    public float speedBoost = 30f;
    public float speedBoostDuration = 10f;

    public float lightBoostMultiplier = 2f;
    public float lightBoostDuration = 10f;

    public float jumpBoostDuration = 10f;

    public float dashSpeed = 10f;
    public float dashDuration = 1f;
    public float dashCooldown = 1f;
    public float dashBoostDuration = 10f;

    private Vector3 checkpointPosition;
    private Animator animator;
    private float moveSpeed;
    private bool isSpeedBoosted = false;
    private bool isJumpBoosted = false;
    private bool isDashBoosted = false;
    private bool canDoubleJump = false;
    private bool canDash = false;
    private bool isDashing = false;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Light2D currentLight;
    private float startingLightRadius;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        currentLight = GetComponent<Light2D>();
        startingLightRadius = currentLight.pointLightOuterRadius;
        moveSpeed = normalSpeed;
        checkpointPosition = transform.position;
        animator = GetComponent<Animator>();
        if (demoMode)
        {
            StartCoroutine(SimulateInputs());
        }
    }

    void Update()
    {
        if (!isDashing && !demoMode)
        {
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y); // Keep the y velocity unchanged
            animator.SetBool("isMoving", moveInput!=0? true : false);

            if(moveInput > 0){ sr.flipX = false; } else if (moveInput < 0) { sr.flipX = true; }

            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartCoroutine(Dash(moveInput));
            }

            if (Input.GetKey(KeyCode.R))
            {
                SendToLastCheckpoint();
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
    }

    //????????????????
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

    public void ActivateSpeedBoost(Color colorParam, float duration)
    {
        if (!isSpeedBoosted)
        {
            isSpeedBoosted = true;
            moveSpeed = speedBoost;
            currentLight.color = colorParam;
            Invoke("DeactivateSpeedBoost", duration);
        }
    }

    void DeactivateSpeedBoost()
    {
        isSpeedBoosted = false;
        currentLight.color = Color.white;
        moveSpeed = normalSpeed;
    }

    public void ActivateLightBoost(Color colorParam, float duration)
    {
        currentLight.pointLightOuterRadius *= lightBoostMultiplier;
        currentLight.color = colorParam;
        Invoke("DeactivateLightBoost", duration);
    }

    void DeactivateLightBoost()
    {
        currentLight.pointLightOuterRadius =startingLightRadius;
        currentLight.color = Color.white;
    }

    public void ActivateJumpBoost(Color colorParam, float duration)
    {
        isJumpBoosted = true;
        currentLight.color = colorParam;
        Invoke("DeactivateJumpBoost", duration);
    }

    void DeactivateJumpBoost()
    {
        isJumpBoosted = false;
        currentLight.color = Color.white;
    }

    public void ActivateDashBoost(Color colorParam, float duration)
    {
        isDashBoosted = true; // Activate dash boost state
        canDash = true;  // Allow dashing
        currentLight.color = colorParam;
        Invoke("DeactivateDashBoost", duration);
    }

    public void DeactivateDashBoost()
    {
        isDashBoosted = false; // End dash boost state
        canDash = false;  // Disable dashing
        currentLight.color = Color.white;
    }

    private IEnumerator Dash(float moveInput)
    {
        canDash = false;
        isDashing = true;
        animator.SetBool("isDashing", isDashing);
        

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;  // Disable gravity during dash

        // Set dash velocity
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed * (sr.flipX? -1 : 1), 0);

        yield return new WaitForSeconds(dashDuration);  // Wait for dash duration

        rb.velocity = Vector2.zero;  // Stop dash velocity
        rb.gravityScale = originalGravity;  // Restore gravity

        isDashing = false;
        animator.SetBool("isDashing", isDashing);
        yield return new WaitForSeconds(dashCooldown);  // Cooldown period
        if (isDashBoosted)
        {
            canDash = true;  // Allow dash again
        }
    }
    public void NewCheckpointPos(Vector3 vec)
    {
        checkpointPosition = vec;
    }

    public void SendToLastCheckpoint()
    {
        GameObject.Find("Game Manager").GetComponent<GameManager>().ResetAllPowerups();
        DeactivateDashBoost();
        DeactivateJumpBoost();
        DeactivateLightBoost();
        DeactivateSpeedBoost();
        transform.position = checkpointPosition;
    }






    //Demo mode stuff
    IEnumerator SimulateInputs()
    {
        while (true)
        {
            // Simulate moving right for 2 seconds
            yield return MoveRight(1.5f);

            // Simulate standing still for 1 second
            yield return StandStill(2.0f);

            yield return MoveRight(1.5f);


            // Simulate jumping
            //yield return Jump();

            // Simulate standing still for 1 second
            yield return StandStill(8.0f);

            // Simulate moving left for 2 seconds
            yield return MoveLeft(2.0f);

            // Simulate standing still for 1 second
            yield return StandStill(8.0f);

            yield return MoveLeft(2.0f);

            yield return StandStill(6.0f);

            yield return MoveLeft(2.0f);

            yield return StandStill(6.0f);

            yield return MoveRight(3f);

            yield return StandStill(10.0f);


        }
    }

    IEnumerator MoveRight(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            // Mimic the "D" key by setting velocity directly
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            animator.SetBool("isMoving", true);
            sr.flipX = false;

            time += Time.deltaTime;
            yield return null;
        }
        // Stop running
        animator.SetBool("isMoving", false);
    }

    IEnumerator MoveLeft(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            // Mimic the "A" key by setting velocity directly
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            animator.SetBool("isMoving", true);
            sr.flipX = true;

            time += Time.deltaTime;
            yield return null;
        }
        // Stop running
        animator.SetBool("isMoving", false);
    }

    IEnumerator Jump()
    {
        // Check if the player is grounded before jumping
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            //animator.SetTrigger("Jump");
            //GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(1.0f); // Adjust based on jump duration
        }
    }

    IEnumerator StandStill(float duration)
    {
        rb.velocity = new Vector2(0, rb.velocity.y); // Stop horizontal movement
        animator.SetBool("isMoving", false); // Set running animation to false
        yield return new WaitForSeconds(duration); // Wait for specified duration
    }
}
