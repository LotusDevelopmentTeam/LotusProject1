using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{

    [Header("Settings")]
    public float time;
    public float speed;
    public float dayIntensity;
    public float nightIntensity;

    //Private vars
    private Light2D globalLight;
    private bool turnDay;
    private bool turnNight = true;
    private float timer;

    void Start()
    {
        globalLight = GetComponent<Light2D>();
        timer = time;
    }

    void Update()
    {
        DayNight();
    }

    private void DayNight()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (turnDay)
            {
                turnDay = false;
                turnNight = true;
            }
            else if (turnNight)
            {
                turnDay = true;
                turnNight = false;
            }
            timer = time;
        }
        if (turnNight)
        {
            globalLight.intensity = Mathf.Lerp(globalLight.intensity, nightIntensity, speed * Time.deltaTime);
        }
        if (turnDay)
        {
            globalLight.intensity = Mathf.Lerp(globalLight.intensity, dayIntensity, speed * Time.deltaTime);
        }
    }
}
