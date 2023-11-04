
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(5, 10);
        float y = Random.Range(5, 10);
        float z = Random.Range(-1,1);
        rb.AddForce(Vector3.up * x, ForceMode.Impulse);
        rb.AddForce(Vector3.forward * -y, ForceMode.Impulse);
        rb.AddForce(Vector3.left * z, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        
    }
}
