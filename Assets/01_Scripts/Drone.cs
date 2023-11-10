using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Drone : MonoBehaviour
{
    /*public float visionRange = 10f;
    public float evolutionTime = 20f;
    public GameObject adultDronePrefab;
    public Transform player;

    public float speed = 4f;
    public float escapeVelocity = 6f;
    public float maxLife = 2f;
    float life = 2f;

    private bool evolved = false;*/

    //Prueba Movimiento Random
    public int rutina;
    public float cromentro;
    public Animator ani;
    public Quaternion angulo;
    public float grado;


    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
       /* player = GameObject.FindGameObjectWithTag("Player").transform;
        Invoke("Evolve", evolutionTime);*/
    }

    // Update is called once per frame
    void Update()
    {
        comportamiento();
       /* if (!evolved)
        {
            EscapeFromPlayer();
        }*/
    }

    void Evolve()
    {
       /* if (!evolved && life > 0)
        {
            evolved = true;
            GameObject newDrone = Instantiate(adultDronePrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }*/
    }

    /*void EscapeFromPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= visionRange)
        {
            Vector3 escapeDirection = transform.position - player.position;

            escapeDirection.Normalize();
            transform.Translate(escapeDirection * speed * Time.deltaTime);
        }
    }*/

    void comportamiento()
    {
        cromentro += 1 * Time.deltaTime;

        if (cromentro >= 4)
        {
            rutina = UnityEngine.Random.Range(0, 2);
            cromentro = 0;
        }

        switch(rutina)
        {
            case 0:
                ani.SetBool("walk", false);
                break; 
            case 1:
                grado = UnityEngine.Random.Range(0, 360);
                angulo = Quaternion.Euler(0, grado, 0);
                rutina++;
                break;
            case 2:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                ani.SetBool("walk", true);
                break;

        }
    }
}
