using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallGuyAI : MonoBehaviour
{
    public float maxHealth = 20f;
    public float regenerationSpeed = 1.5f;
    float health;

    public Animator animator;

    Transform[] path;
    Transform lastPoint;
    bool canMove = false;
    float movementTolerance = 0.02f;
    int movementTarget = 0;
    float timeOfMovement = 0f, movementSpeed = 0.7f, movementTimeLimit;

    [Space]
    bool canAttack = false;
    public GameObject kid;

    Renderer thisEnemy;

    [System.Obsolete]
    void Start()
    {
        thisEnemy = transform.FindChild("Body").GetComponent<Renderer>();
        health = maxHealth;
        lastPoint = transform;
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
        
        if(canMove) move();
        if(canAttack) invokeNightmares();

        regenerate();

        animator.SetBool("canMove", canMove);
        animator.SetBool("canAttack", canAttack);
    }

    [System.Obsolete]
    public void getDamage()
    {
        health -= 3;
        if (health <= 0)
        {
            canAttack = false;
            animator.SetBool("isDead", true);
            thisEnemy.materials[1].SetFloat("_Alive", 0);
            GetComponent<Collider>().enabled = false;
            this.Invoke(() => { Destroy(transform.root.gameObject); }, 5f);
        }
    }

    void regenerate()
    {
        if (!animator.GetBool("isDead") && health < maxHealth)
            health += regenerationSpeed * Time.deltaTime;
    }

    public void setPath(Transform[] newPath)
    {
        path = newPath;
        canMove = true;
    }

    public void setKid(GameObject newKid) { kid = newKid; }

    void move()
    {
        if(movementTarget < path.Length)
        {
            Debug.DrawLine(transform.position, path[movementTarget].position);

            movementTimeLimit = Vector3.Distance(lastPoint.position, path[movementTarget].position);
            timeOfMovement += Time.deltaTime;

            Quaternion targetRotation = Quaternion.identity;
            Vector3 targetDirection = (path[movementTarget].position - transform.position).normalized;
            targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 150f);

            transform.position = Vector3.Lerp(lastPoint.position, path[movementTarget].position, (timeOfMovement / movementTimeLimit) * movementSpeed);

            if(Vector3.Distance(transform.position, path[movementTarget].position) < movementTolerance)
            {
                timeOfMovement = 0f;
                movementTarget++;
                lastPoint = path[movementTarget - 1];
            }
        }
        else
        {
            canMove = false;
            canAttack = true;
        }
    }

    void invokeNightmares()
    {
        //Rotate to kid
        Quaternion targetRotation = Quaternion.identity;
        Vector3 targetDirection = (kid.transform.position - transform.position).normalized;
        targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 150f);

        //Attack kid
        kid.GetComponent<KidController>().getComfortDamage(2.6f * Time.deltaTime);
        kid.GetComponent<KidController>().getSleepDamage(1.7f * Time.deltaTime);
    }

}
