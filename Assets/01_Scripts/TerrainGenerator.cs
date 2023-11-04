using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    
    public List<GameObject> chunk;
    public List<GameObject> mushWood;
    void Start()
    {
        for (int i = -3; i < 3; i++)
        {
            for (int j = -3; j < 3; j++)
            {
                int type = Random.Range(0, chunk.Count);
                if(type == 1)
                {
                    type = Random.Range(0, chunk.Count);
                    
                }
                GameObject nuevoChunk = Instantiate(chunk[type], new Vector3(transform.position.x + (i * 150), transform.position.y , transform.position.z + (j * 150)), chunk[type].transform.rotation);
                nuevoChunk.transform.parent = transform;

                Transform child = nuevoChunk.transform.GetChild(0).GetChild(0);
                MeshCollider collider = child.GetComponent<MeshCollider>();

                if(collider != null && type != 1)
                {
                    for (int k = 0; k < 25; k++)
                    {
                        Vector3 posicionAlAzar = GenerateExtra(collider);
                        RaycastHit hit;
                        if (Physics.Raycast(posicionAlAzar, Vector3.down, out hit, 0.6f) &&  !Mathf.Approximately(posicionAlAzar.y, collider.bounds.min.y))
                        {
                            int dir = Random.Range(0,4);
                            int woodType = Random.Range(0, mushWood.Count);
                            float grad = 0;
                            switch(dir)
                            {
                                case 0:
                                    grad = 0;
                                    break;
                                case 1:
                                    grad = 90;
                                    break;
                                case 2:
                                    grad = 180;
                                    break;
                                case 3:
                                    grad = 270;
                                    break;
                            }
                            GameObject Tree = Instantiate(mushWood[woodType], posicionAlAzar, Quaternion.Euler(180 , mushWood[woodType].transform.rotation.y + grad, mushWood[woodType].transform.rotation.z));
                            Tree.transform.parent = transform;
                        }
                        else
                        {
                            k--;
                        }
                    }
                    
                }
            }
        }
    }
    Vector3 GenerateExtra(MeshCollider collider)
    {
        Bounds bounds = collider.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = bounds.max.y; 
        float z = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(x, y, z);
    }
}
