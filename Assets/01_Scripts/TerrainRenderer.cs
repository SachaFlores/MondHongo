using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainRenderer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Terrain;
    public float distanceToRender;
    private Transform player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform chunk in Terrain.transform)
        {
            MeshRenderer render;
            MeshRenderer renderLake;
            render = chunk.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
            if (render != null)
            {
                try
                {
                    renderLake = chunk.transform.GetChild(1).GetComponent<MeshRenderer>();
                }
                catch
                {
                    renderLake = null;
                }


                if (checkDistance(chunk))
                {
                    render.enabled = true;
                    if (renderLake != null)
                    {
                        renderLake.enabled = true;
                    }

                }
                else
                {
                    render.enabled = false;
                    if (renderLake != null)
                    {
                        renderLake.enabled = false;
                    }
                }

            }

        }
    }

    bool checkDistance(Transform chunk)
    {
        if (player != null && chunk != null)
        {
            float distancia = Vector3.Distance(player.position, chunk.position);

            if (distancia <= distanceToRender)
            {
                return true;
            } 
        }
        return false;
    }
}
