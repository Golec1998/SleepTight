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
    public Animator animator;

    [Space]

    public float walkSpeed = 8f;
    public float walkAcceleration = 50f;
    public float walkDeacceleration = 20f;

    public float runSpeed = 15f;
    public float runAcceleration = 30f;
    public float runDeacceleration = 15f;

    public float combatSpeed = 4f;
    public float combatDeacceleration = 10f;
    public float dashSpeed = 20f;

    float maxSpeed;
    float speed = 0f;
    float acceleration;
    float deacceleration;
    public float turnSmoothTime = 0.1f;
    public float gravity = 20f;
    public float jumpPower = 12f;
    public float multiJumpPower = 8f;
    public int multiJumpCount = 1;
    int multiJumpCounter = 0;
    public float groundDistance = 0.2f;
    float turnSmoothVelocity;
    float targetAngle = 0;
    float angle = 0;
    float angleDif = 0;

    Vector3 velocity;
    bool isGrounded;
    bool isSprinting;
    bool combatMode = false;

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
            combatMode = !combatMode;
            combatCamPosition.position = transform.position + combatCameraOffset;
        }

        if (Vector3.Distance(transform.position, combatCamPosition.position) > 30 || Vector3.Distance(transform.position, combatCamPosition.position) < 15)
            combatMode = false;

        animator.SetBool("combatMode", combatMode);

        if (!combatMode)
            moveModeControls();
        else
            combatModeControls();
    }

    void moveModeControls()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            multiJumpCounter = 0;
            velocity.y = -5f;
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
                velocity.y = jumpPower;
            else if (multiJumpCounter < multiJumpCount)
            {
                multiJumpCounter++;
                if (velocity.y < 0)
                    velocity.y = multiJumpPower;
                else
                    velocity.y += multiJumpPower;

                if (velocity.y > jumpPower)
                    velocity.y = jumpPower;
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
        }
        else
        {
            angleDif = 0;
            speed -= deacceleration * Time.deltaTime;
        }

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

        if (isGrounded && velocity.y < 0)
            velocity.y = -5f;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(h, 0f, v).normalized;

        if (direction.magnitude >= 0.1f)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            if(speed < maxSpeed)
            speed = maxSpeed;

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                speed = dashSpeed;
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

}
