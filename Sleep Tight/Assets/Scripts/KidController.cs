using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class KidController : MonoBehaviour
{

    public float maxSleep = 100f;
    float sleep;
    public float maxComfort = 100f;
    float comfort;

    public float sleepRegenerateRate = 1f;
    public float comfortRegenerateRate = 1f;
    bool canRegenerate = true;

    [Space]
    public float checkSurroundingsRange = 2f;
    public LayerMask enemyLayers;
    bool sawMonster = false;
    string eventPath = "event:/Kid/Scream";

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
        if(!sawMonster)
        {
            comfort = 0;
            sleep = 0;
            
            Collider[] enemies = Physics.OverlapSphere(transform.position, checkSurroundingsRange, enemyLayers);
            if(enemies.Length > 0)
            {
                sawMonster = true;
                playScream();
            }
        }
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

    void playScream()
    {
        EventInstance Sound = RuntimeManager.CreateInstance(eventPath);
        RuntimeManager.AttachInstanceToGameObject(Sound, transform, GetComponent<Rigidbody>());

        Sound.start();
        Sound.release();
    }

    public float getSleep() { return sleep; }
    public float getComfort() { return comfort; }
    public float getWokenUpCount() { return wokenUp; }
    public bool getSawMonster() { return sawMonster; }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkSurroundingsRange);
    }

}
