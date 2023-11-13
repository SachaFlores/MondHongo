using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class Zombie : MonoBehaviour
{
    public float range = 15f;
    public float speed = 3f;
    public float MaxLife ;
    public float life;
    private Transform player;
    private bool isChasing = false;

    public Rigidbody rb;
    //ataque ?¿
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask playerLayers;
    private bool attack = true;
    public float damage = 3f;
    private float couldDownMelee = 0f;
    public float Timer = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        IsMelee();
        if (distanceToPlayer <= range)
        {
            isChasing = true;
        }
        else
        {
            isChasing=false;
        }
        rotateToTarget();
        if (isChasing)
        {
            Quaternion rotation = transform.rotation;
            Vector3 moveDirection = rotation * Vector3.forward;
            rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);
        }
        if(distanceToPlayer <= 1)
        {
            MeleeAttack();
        }   
    }
    void rotateToTarget()
    {
        if (isChasing)
        {
            Vector3 dir = player.position - transform.position;
            float angleY = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angleY, 0);
        }
    }
    void MeleeAttack()
    {
        if (attack)
        {
            Collider[] hitplayers = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayers);
            foreach (Collider player in hitplayers)
            {
                player.GetComponent<Player>().takeDamage(damage);
            }
            attack = false;
        }
        else
        {
            Debug.Log("I can't attack");
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
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
}
