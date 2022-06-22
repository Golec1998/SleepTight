using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidController : MonoBehaviour
{

    public float maxSleep = 100f;
    float sleep;
    public float maxComfort = 100f;
    float comfort;

    public float sleepRegenerateRate = 1f;
    public float comfortRegenerateRate = 1f;

    void Start()
    {
        sleep = maxSleep;
        comfort = maxComfort / 2f;
    }

    void Update()
    {
        regenerate();
    }

    void regenerate()
    {
        if(comfort < maxComfort)
            comfort += Time.deltaTime * comfortRegenerateRate;
        
        sleep += comfort / maxComfort - 0.5f;
        if(sleep > maxSleep)
            sleep = maxSleep;
    }

    public void getComfortDamage(float damage)
    {
        comfort -= damage * Time.deltaTime;
    }

    public void getSleepDamage(float damage)
    {
        sleep -= damage * Time.deltaTime;
    }

    public float getSleep() { return sleep; }
    public float getComfort() { return comfort; }

}
