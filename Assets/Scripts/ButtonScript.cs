using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject door;
    public bool hasTimer;
    public float timer = 3f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Light2D>().intensity = 0;
        door.GetComponent<DoorScript>().Activate();
    }
}
