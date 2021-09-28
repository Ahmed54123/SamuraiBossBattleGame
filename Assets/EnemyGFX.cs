using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public Transform player;

    public bool isFlipped = false;


    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;

        }

        else if(transform.position.x > player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}



    //private void Start()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (rb.velocity.x >= 0f)
    //    {
    //        transform.localScale = new Vector3(1f, 1f, 1f);
    //    }
    //    else if (rb.velocity.x <= 0f)
    //    {
    //        transform.localScale = new Vector3(-1f, 1f, 1f);

    //    }
    //    } 
    //}


