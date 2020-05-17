using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRotator : MonoBehaviour
{
    [Header("Hand Velocity")]
    [SerializeField] GameObject hourPivot;
    [SerializeField] GameObject minutePivot;
    [SerializeField] float hourSpeed = 0.1f;
    [SerializeField] float minuteSpeed;
    [SerializeField] bool enemiesAdvance = true;

    [Header("Rotation Degrees")]
    [SerializeField] float hourEulerAngZ;
    [SerializeField] float minuteEulerAngZ;

    [Header("Time Translations")]
    [SerializeField] float hourSimplified;
    [SerializeField] float minuteSimplified;
    [SerializeField] float totalMinutes;

    [Header("Player")]
    [SerializeField] float playerHourRotation = -90f;
    [SerializeField] float hourTickValue = -0.416666f;
    [SerializeField] float minuteTickValue = -5f;
    [SerializeField] int minuteAdvanceCount = 0;
    [SerializeField] int queuedRotations = 0;


    void Start()
    {
        //minute hand angular velocity 12x faster than hour hand
        minuteSpeed = hourSpeed * 12f;
        MinuteCalculator();
        PlayerSpawnValue();
    }

    void FixedUpdate()
    {
        //Constant rotation per frame for enemies only.
        if (enemiesAdvance)
        {
            GradualAdvance();
        }
        
        //Debug details on rotation calculation.  Not needed in update, as calcuation is already called upon collision.
        MinuteCalculator();

        //When the player defeats an enemy clock, the minute hand advances a full rapid rotation and hour hand advances a rapid 12th.
        if (CompareTag("Player") && queuedRotations > 0)
        {
            MinuteRapidRotate();
            HourRapidRotate();
        }
    }

    private void GradualAdvance()
    {
        if (!CompareTag("Player"))
        {
            hourPivot.transform.Rotate(0, 0, -hourSpeed * Time.deltaTime);
            minutePivot.transform.Rotate(0, 0, -minuteSpeed * Time.deltaTime);
        }
    }

    public float MinuteReturner()
    {
        MinuteCalculator();
        return totalMinutes;
    }

    private void MinuteCalculator()
    {
        //Calculate current hand rotations
        hourEulerAngZ = hourPivot.transform.localEulerAngles.z;
        minuteEulerAngZ = minutePivot.transform.localEulerAngles.z;

        //translate rotations to time
        hourSimplified = Mathf.Floor(12f - (hourEulerAngZ / 30f));
        minuteSimplified = 60f - (minuteEulerAngZ / 6f);
        if (minuteSimplified == 60f)
        {
            minuteSimplified = 0;
        }

        //return non-zero total minute float between (0 - 720)
        totalMinutes = (hourSimplified * 60f) + minuteSimplified;
    }

    //Random hand rotations for newly spawned enemy clocks. Minute is relative to hour.
    public void RandomHandRotation()
    {
        float ranHourRotation = Random.Range(-360f, 0);
        hourPivot.transform.rotation = Quaternion.Euler(0, 0, ranHourRotation);
        minutePivot.transform.rotation = Quaternion.Euler(0, 0, (ranHourRotation % 30f) * 12f);
        
    }

    //Determine player starting hour.  Minute remains at midnight.
    private void PlayerSpawnValue()
    {
        if (CompareTag("Player"))
        {
            hourPivot.transform.Rotate(0, 0, playerHourRotation);
        }
    }

   //Add a rapid rotation to the queue on enemy clock death.
    public void AdvancePlayerTime()
    {
        queuedRotations += 1;
    }

    private void MinuteRapidRotate()
    {
        minutePivot.transform.Rotate(0, 0, minuteTickValue);
        minuteAdvanceCount++;
        if (minuteAdvanceCount >= 360 / -minuteTickValue)
        {
            minuteAdvanceCount = 0;
            queuedRotations -= 1;        
        }
    }

    private void HourRapidRotate()
    {
        hourPivot.transform.Rotate(0, 0, hourTickValue);
    }
}
