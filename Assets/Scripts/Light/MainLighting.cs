using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MainLighting : MonoBehaviour
{

    [Header("Day/Night cycle settings")]
    public Light2D globalLight;
    public float dayNightTimer;
    public float speed;
    public float dayIntensity;
    public float nightIntensity;

    public Light2D testLight;

    //private vars
    private float timer;
    [HideInInspector] public float globalTimer;
    private float lightIntensity;

    void Start()
    {
        timer = dayNightTimer;
        lightIntensity = dayIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        DayNightCycle();
    }

    private void DayNightCycle()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (lightIntensity < 0.5)
            {
                lightIntensity = dayIntensity;
            }
            else if (lightIntensity > 0.5)
            {
                lightIntensity = nightIntensity;
            }
            timer = dayNightTimer;
        }
        globalLight.intensity = Mathf.Lerp(globalLight.intensity, lightIntensity, speed * Time.deltaTime);
        Debug.Log(timer + " " + globalLight.intensity);

        //Below is for testing, this needs to be a separate script on every building light using information from this script to turn the lights on when its nighttime and turn them off during the day.
        if (globalLight.intensity < 0.5f)
        {
            testLight.gameObject.SetActive(true);
        }
        else
        {
            testLight.gameObject.SetActive(false);
        }
    }
}
