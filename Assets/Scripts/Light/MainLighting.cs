using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MainLighting : MonoBehaviour
{
    public static MainLighting instance;

    [Header("Day/Night cycle settings")]
    public Light2D globalLight;
    public float dayNightTimer;
    public float speed;
    public float dayIntensity;
    public float nightIntensity;

    //private vars
    private float timer;
    [HideInInspector] public float globalTimer;
    private float lightIntensity;

    private void Awake()
    {
        instance = this;
    }

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
    }
}
