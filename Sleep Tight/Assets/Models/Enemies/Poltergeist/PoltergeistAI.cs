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

    [Space]

    public Transform throwableChecker;
    public Transform spinner;
    public LayerMask throwableLayer;
    int[] throwablePosition = { -1, -1, -1, -1, -1 };
    public GameObject[] throwableSlots;

    Renderer thisEnemy;
    Renderer thisEnemyEyes;

    void Start()
    {
        thisEnemy = transform.Find("Body").GetComponent<Renderer>();
        thisEnemyEyes = transform.Find("Eyes").GetComponent<Renderer>();
        health = maxHealth;
        alive = 0;
        collectThrowables();
    }

    [System.Obsolete]
    void Update()
    {
        if(animator.GetBool("isDead"))
            alive -= 0.5f * Time.deltaTime;
        else
            alive += 0.35f * Time.deltaTime;
        if(alive < 0f)
            alive = 0f;
        else if(alive > 1f)
            alive = 1f;
        thisEnemy.materials[0].SetFloat("_Alive", alive);
        thisEnemyEyes.materials[0].SetFloat("_Alive", alive);

        regenerate();
    }

    void collectThrowables()
    {
        Collider[] throwables = Physics.OverlapSphere(throwableChecker.position, 3f, throwableLayer);
        int n = throwables.Length;
        if (n > 5)
            n = 5;
        int i = 0;

        //Tu zbieramy do karuzeli
        do
        {
            int position = Random.Range(0, 5);
            if (throwablePosition[position] == -1)
            {
                throwablePosition[position] = i;
                i++;
            }
        } while (i < n);
        throwables[0].transform.position = throwableSlots[0].transform.position;
    
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(throwableChecker.position, 3f);
        Gizmos.DrawWireSphere(spinner.position, 0.5f);
    }

}
