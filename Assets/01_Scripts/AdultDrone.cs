using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class AdultDrone : MonoBehaviour
{
    public Transform attackPoint;
    public Rigidbody rb;
    public float speed = 4f;
    public float damage = 2f;
    float life = 2f;

    public int routine;
    public float cromentro;
    public Animator ani;
    public Quaternion angle;
    public float degree;

    public GameObject target;
    public bool attacking;

    public float eggDropInterval = 40f;
    private float lastEggDropTime;

    public GameObject eggPrefab;

    void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("Player");
        lastEggDropTime = Time.time;
    }

    void Update()
    {
        Behavior();

        if (Time.time - lastEggDropTime >= eggDropInterval)
        {
            DropEgg();
            lastEggDropTime = Time.time;
        }
    }

    public void takeDamage(float amount)
    {
        life -= amount;
        rb.AddForce(Vector3.up * 3, ForceMode.Impulse);

        if (life <= 0)
        {
            Destroy(gameObject);
            Debug.Log("c murió");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && attacking)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.takeDamage(damage);
            }
        }
    }

    void Behavior()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 7)
        {
            ani.SetBool("run", false);
            cromentro += 1 * Time.deltaTime;

            if (cromentro >= 4)
            {
                routine = Random.Range(0, 2);
                cromentro = 0;
            }

            switch (routine)
            {
                case 0:
                    ani.SetBool("walk", false);
                    break;
                case 1:
                    degree = Random.Range(0, 360);
                    angle = Quaternion.Euler(0, degree, 0);
                    routine++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    ani.SetBool("walk", true);
                    break;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 1 && !attacking)
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                ani.SetBool("walk", false);
                ani.SetBool("run", true);
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                ani.SetBool("attack", false);
            }
            else
            {
                ani.SetBool("walk", false);
                ani.SetBool("run", false);
                ani.SetBool("attack", true);
                attacking = true;
            }
        }
    }
    public void Final_Ani()
    {
        ani.SetBool("attack", false);
        attacking = false;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(""))
        {
            print("daño");
        }
    }*/

    void DropEgg()
    {
        if (!attacking)
        {
            Instantiate(eggPrefab, transform.position, Quaternion.identity);
        }
    }
}
