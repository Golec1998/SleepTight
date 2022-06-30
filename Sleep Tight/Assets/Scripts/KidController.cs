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
    bool canRegenerate = true;

    int wokenUp = 0;

    void Start()
    {
        sleep = maxSleep;
        comfort = maxComfort;
    }

    void Update()
    {
        if(canRegenerate)
            regenerate();
        else
            checkSurroundings();
    }

    void regenerate()
    {
        if(comfort < maxComfort)
            comfort += Time.deltaTime * comfortRegenerateRate;
        else if(comfort < 0f)
            comfort = 0;
            
        sleep += (comfort / maxComfort - 0.5f) * sleepRegenerateRate * Time.deltaTime;
        if(sleep > maxSleep)
            sleep = maxSleep;
        else if(sleep < 0)
            wakeUp();
    }

    public void getComfortDamage(float damage)
    {
        comfort -= damage;
    }

    public void getSleepDamage(float damage)
    {
        sleep -= damage;
    }

    void checkSurroundings()
    {
        comfort = 0;
        sleep = 0;
        //TODO
    }

    void wakeUp()
    {
        canRegenerate = false;
        wokenUp++;

        this.Invoke(() => {
            canRegenerate = true;
            sleep = maxSleep / 2f;
            comfort = maxComfort / 2f;
        }, 3f);
    }

    public float getSleep() { return sleep; }
    public float getComfort() { return comfort; }
    public float getWokenUpCount() { return wokenUp; }

}
