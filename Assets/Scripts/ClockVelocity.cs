using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockVelocity : MonoBehaviour
{
    [SerializeField] float baseVelocity = 1f;
    Rigidbody2D clockRigidbody;
    [SerializeField] float directionOffset = 5f;
    [SerializeField] float chosenOffset;
    [SerializeField] int spawnZoneReceived;
    
    void Start()
    {
        clockRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        clockRigidbody.transform.Translate(VelocityDirection() * Time.deltaTime * (baseVelocity + FindObjectOfType<ScoreKeeper>().VelocityIncrement()));
    }

    //Get spawn zone from ClockSpawner
    public void GetSpawnZone(int spawnZone)
    {
        //Determine velocity variance before Update() to prevent jittering
        chosenOffset = Random.Range(-directionOffset, directionOffset);
        //LEFT
        if (spawnZone == 0)
        {
            spawnZoneReceived = 0;
        }
        //RIGHT
        else if (spawnZone == 1)
        {
            spawnZoneReceived = 1; 
        }
        //TOP
        else if (spawnZone == 2)
        {
            spawnZoneReceived = 2; 
        }
        //BOTTOM
        else
        {
            spawnZoneReceived = 3; 
        }
    }
    
    //Link ClockSpawner to velocity direction in local script 
    private Vector3 VelocityDirection()
    {
        //LEFT
        if (spawnZoneReceived == 0)
        {
            return Vector3.right + new Vector3(0, chosenOffset, 0);
        }
        //RIGHT
        else if (spawnZoneReceived == 1)
        {
            return Vector3.left + new Vector3(0, chosenOffset, 0);
        }
        //TOP
        else if (spawnZoneReceived == 2)
        {
            return Vector3.down + new Vector3(chosenOffset, 0, 0);
        }
        //BOTTOM
        else
        {
            return Vector3.up + new Vector3(chosenOffset, 0, 0);
        }
    }
}
