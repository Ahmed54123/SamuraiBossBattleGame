using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint;
    bool reachedEndPath = false;

    Seeker seeker;
    Rigidbody2D enemyRb;

    public Transform enemyGraphics;
    void Start()
    {
        seeker = GetComponent<Seeker>();
        enemyRb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        
    }

    void UpdatePath()
    {

        if (seeker.IsDone())
        {
            seeker.StartPath(enemyRb.position, target.position, OnPathComplete);

        }
    }

    void FixedUpdate()
    {
        if(path == null)
        {
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndPath = true;
            return;

            
        }
        else
        {
            reachedEndPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - enemyRb.position);
        Vector2 force = direction * speed * Time.deltaTime;

        enemyRb.AddForce(force);

        float distance = Vector2.Distance(enemyRb.position, path.vectorPath[currentWaypoint]);

        if (distance< nextWaypointDistance)
        {
            currentWaypoint++;
        }

         if (enemyRb.velocity.x >= 0.01f)
        {
            enemyGraphics.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (enemyRb.velocity.x <= 0.01f)
        {
            enemyGraphics.localScale = new Vector3(1f, 1f, 1f);

        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
