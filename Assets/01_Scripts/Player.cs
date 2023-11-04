using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform player;
    public Transform cam;
    public Rigidbody rb;

    public float mouseSensitivity = 2.0f;
    public float moveSpeed = 5;

    private float rotationX = 0;
    private float rotationY = 0;

    private bool canJump = true;
    public float jumpForce = 7f;

    public Transform attackPoint;
    public float attackRange = 2f;
    public LayerMask enemyLayers;
    private bool attack = true;
    public float damage = 3f;
    private float couldDownMelee = 0f;
    public float Timer = 1.0f;


    public float MaxLife;
    public float life;

    public float maxHunger;
    public float maxThirst;
    public float HungerPerSec;
    public float thirstPerSec;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    bool dash = false;
    bool canDash = true;
    public float couldDownDash = 0f;
    public float TimeToDash = 3f;
    public float DashRecover = 0f;
    // Update is called once per frame
    void Update()
    {
        HungerNThirst();
        CheckIfCanDash();
        RotateCamera();
        MovePlayer();
        Jump();
        IsMelee();
        MeleeAttack();
    }
    void HungerNThirst()
    {
        maxHunger += -HungerPerSec;
        maxThirst += -thirstPerSec;
        if(maxHunger <= 0 || maxThirst<=0)
        {
            Debug.Log("rip");
        }
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationX -= mouseX;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, -25f, 55f);

        cam.localRotation = Quaternion.Euler(rotationY, 0, 0);

        player.Rotate(Vector3.up * mouseX);
    }

    void CheckIfCanDash ()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dash = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            couldDownDash = 0;
            dash = false;
            canDash = false;
        }

        if (!canDash)
        {
            if (DashRecover < 2)
            {
                DashRecover += Time.deltaTime;
                dash = false;
            }
            else
            {
                DashRecover = 0;
                canDash = true;
            }
        }
        if (dash && canDash)
        {
            if (couldDownDash < TimeToDash)
            {
                couldDownDash += Time.deltaTime;
            }
            else
            {
                couldDownDash = 0;
                dash = false;
                canDash = false;
            }
        } 
    }

    void MovePlayer()
    {
        float speed = moveSpeed;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if(dash)
        {
            speed = speed * 5;   
        }
        Vector3 moveDirection = (player.forward * z + player.right * x).normalized;
        rb.velocity = new Vector3( moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);
    }

    void Jump()
    {
        if (canJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);

                canJump = false;
            }
        }
    }

    void MeleeAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (attack)
            {
                Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
                foreach (Collider enemy in hitEnemies)
                {
                    if(enemy.CompareTag("Zombie"))
                    {
                        enemy.GetComponent<Zombie>().takeDamage(damage);
                    }
                    else if (enemy.CompareTag("Golem"))
                    {
                        enemy.GetComponent<Golem>().takeDamage(damage);
                    }
                    
                }
                attack = false;
            }
            else
            {
                Debug.Log("I can't attack");
            }
        }
    }
    void IsMelee()
    {
        if (!attack)
        {
            if (couldDownMelee < Timer)
            {
                couldDownMelee += Time.deltaTime;
            }
            else
            {
                couldDownMelee = 0;
                attack = true;
            }
        }
    }

    public void takeDamage(float amount)
    {
        life += -amount;
        rb.AddForce(Vector2.up * 3, ForceMode.Impulse);
        Debug.Log(" Me atacaron");
        if (life <= 0)
        {
            //Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
}
