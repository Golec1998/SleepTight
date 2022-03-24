using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Transform combatCamPosition;
    public Vector3 combatCameraOffset;
    public Animator cameraAnimator;
    public Animator characterAnimator;

    [Space]

    public float walkSpeed = 3.5f;
    public float walkAcceleration = 25f;
    public float walkDeacceleration = 10f;

    [Space]

    public float runSpeed = 7f;
    public float runAcceleration = 15f;
    public float runDeacceleration = 8f;

    [Space]

    public float combatSpeed = 2f;
    public float combatDeacceleration = 5f;
    public float dashSpeed = 10f;
    public float dashCooldown = 0.5f;
    public float attackCooldown = 0.3f;

    [Space]

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    [Space]

    public float jumpPower = 6f;
    public float multiJumpPower = 4f;
    public int multiJumpCount = 1;

    [Space]

    public float gravity = 12f;

    [Space]

    public float turnSmoothTime = 0.1f;
    public float groundDistance = 0.2f;

    float maxSpeed;
    float speed = 0f;
    float acceleration;
    float deacceleration;
    int multiJumpCounter = 0;
    float turnSmoothVelocity;
    float targetAngle = 0;
    float angle = 0;
    float angleDif = 0;

    Vector3 velocity;
    bool isGrounded = true;
    bool isMoving = false;
    bool isSprinting = false;
    bool combatMode = false;
    bool canDash = true;
    bool canAttack = true;

    private void Start()
    {
        maxSpeed = walkSpeed;
        acceleration = walkAcceleration;
        deacceleration = walkDeacceleration;
    }

    void Update()
    {
        if (Input.GetButtonDown("CombatMode"))
        {
            characterAnimator.SetBool("isMoving", false);
            characterAnimator.SetBool("isSprinting", false);
            combatMode = !combatMode;
            combatCamPosition.position = transform.position + combatCameraOffset;
        }

        float distanceToCombatCamera = Vector3.Distance(transform.position, combatCamPosition.position);
        if (distanceToCombatCamera > 12 || distanceToCombatCamera < 5)
            combatMode = false;

        cameraAnimator.SetBool("combatMode", combatMode);
        characterAnimator.SetBool("isInCombatMode", combatMode);

        if (!combatMode)
            moveModeControls();
        else
            combatModeControls();
    }

    void moveModeControls()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        characterAnimator.SetBool("isInAir", !isGrounded);
        if (isGrounded)
            characterAnimator.ResetTrigger("jump");

        if (isGrounded && velocity.y < 0)
        {
            multiJumpCounter = 0;
            velocity.y = -5f;
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                velocity.y = jumpPower;
                characterAnimator.SetTrigger("jump");
            }
            else if (multiJumpCounter < multiJumpCount)
            {
                multiJumpCounter++;
                if (velocity.y < 0)
                    velocity.y = multiJumpPower;
                else
                    velocity.y += multiJumpPower;

                if (velocity.y > jumpPower)
                    velocity.y = jumpPower;
                characterAnimator.SetTrigger("jump");
            }
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(h, 0f, v).normalized;

        if (Input.GetButtonDown("Sprint"))
            isSprinting = true;
        else if (Input.GetButtonUp("Sprint"))
            isSprinting = false;

        if (isSprinting)
        {
            maxSpeed = runSpeed;

            if (isGrounded)
            {
                acceleration = runAcceleration;
                deacceleration = runDeacceleration;
            }
            else
            {
                acceleration = runAcceleration / 10f;
                deacceleration = runDeacceleration / 10f;
            }
        }
        else
        {
            maxSpeed = walkSpeed;

            if (isGrounded)
            {
                acceleration = walkAcceleration;
                deacceleration = walkDeacceleration;
            }
            else
            {
                acceleration = walkAcceleration / 10f;
                deacceleration = walkDeacceleration / 10f;
            }
        }

        if (direction.magnitude >= 0.1f)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            if (speed < maxSpeed)
                speed += acceleration * Time.deltaTime;
            else
                speed -= deacceleration * Time.deltaTime;

            angleDif = angle - targetAngle;
            isMoving = true;
        }
        else
        {
            angleDif = 0;
            speed -= deacceleration * Time.deltaTime;
            isMoving = false;
            isSprinting = false;
        }

        characterAnimator.SetBool("isMoving", isMoving);
        characterAnimator.SetBool("isSprinting", isSprinting);

        if (speed < 0 || (angleDif > 90 && angleDif < 270) || (angleDif < -90 && angleDif > -270))
            speed = 0;

        Vector3 moveDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
        controller.Move(moveDirection.normalized * speed * Time.deltaTime);

        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void combatModeControls()
    {

        maxSpeed = combatSpeed;
        deacceleration = combatDeacceleration;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        characterAnimator.SetBool("isInAir", !isGrounded);

        if (isGrounded && velocity.y < 0)
            velocity.y = -5f;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(h, 0f, v).normalized;

        if (Input.GetButtonDown("Attack") && canAttack)
        {
            canAttack = false;
            speed = 0;
            characterAnimator.SetTrigger("attack");
            this.Invoke(() => { canAttack = true; }, attackCooldown);
        }

        if (direction.magnitude >= 0.1f && canAttack)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            if(speed < maxSpeed)
            speed = maxSpeed;

            if (Input.GetButtonDown("Jump") && isGrounded && canDash)
            {
                canDash = false;
                speed = dashSpeed;
                characterAnimator.SetTrigger("dash");
                this.Invoke(() => { canDash = true; }, dashCooldown);
            }

            if (speed > maxSpeed)
                speed -= deacceleration * Time.deltaTime;
        }
        else
        {
            speed -= deacceleration * Time.deltaTime;
        }

        if (speed < 0)
            speed = 0;

        Vector3 moveDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
        controller.Move(moveDirection.normalized * speed * Time.deltaTime);

        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void dealDamage()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider enemy in hitEnemies)
            enemy.GetComponent<EnemyAI>().getDamage();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
