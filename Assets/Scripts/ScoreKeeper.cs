using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dayText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] int dayScore = 1;
    [SerializeField] int hourScore = 3;
    [SerializeField] string amPm = "AM";
    [SerializeField] bool dayRoll = false;
    [SerializeField] float velocityIncrement = 0;

    void Start()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        dayText.text = ("Day " + dayScore);
        timeText.text = (hourScore + ":00 " + amPm);
    }

    private void IncrementDay()
    {
        dayScore++;
        amPm = "AM";
        UpdateScore();
        velocityIncrement += 0.5f;
        if (FindObjectOfType<ClockSpawner>().spawnDelay > 0.25f)
        {
            FindObjectOfType<ClockSpawner>().spawnDelay -= 0.1f;
        }
        FindObjectOfType<BackgroundParallax>().IncreaseBackgroundSpeed();
        dayRoll = false;
    }
    public void IncrementHour()
    {
        if (dayRoll == true)
        {
            IncrementDay();
        }
        hourScore++;
        if (hourScore == 12 && amPm == "AM")
        {
            amPm = "PM";
            FindObjectOfType<HourglassSpawner>().SpawnHourglass();
        }
        else if (hourScore == 13)
        {
            hourScore = 1;
        }
        else if (hourScore == 12 && amPm == "PM")
        {
            amPm = "AM";
            dayRoll = true;
            FindObjectOfType<HourglassSpawner>().SpawnHourglass();
        }
        UpdateScore();
    }

    public float VelocityIncrement()
    {
        return velocityIncrement;
    }
}
