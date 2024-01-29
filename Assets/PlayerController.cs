using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;

    [Header("Move")]
    public float speed;
    public float rotationSpeed;
    private Vector2 move, mouseLook, joystickLook;
    private Vector3 rotationTarget;
    public bool isPc;

    [Header("Dash")]
    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;
    private bool isDashing = false;
    public InputAction dashAction;

    [Header("Melee Attack")]
    public GameObject guantes;
    public GameObject guanteL;
    public GameObject guanteR;
    private Animator guantesAnimator;
    private Animator LguanteAnimator;
    private Animator RguanteAnimator;
    public float regularAttackDamage = 10f; // Damage for first two attacks
    public float thirdAttackDamage = 20f;   // Damage for the third attack
    private bool isAttacking = false;
    private int attackCount = 0;
    private float lastAttackTime = 0f;
    public float attacktime;
    public InputAction attackAction;

    [Header("Shoot")]
    public InputAction shootAction;

    [Header("References")]
    public Health playerHealth; // Reference to the Health script



    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }

    public void OnJoystickLook(InputAction.CallbackContext context)
    {
        joystickLook = context.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UnityEngine.Debug.Log("Dash Button Press");
            StartDash();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UnityEngine.Debug.Log("Attack Button Press");
            MeleeAttack();
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UnityEngine.Debug.Log("Shoot");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dashAction.performed += OnDash;
        dashAction.Enable();

        attackAction.performed += OnAttack;
        attackAction.Enable();

        shootAction.Enable();

        LguanteAnimator = guanteL.GetComponent<Animator>();
        RguanteAnimator = guanteR.GetComponent<Animator>();
        guantesAnimator = guantes.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isPc)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mouseLook);

            if (Physics.Raycast(ray, out hit))
            {
                rotationTarget = hit.point;
            }

            movePlayerWithAim();
        }
        else
        {
            if (joystickLook.x == 0 && joystickLook.y == 0)
            {
                movePlayer();
            }
            else
            {
                movePlayerWithAim();
            }
        }
    }

    public void movePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed);
        }

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    public void movePlayerWithAim()
    {
        if (isPc)
        {
            var lookPos = rotationTarget - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);

            Vector3 aimDirection = new Vector3(rotationTarget.x, 0f, rotationTarget.z);

            if(aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed);
            }
        }
        else
        {
            Vector3 aimDirection = new Vector3(joystickLook.x, 0f, joystickLook.y);

            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimDirection), rotationSpeed);
            }
        }

        Vector3 motion = new Vector3(move.x, 0f, move.y);

        transform.Translate(motion * speed * Time.deltaTime, Space.World);
    }

    private void StartDash()
    {
        if (!isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;

        // Store the original speed
        float originalSpeed = speed;

        // Increase the speed temporarily for the dash
        speed = dashSpeed;

        yield return new WaitForSeconds(dashTime);

        // Reset the speed after the dash
        speed = originalSpeed;

        yield return new WaitForSeconds(dashCooldown);

        isDashing = false;
    }

    public void MeleeAttack()
    {
        if (!isAttacking)
        {
            float currentTime = Time.time;

            // Check if the time since the last attack is within 0.5 seconds
            if (currentTime - lastAttackTime <= 0.5f)
            {
                attackCount++;
            }
            else
            {
                attackCount = 1; // Reset the combo
            }

            lastAttackTime = currentTime;

            // Apply different damage based on attack count
            float currentDamage = (attackCount == 3) ? thirdAttackDamage : regularAttackDamage;

            StartCoroutine(ComboAttack());

            // Apply damage to the enemy if it has a Health script
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                Health enemyHealth = hit.collider.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(currentDamage);
                }
            }
        }
    }

    private IEnumerator ComboAttack()
    {
        isAttacking = true;

        if (attackCount == 1)
        {
            LguanteAnimator.Play("Golpe 1"); // First Hit
        }
        else if (attackCount == 2)
        {
            RguanteAnimator.Play("Golpe 2"); // Second Hit
        }
        else if (attackCount >= 3)
        {
            guantesAnimator.Play("Golpe 3"); // Third Hit
            attackCount = 0; // Reset attack count
        }

        yield return new WaitForSeconds(attacktime);

        isAttacking = false;
    }
}
