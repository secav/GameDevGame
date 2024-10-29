using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class CrystalTrigger : MonoBehaviour
{
    private float currentLight = 0f;
    public float lightIncreaseRate = 1.0f;
    public float maxIntensity = 1.0f;
    private List<Light2D> lightList;
    private bool lightsOn = false;


    // Start is called before the first frame update
    void Start()
    {
        lightList = new List<Light2D>();
        foreach(GameObject gobj in GameObject.FindGameObjectsWithTag("Light"))
        {
            if(gobj.tag == "Light")
            {
                Light2D lightComponent = gobj.GetComponent<Light2D>();
                lightList.Add(lightComponent);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lightsOn && currentLight < maxIntensity)
        {
            currentLight += lightIncreaseRate*Time.deltaTime;
            foreach(Light2D light in lightList)
            {
                light.intensity = currentLight;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            lightsOn = true;
        }
    }
}
