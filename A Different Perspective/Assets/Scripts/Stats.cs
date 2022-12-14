using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour
{
    public float maxHealth = 100;
    public float health = 100;
    public float water = 100;
    public float hunger = 100;
    public float stamina = 100;
    private float healthDrain = 2f;
    private float waterDrain = 0.9f;
    private float hungerDrain = 0.5f;
    private float StaminaDrain = 1f;
    public Slider healthslider, hungerSlider, waterSlider, staminaSlider;
    public LightingManager lighting;


    private void Awake()
    {
        health = maxHealth;
    }

    public void Update()
    {
        healthslider.value = health;
        waterSlider.value = water;
        hungerSlider.value = hunger;
        staminaSlider.value = stamina;
        WaterStat();
        HungerStat();
        StaminaStat();
    }
 

    public void WaterStat()
    {
        water -= waterDrain * Time.deltaTime;

        if (water < 0)
        {
            water = 0;
            health -= healthDrain * Time.deltaTime;

            if (health <= 0)
            {
                health = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void HungerStat()
    {
        hunger -= hungerDrain * Time.deltaTime;

        if (hunger < 0)
        {
            hunger = 0;
            health -= healthDrain * Time.deltaTime;

            if (health <= 0)
            {
                health = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void StaminaStat()
    {

        if (lighting.TimeofDay >= 20)
        {
            stamina -= StaminaDrain * Time.deltaTime;

            if (stamina <= 0)
            {
                stamina = 0;
                health -= healthDrain * Time.deltaTime;

                if (health <= 0)
                {
                    health = 0;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }
    public void GetWater(float gain)
    {
        if (gain > 0)
        {
            if (water < 90)
                water += gain;
            else
                water = 100;
        }
    }
    public void GetFood(float gain)
    {
        if (gain > 0)
        {
            if (hunger < 90)
                hunger += gain;
            else
                hunger = 100;
        }
    }
}
