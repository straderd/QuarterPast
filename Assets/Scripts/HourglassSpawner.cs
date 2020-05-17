using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourglassSpawner : MonoBehaviour
{
    [SerializeField] GameObject hourglass;
    [SerializeField] float xRangeRight;
    [SerializeField] float xRangeLeft;
    [SerializeField] float yRange = 4f;
    [SerializeField] float zLocation = -5f;
    
    public void SpawnHourglass()
    {
        if (GameObject.Find("Player Clock").transform.position.x >= 0)
        {
            xRangeRight = 0;
            xRangeLeft = -8f;
        }
        else
        {
            xRangeRight = 8f;
            xRangeLeft = 0;
        }
        Instantiate(hourglass, new Vector3(Random.Range(xRangeLeft, xRangeRight), Random.Range(-yRange, yRange), zLocation), Quaternion.identity);
    }
}
