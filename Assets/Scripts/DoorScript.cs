using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public float speed = 3f;
    private bool active = false;
    private Color objectColor;
    public float decreaseRate = 10f;
    private Renderer objectRenderer;
    private BoxCollider2D objectCollider;

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectCollider = GetComponent<BoxCollider2D>();

        // Ensure the object is fully opaque at the start
        objectColor = objectRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            // Decrease alpha over time
            objectColor.a -= decreaseRate * Time.deltaTime;

            // Clamp alpha between 0 and 1
            objectColor.a = Mathf.Clamp(objectColor.a, 0f, 1f);
            objectRenderer.material.color = objectColor;

            // Disable collider when alpha is 0
            if (objectColor.a <= 0f)
            {
                objectCollider.enabled = false;
                active = false; // Stop the process if needed
            }
        }
    }

    public void Activate()
    {
        active = true; // Start the transparency process
    }
}
