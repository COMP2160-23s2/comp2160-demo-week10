using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformMove : MonoBehaviour
{
#region Serialized fields
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float acceleration = 20;
#endregion

#region Private fields
    private int next = 0;
    private float speed = 0;
    private Rigidbody2D rigidbody;
#endregion

#region Init
    void Awake() 
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        // reparent the waypoints so they are not attached to the platform
        for (int i = 0; i < waypoints.Length; i++) 
        {
            waypoints[i].parent = transform.parent;
        }

        // start at waypoint 0
        transform.position = waypoints[next].position;
        NextWaypoint();
    }
#endregion

#region Update
    void FixedUpdate()
    {
        Vector2 dir = (Vector2)waypoints[next].position - rigidbody.position;
        Accelerate(dir.magnitude);
        Move(dir);

        // Profiling
        GameStats.PlatformSpeed.Value = speed;
    }

    private void Move(Vector2 dir)
    {
        float move = speed * Time.fixedDeltaTime;
        if (move > dir.magnitude)
        {
            // stop if we would otherwise overshoot the waypoint
            speed = 0;            
        }
        rigidbody.velocity = speed * dir.normalized;
    }

    private void Accelerate(float distanceToWaypoint) 
    {
        // calculate how far from the waypoint we should start to brake
        // v^2 = u^2 + 2as 
        // 0 = u^2 + 2as
        // s = -u^2 / 2a

        // this assumes we get to maximum speed
        // if the waypoints are too close to one another,
        // we may never reach max speed and this code will not work
        float brakingDistance = maxSpeed * maxSpeed / acceleration / 2;

        // Profiling:
        GameStats.Distance.Value = distanceToWaypoint;
        GameStats.BrakingDistance.Value = brakingDistance;

        // accelerate or brake depending on the distance to the next waypoint
        if (distanceToWaypoint <= brakingDistance) 
        {
            // close to waypoint, slow down
            speed -= acceleration * Time.fixedDeltaTime;
            speed = Mathf.Max(speed, 0);
            if (speed == 0)
            {
                NextWaypoint();
            }
        }
        else 
        {
            // far from waypoint, speed up
            speed += acceleration * Time.fixedDeltaTime;
            speed = Mathf.Min(speed, maxSpeed);
        }
    }
    
    private void NextWaypoint()
    {
        next = (next + 1) % waypoints.Length;  // wrap to between 0 of #waypoints-1
    }
#endregion
}

