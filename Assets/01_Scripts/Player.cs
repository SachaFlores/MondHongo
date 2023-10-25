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

    public bool canJump = true;
    public float jumpForce = 7f;

    public Transform attackPoint;
    public float attackRange = 2f;
    public LayerMask enemyLayers;
    private bool attack = true; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
        MovePlayer();
        Jump();
        MeleeAttack();
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

    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 moveDirection = (player.forward * z + player.right * x).normalized;

        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
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
                    Debug.Log("mele" + enemy.name);
                }
                attack = false;
            }
            else
            {
                Debug.Log("I can't attack");
            }
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
