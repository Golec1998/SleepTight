using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float maxHealth = 20f;
    public float regenerationSpeed = 1.5f;
    float health;

    public Animator animator;

    Renderer thisGuy;
    public Transform thisGuysHPShard;
    Renderer thisGuysHP;

    [System.Obsolete]
    void Start()
    {
        thisGuy = transform.FindChild("Body").GetComponent<Renderer>();
        thisGuysHP = thisGuysHPShard.FindChild("HP").GetComponent<Renderer>();
        health = maxHealth;
    }

    [System.Obsolete]
    void Update()
    {
        thisGuy.materials[0].SetFloat("HP", health / maxHealth);
        if (animator.GetBool("isDead"))
        {
            transform.FindChild("TallGuyCameraEffects").position = new Vector3(transform.FindChild("TallGuyCameraEffects").position.x, transform.FindChild("TallGuyCameraEffects").position.y + 2f * Time.deltaTime, transform.FindChild("TallGuyCameraEffects").position.z);
            health -= 5 * Time.deltaTime;
        }
        else
            thisGuysHP.materials[0].SetFloat("Fill", health / maxHealth);

        regenerate();

        Debug.Log(health);

    }

    [System.Obsolete]
    public void getDamage()
    {
        Debug.Log("Ouch!");
        health -= 3;
        if (health <= 0)
        {
            Debug.Log("Dead");
            animator.SetBool("isDead", true);
            thisGuy.materials[1].SetFloat("Alive", 0);
            GetComponent<Collider>().enabled = false;
            Destroy(transform.FindChild("LifeShard").gameObject);
            this.Invoke(() => { Destroy(transform.root.gameObject); }, 5f);
        }
    }

    void regenerate()
    {
        if (!animator.GetBool("isDead"))
            health += regenerationSpeed * Time.deltaTime;
    }

}
