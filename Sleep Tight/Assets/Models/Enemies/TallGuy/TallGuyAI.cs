using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallGuyAI : MonoBehaviour
{
    public float maxHealth = 20f;
    public float regenerationSpeed = 1.5f;
    float health;

    public Animator animator;

    Renderer thisEnemy;
    //public Transform thisEnemyHPShard;
    //Renderer thisEnemyHP;

    [System.Obsolete]
    void Start()
    {
        thisEnemy = transform.FindChild("Body").GetComponent<Renderer>();
        //thisEnemyHP = thisEnemyHPShard.FindChild("HP").GetComponent<Renderer>();
        health = maxHealth;
    }

    [System.Obsolete]
    void Update()
    {
        thisEnemy.materials[0].SetFloat("_HP", health / maxHealth);
        if (animator.GetBool("isDead"))
        {
            transform.FindChild("TallGuyCameraEffects").position = new Vector3(transform.FindChild("TallGuyCameraEffects").position.x, transform.FindChild("TallGuyCameraEffects").position.y + 2f * Time.deltaTime, transform.FindChild("TallGuyCameraEffects").position.z);
            health -= 5 * Time.deltaTime;
        }
        //else
            //thisEnemyHP.materials[0].SetFloat("_Fill", health / maxHealth);

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
            thisEnemy.materials[1].SetFloat("_Alive", 0);
            GetComponent<Collider>().enabled = false;
            //Destroy(transform.FindChild("LifeShard").gameObject);
            this.Invoke(() => { Destroy(transform.root.gameObject); }, 5f);
        }
    }

    void regenerate()
    {
        if (!animator.GetBool("isDead") && health < maxHealth)
            health += regenerationSpeed * Time.deltaTime;
    }

}
