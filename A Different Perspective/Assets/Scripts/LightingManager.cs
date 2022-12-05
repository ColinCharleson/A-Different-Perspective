using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{


    //Refrences
  [SerializeField] private Light DirectionalLight;
  [SerializeField] private Lighting Preset;
    //variables
  [SerializeField, Range(0,24)] private float TimeofDay = 10;
    private float timeCOunter = 0.1f;


    private void Update()
    {
        if(Preset == null)
        {
            return;
        }
        if(Application.isPlaying)
        {
            TimeofDay += timeCOunter * Time.deltaTime;
            TimeofDay %= 24;
            UpdateLighting(TimeofDay / 24);
            if(TimeofDay >= 20)
            {
                TimeofDay = 20;
            }
        }
    }

    private void UpdateLighting(float time)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(time);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(time);
     
        if(DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(time);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((time * 360) - 90f, -170, 0));
        }

    }
}
