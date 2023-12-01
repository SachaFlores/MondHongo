
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        /*float x = Random.Range(5, 10);
        float y = Random.Range(5, 10);
        float z = Random.Range(-1,1);
        rb.AddForce(Vector3.up * x, ForceMode.Impulse);
        rb.AddForce(Vector3.forward * -y, ForceMode.Impulse);
        rb.AddForce(Vector3.left * z, ForceMode.Impulse);*/

        // Obtener la rotación actual del enemigo
        Quaternion enemyRotation = transform.rotation;

        // Obtener la dirección hacia adelante en la rotación actual
        Vector3 forwardDirection = enemyRotation * Vector3.forward;

        // Calcular una fuerza random
        float forceMagnitude = Random.Range(8, 15); // Ajusta el rango según sea necesario

        // Escalar la fuerza random
        Vector3 randomForce = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));

        // Aplicar la fuerza hacia adelante
        Vector3 forwardForce = forwardDirection * forceMagnitude;

        // Aplicar la fuerza de elevación (hacia arriba)
        Vector3 upwardForce = Vector3.up * forceMagnitude;

        // Sumar ambas fuerzas
        Vector3 totalForce = forwardForce + randomForce + upwardForce;

        // Aplicar la fuerza al objeto Rigidbody
        rb.AddForce(totalForce, ForceMode.Impulse);
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
