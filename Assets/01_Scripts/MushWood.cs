using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushWood : MonoBehaviour
{
    public float life;
    public GameObject father;
    public GameObject mushWood;
    public GameObject mush;

    float cantWood;
    float cantMush;
    
    void Start()
    {
        cantWood = (int)Random.Range(5f, 15f);
        cantMush = (int)Random.Range(5f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float amount)
    {
        life -= amount;
        if(life <= 0)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            for (int i = 0; i< cantWood; i++)
            {
                position.y += 1; 
                
                Instantiate(mushWood, position, transform.rotation);
            }
            for (int j = 0; j < cantMush; j++)
            {
                position.y += 1;
                
                Instantiate(mush, position, transform.rotation);
            }
            Destroy(father);
        }
        Debug.Log("aaaa");
    }
}
