using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DarknessTrigger : MonoBehaviour
{
    public GameObject darktrig;
    public Light2D light2;
    private Light2D light;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
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
            light2.intensity = 0.007f;
            light.intensity = 0.007f;
        }
    }
}
