using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CheckpointTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isCheckpoint = false;
    private Light2D light;
    void Start()
    {
        light = GetComponentInChildren<Light2D>();
        if (isCheckpoint)
        {
            GetComponent<Animator>().enabled = false;
            light.intensity = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCheckpoint && collision.tag == "Player")
        {
            GetComponent<Animator>().enabled = true;
            light.intensity = 1;
            PlayerController player = collision.GetComponent<PlayerController>();
            player.NewCheckpointPos(transform.position);
            GetComponent<AudioSource>().Play();
        }
    }
}
