using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreFabItem : MonoBehaviour
{
    public Item item;
    public GameObject father;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Added()
    {
        Destroy(father);    
    }
}
