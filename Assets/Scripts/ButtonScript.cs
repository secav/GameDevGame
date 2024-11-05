using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ButtonScript : MonoBehaviour
{
    public GameObject door;
    public bool hasTimer;
    public float timer = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Light2D>().intensity = 0;
        door.GetComponent<DoorScript>().Activate();

        if (hasTimer)
        {
            StartCoroutine(DeactivateDoorAfterTimer());
        }
    }

    private IEnumerator DeactivateDoorAfterTimer()
    {
        // Wait for the specified time
        yield return new WaitForSeconds(timer);

        // Deactivate the door after the timer expires
        door.GetComponent<DoorScript>().Deactivate();
        GetComponent<Light2D>().intensity = 1;
    }
}
