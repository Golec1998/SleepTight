using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoltergeistAI : MonoBehaviour
{

    public float maxHealth = 10f;
    public float regenerationSpeed = 0.5f;
    float health;
    float alive;

    public Animator animator;

    Renderer thisEnemy;
    Renderer thisEnemyEyes;

    void Start()
    {
        thisEnemy = transform.Find("Body").GetComponent<Renderer>();
        thisEnemyEyes = transform.Find("Eyes").GetComponent<Renderer>();
        health = maxHealth;
        alive = 1;
    }

    [System.Obsolete]
    void Update()
    {
        if(animator.GetBool("isDead") && alive > 0)
            alive -= 0.5f * Time.deltaTime;
        thisEnemy.materials[0].SetFloat("_Alive", alive);
        thisEnemyEyes.materials[0].SetFloat("_Alive", alive);

        regenerate();
    }

    [System.Obsolete]
    public void getDamage()
    {
        //Debug.Log("Ouch!");
        health -= 3;
        if (health <= 0)
        {
            //Debug.Log("Dead");
            animator.SetBool("isDead", true);
            thisEnemy.materials[0].SetFloat("Alive", 0);
            GetComponent<Collider>().enabled = false;
            Destroy(transform.Find("LifeShard").gameObject);
            this.Invoke(() => { Destroy(transform.root.gameObject); }, 5f);
        }
    }

    void regenerate()
    {
        if (!animator.GetBool("isDead") && health < maxHealth)
            health += regenerationSpeed * Time.deltaTime;
    }

}
