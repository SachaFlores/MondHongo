using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    public float range = 15f;
    public float speed = 3f;
    public float MaxLife;
    public float life;
    private Transform player;
    private bool isChasing = false;

    public Transform attackPoint_R;
    public Transform attackPoint_L;
    public Transform attackPoint_2R;
    public Transform attackPoint_2L;

    public GameObject Rock;
    public bool canRock = true;
    public float timetoRock = 0;
    public float timeBtnRock = 3;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= range)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }
        rotateToTarget();
        checkIfCanRock();
        if (isChasing)
        {
            if(canRock)
            {
                rockAttack();
                canRock = false;
            }
        }

    }

    void checkIfCanRock()
    {
        if (!canRock)
        {
            if (timetoRock < timeBtnRock)
            {
                timetoRock += Time.deltaTime;
            }
            else
            {
                timetoRock = 0;
                canRock = true;
            }
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

    void rockAttack()
    {
        Instantiate(Rock, attackPoint_R.position, transform.rotation);
        Instantiate(Rock, attackPoint_L.position, transform.rotation);
        Instantiate(Rock, attackPoint_2R.position, transform.rotation);
        Instantiate(Rock, attackPoint_2L.position, transform.rotation);


    }

    public void takeDamage(float amount)
    {
        life += -amount;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
