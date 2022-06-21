using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    float health;
    public float maxHealth;
    float energy;
    public float maxEnergy;

    [Space]
    [Header ("For debug only")]
    public bool isDying = false;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {



        //Debug
        if(isDying)
            health -= Time.deltaTime;
    }

    public float getHealth() { return health; }
    public float getEnergy() { return energy; }

}
