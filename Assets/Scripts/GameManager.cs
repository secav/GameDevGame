using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] powerups;
    void Start()
    {
        powerups = GameObject.FindGameObjectsWithTag("Powerup");
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
    }

}
