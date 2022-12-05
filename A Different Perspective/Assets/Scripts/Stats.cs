using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public float maxHealth = 100;
    public float health = 100;
    public float water = 100;
    public float hunger = 100;
    private float healthDrain = 2f;
    private float waterDrain = 0.9f;
    private float hungerDrain = 0.5f;
    public Slider healthslider, hungerSlider, waterSlider;


    private void Awake()
    {
        health = maxHealth;
    }

    public void Update()
    {
        healthslider.value = health;
        waterSlider.value = water;
        hungerSlider.value = hunger;
        WaterStat();
        HungerStat();
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
                Debug.Log("Death");
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
                Debug.Log("Death");
            }
        }
    }



}
