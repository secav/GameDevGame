using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PowerupJump : MonoBehaviour
{
    private Light2D light;
    public enum PowerupType
    {
        Speed,
        Light,
        Dash,
        Jump
    }
    public PowerupType type = PowerupType.Dash;
    public float powerUpDuration = 5f; // Duration of the speed boost
    private SpriteRenderer sr;
    public Sprite speedSprite;
    public Sprite dashSprite;
    public Sprite jumpSprite;
    public Sprite lightSprite;

    private void Start()
    {
        light = GetComponent<Light2D>();
        sr = GetComponent<SpriteRenderer>();
        switch (type)
        {
            case PowerupType.Speed:
                light.color = Color.red;
                sr.sprite = speedSprite;
                break;
            case PowerupType.Light:
                light.color = Color.yellow;
                sr.sprite = lightSprite;
                break;
            case PowerupType.Dash:
                sr.sprite = dashSprite;
                break;
            case PowerupType.Jump:
                light.color = Color.green;
                sr.sprite = jumpSprite;
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            switch (type)
            {
                case PowerupType.Speed:
                    player.ActivateSpeedBoost(light.color);
                    break;
                case PowerupType.Light:
                    player.ActivateLightBoost(light.color);
                    break;
                case PowerupType.Dash:
                    player.ActivateDashBoost(light.color);
                    break;
                case PowerupType.Jump:
                    player.ActivateJumpBoost(light.color);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
