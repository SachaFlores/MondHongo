using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eggs : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject dronePrefab;
    public TypeDrone drone = TypeDrone.Drone_1;

    public float transformationTime = 10f;
    float life = 2f;
    public float maxLife = 2f;

    private bool hatched = false;

    // Start is called before the first frame update
    void Start()
    {
        life = maxLife;

        Invoke("Born", transformationTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float amount)
    {
        life += -amount;
        if (life <= 0)
        {
            DestroyEgg();
        }
    }

    void DestroyEgg()
    {
        if(!hatched)
        {
            hatched = true;
            Destroy(gameObject);
        }
    }

    void Born()
    {
        if(!hatched && life > 0)
        {
            hatched = true;
            GameObject newDrone = Instantiate(dronePrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public enum TypeDrone
    {
        Drone_1,
        Drone_2, 
        Drone_3, 
        Drone_4,
    }
}
