using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class HouseLight : MonoBehaviour
{
    public List<Light2D> houseLights;

    //Private vars

    
    void Start()
    {
        GetAllHouseLights();
    }

    void Update()
    {
        LightTimer();
    }
    
    //Get all houselights in scene and add them to the list
    private void GetAllHouseLights()
    {
        GameObject[] allHouseLights = GameObject.FindGameObjectsWithTag("Houselight");
        foreach (GameObject light in allHouseLights)
        {
            houseLights.Add(light.GetComponent<Light2D>());
        }
    }

    private void LightTimer()
    {
        if (MainLighting.instance.globalLight.intensity < 0.7f)
        {
            LightSwitch(true);
        }
        else
        {
            LightSwitch(false);
        }
    }

    private void LightSwitch(bool truefalse)
    {
        foreach (Light2D item in houseLights)
        {
            item.gameObject.SetActive(truefalse);
        }
    }
}
