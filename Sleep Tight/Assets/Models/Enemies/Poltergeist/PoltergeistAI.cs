using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

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
    public Transform[] throwableSlots;
    Collider[] throwables;
    int numberOfThrowables;
    bool readyToRotate = false;
    float timeToThrow;
    bool thrown = false;
    string eventPath = "event:/Enemies/Poltergeist/Wolololo";

    Renderer thisEnemy;
    Renderer thisEnemyEyes;

    [Space]
    [Header("For debug only!")]
    public bool returnToOrigin = false;
    public bool showActiveRadiuses = false;

    void Start()
    {
        thisEnemy = transform.Find("Body").GetComponent<Renderer>();
        thisEnemyEyes = transform.Find("Eyes").GetComponent<Renderer>();
        health = maxHealth;
        alive = 0;

        collectThrowables();
        this.Invoke(() => { readyToRotate = true; timeToThrow = Time.time + 10f; }, 3f);

        playSound();
    }

    [System.Obsolete]
    void Update()
    {
        if(animator.GetBool("isDead"))
        {
            alive -= 0.5f * Time.deltaTime;
            if(!thrown)
                returnThrowables();
        }
        else if(thrown)
            alive -= 0.2f * Time.deltaTime;
        else
            alive += 0.35f * Time.deltaTime;

        if(alive < 0f)
            alive = 0f;
        else if(alive > 1f)
            alive = 1f;

        thisEnemy.materials[0].SetFloat("_Alive", alive);
        thisEnemyEyes.materials[0].SetFloat("_Alive", alive);

        if (returnToOrigin) returnThrowables();
        else if (readyToRotate) rotateThrowables();

        regenerate();
    }

    void collectThrowables()
    {
        throwables = Physics.OverlapSphere(throwableChecker.position, 2f, throwableLayer);
        numberOfThrowables = throwables.Length;
        if (numberOfThrowables > 5)
            numberOfThrowables = 5;
        int i = 0;

        do
        {
            int position = Random.Range(0, 5);
            if (throwablePosition[position] == -1)
            {
                throwablePosition[position] = i;
                i++;
            }
        } while (i < 5);
    }

    [System.Obsolete]
    void rotateThrowables()
    {
        spinner.eulerAngles = new Vector3(
            spinner.eulerAngles.x,
            spinner.eulerAngles.y + Time.deltaTime * 200f,
            spinner.eulerAngles.z
        );

        for(int i = 0; i < numberOfThrowables; i++)
        {
            Transform tempThrowable = throwables[i].transform.Find("ThrowableOrigin").transform;
            tempThrowable.position = Vector3.Lerp(tempThrowable.position, new Vector3(throwableSlots[throwablePosition[i]].position.x, throwableSlots[throwablePosition[i]].position.y + Random.Range(-0.15f, 0.15f), throwableSlots[throwablePosition[i]].position.z), Time.deltaTime * 2f);
            tempThrowable.eulerAngles = Vector3.Lerp(tempThrowable.position, new Vector3(Random.Range(-45f, 45f), Random.Range(-180f, 180f), Random.Range(-45f, 45f)), Time.deltaTime * 2f);
        }
    
        if(Time.time > timeToThrow)
            throwThrowables();
    }

    void throwThrowables()
    {
        readyToRotate = false;
        thrown = true;
        GetComponent<Collider>().enabled = false;
        this.Invoke(() => { Destroy(transform.root.gameObject); }, 5f);

        for (int i = 0; i < throwables.Length; i++)
        {
            Transform tempThrowable = throwables[i].transform.Find("ThrowableOrigin").transform;
            tempThrowable.gameObject.AddComponent<Rigidbody>();
            tempThrowable.GetComponent<Rigidbody>().AddForce(
                    (tempThrowable.position.x - spinner.position.x) * 13f,
                    3f,
                    (tempThrowable.position.z - spinner.position.z) * 13f,
                    ForceMode.Impulse
                );
            tempThrowable.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-170f, 170f), Random.Range(-170f, 170f), Random.Range(-170f, 170f)), ForceMode.Impulse);
        }
    }

    void returnThrowables()
    {
        readyToRotate = false;

        for (int i = 0; i < numberOfThrowables; i++)
        {
            Transform tempThrowable = throwables[i].transform.Find("ThrowableOrigin").transform;
            tempThrowable.position = Vector3.Lerp(tempThrowable.position, tempThrowable.parent.transform.position, Time.deltaTime * 2f);
            tempThrowable.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    [System.Obsolete]
    public void getDamage()
    {
        health -= 3;
        if (health <= 0)
        {
            animator.SetBool("isDead", true);
            thisEnemy.materials[0].SetFloat("Alive", 0);
            GetComponent<Collider>().enabled = false;
            this.Invoke(() => { Destroy(transform.root.gameObject); }, 5f);
        }
        else
            animator.SetTrigger("isAttacked");
    }

    void regenerate()
    {
        if (!animator.GetBool("isDead") && health < maxHealth)
            health += regenerationSpeed * Time.deltaTime;
    }

    void playSound()
    {
        EventInstance Sound = RuntimeManager.CreateInstance(eventPath);
        RuntimeManager.AttachInstanceToGameObject(Sound, transform, GetComponent<Rigidbody>());

        Sound.start();
        Sound.release();
    }

    private void OnDrawGizmos()
    {
        if(showActiveRadiuses)
        {
            Gizmos.DrawWireSphere(throwableChecker.position, 2f);
            Gizmos.DrawWireSphere(throwableSlots[0].position, 0.05f);
            Gizmos.DrawWireSphere(throwableSlots[1].position, 0.05f);
            Gizmos.DrawWireSphere(throwableSlots[2].position, 0.05f);
            Gizmos.DrawWireSphere(throwableSlots[3].position, 0.05f);
            Gizmos.DrawWireSphere(throwableSlots[4].position, 0.05f);
        }
    }

}
