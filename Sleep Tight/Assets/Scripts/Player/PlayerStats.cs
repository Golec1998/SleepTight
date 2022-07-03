using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public Transform kid;
    float health;
    public float maxHealth;
    float energy;
    public float maxEnergy;
    public float healthRegenerateRate;
    public float energyRegenerateRate;

    [Space]
    [Header ("For debug only")]
    public bool isDying = false;

    void Start()
    {
        health = maxHealth;
        energy = maxEnergy;
    }

    void Update()
    {
        regenerate();


        //Debug
        if(isDying)
        {
            health -= Time.deltaTime;
            energy -= Time.deltaTime;
        }
    }

    public float getHealth() { return health; }
    public float getEnergy() { return energy; }

    void regenerate()
    {
        float regenerateMultiplier = 1f;
        float dist = Vector3.Distance(transform.position, kid.position);

        if(dist < 3.5f)
            regenerateMultiplier = 7f / dist;

        energy += energyRegenerateRate * Time.deltaTime * regenerateMultiplier;
        if(energy > maxEnergy)
            energy = maxEnergy;
        else if(energy < 0)
            energy = 0;

        health += ((energy / maxEnergy) - 0.2f) * healthRegenerateRate * Time.deltaTime * regenerateMultiplier;
        if(health > maxHealth)
            health = maxHealth;
    }

    public bool useEnergy(float amount)
    {
        bool result = false;

        if(energy > amount)
        {
            result = true;
            energy -= amount;
        }

        return result;
    }

}
