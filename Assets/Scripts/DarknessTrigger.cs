using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DarknessTrigger : MonoBehaviour
{
    public float newIntensity = 0.007f;
    public Light2D light2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("off");
            light2.intensity = newIntensity;
        }
    }
}
