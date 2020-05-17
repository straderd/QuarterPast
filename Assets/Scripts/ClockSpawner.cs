using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockSpawner : MonoBehaviour
{
    [SerializeField] GameObject newClock;
    [SerializeField] bool spawning = true;

    [SerializeField] float spawnX;
    [SerializeField] float spawnY;
    [SerializeField] float spawnZ;
    [SerializeField] float spawnRangeX = 7.5f;
    [SerializeField] float spawnRangeY = 4f;
    [SerializeField] float spawnOuterX = 10f;
    [SerializeField] float spawnOuterY = 6f;

    private int spawnZone;
    private int spawnZoneRotation;
    public float spawnDelay = 2f;
 
    void Start()
    {
        spawnZ = -0.01f;
        StartCoroutine(SpawnTimer()); 
    }

    IEnumerator SpawnTimer()
    {
        //Spawn a new clock at a random location, at a set interval.
        while (spawning)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnZonePicker();
            Instantiate(newClock, new Vector3(spawnX, spawnY, spawnZ), Quaternion.identity);
            spawnZ -= 0.01f;
            SpawnRotationPicker();
            RandomizeClockValue();
        }
    }

    private void RandomizeClockValue()
    {
        newClock.GetComponent<HandRotator>().RandomHandRotation();
    }

    private void SpawnRotationPicker()
    {
        //LEFT
        if (spawnZoneRotation == 0)
        {
            FindObjectOfType<ClockVelocity>().GetSpawnZone(0);
        }
        //RIGHT
        else if (spawnZoneRotation == 1)
        {
            FindObjectOfType<ClockVelocity>().GetSpawnZone(1);
        }
        //TOP
        else if (spawnZoneRotation == 2)
        {
            FindObjectOfType<ClockVelocity>().GetSpawnZone(2);
        }
        //BOTTOM
        else
        {
            FindObjectOfType<ClockVelocity>().GetSpawnZone(3);
        }
    }

    private void SpawnZonePicker()
    {
        spawnZone = Random.Range(0, 4);
        //LEFT
        if (spawnZone == 0)
        {
            spawnX = -spawnOuterX;
            spawnY = Random.Range(-spawnRangeY, spawnRangeY);
            spawnZoneRotation = 0;
        }
        //RIGHT
        else if (spawnZone == 1)
        {
            spawnX = spawnOuterX;
            spawnY = Random.Range(-spawnRangeY, spawnRangeY);
            spawnZoneRotation = 1;
        }
        //TOP
        else if (spawnZone == 2)
        {
            spawnX = Random.Range(-spawnRangeX, spawnRangeX);
            spawnY = spawnOuterY;
            spawnZoneRotation = 2;
        }
        //BOTTOM
        else
        {
            spawnX = Random.Range(-spawnRangeX, spawnRangeX);
            spawnY = -spawnOuterY;
            spawnZoneRotation = 3;
        }
    }
}
