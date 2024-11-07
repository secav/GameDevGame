using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] powerups;
    GameObject[] spikes;
    void Start()
    {
        powerups = GameObject.FindGameObjectsWithTag("Powerup");
        spikes = GameObject.FindGameObjectsWithTag("Spikes");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetAllPowerups()
    {
        foreach(GameObject powerup in powerups)
        {
            Debug.Log(powerup);
            powerup.GetComponent<PowerupJump>().ResetPowerup();
        }
        
        foreach(GameObject spike in spikes)
        {
            Debug.Log(spike);
            spike.GetComponent<FallingSpikeScript>().ResetSpike();
        }
    }

    public void DestroyPlayer(Vector3 checkpointPos)
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        GameObject newPlayer = Instantiate(playerObj);
        playerObj.SetActive(false);
        newPlayer.transform.position = checkpointPos;
    }

}
