using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollow : MonoBehaviour
{
    public GameObject player;

    void Start()
    {



        
    }

    void Update()
    {
        Vector3 newCamPos = new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y, Camera.main.transform.position.z);
        Camera.main.transform.position = newCamPos;


    }
}
