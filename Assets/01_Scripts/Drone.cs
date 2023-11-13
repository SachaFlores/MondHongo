using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Drone : MonoBehaviour
{
    public float visionRange = 10f;
    public float evolutionTime = 20f;
    public GameObject adultDronePrefab;
    public Rigidbody rb;
    //public Transform player;

    public float speed = 4f;
    float life = 2f;

    private bool evolved = false;

    // Movimiento Random
    public int routine;
    public float cromentro;
    public Animator ani;
    public Quaternion angle;
    public float degree;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        player = GameObject.Find("Player");
        Invoke("Evolve", evolutionTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!evolved)
        {
            Behavior();
        }
    }

    void Evolve()
    {
        if (!evolved && life > 0)
        {
            evolved = true;
            GameObject newDrone = Instantiate(adultDronePrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    /* void EscapeFromPlayer()
     {
         float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

         if (distanceToPlayer <= visionRange)
         {
             Vector3 escapeDirection = transform.position - player.transform.position;

             escapeDirection.Normalize();
             transform.Translate(escapeDirection * speed * Time.deltaTime);
         }
     }*/

    public void takeDamage(float amount)
    {
        life -= amount;
        rb.AddForce(Vector3.up * 3, ForceMode.Impulse);

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Behavior()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 5)
        {
            ani.SetBool("run", false);
            cromentro += 1 * Time.deltaTime;

            if (cromentro >= 4)
            {
                routine = UnityEngine.Random.Range(0, 2);
                cromentro = 0;
            }

            switch (routine)
            {
                case 0:
                    ani.SetBool("walk", false);
                    break;
                case 1:
                    degree = UnityEngine.Random.Range(0, 360);
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
            var lookPos = transform.position - player.transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);
            ani.SetBool("walk", false);
            ani.SetBool("run", true);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
