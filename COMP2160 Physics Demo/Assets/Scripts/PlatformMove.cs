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
        GameStats.PlatformSpeed.Value = speed;
        Move(dir);
    }

    private void Move(Vector2 dir)
    {
        float move = speed * Time.fixedDeltaTime;
        Debug.Log($"move = {move}");
        if (move > dir.magnitude)
        {
            // don't overshoot the waypoint
            rigidbody.MovePosition(waypoints[next].position);            
        }
        else
        {            
            rigidbody.MovePosition(rigidbody.position + move * dir.normalized);
        }

    }

    private float oldDistance = 0;
    private void Accelerate(float distanceToWaypoint) 
    {
        Debug.Log($"distance travelled = {oldDistance - distanceToWaypoint}");
        oldDistance = distanceToWaypoint;

        // v^2 = u^2 + 2as 
        // 0 = u^2 + 2as
        // s = -u^2 / 2a

        float brakingDistance = maxSpeed * maxSpeed / acceleration / 2;

        GameStats.Distance.Value = distanceToWaypoint;
        GameStats.BrakingDistance.Value = brakingDistance;

        // accelerate or brake depending on the distance to the next waypoint

        if (distanceToWaypoint <= brakingDistance) 
        {
            Debug.Log($"distance {distanceToWaypoint} <= {brakingDistance}");

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
            Debug.Log($"distance {distanceToWaypoint} > {brakingDistance}");
            // far from waypoint, speed up
            speed += acceleration * Time.fixedDeltaTime;
            speed = Mathf.Min(speed, maxSpeed);
        }
        Debug.Log($"speed = {speed}");

    }
    
    private void NextWaypoint()
    {
        next = (next + 1) % waypoints.Length;  // wrap to between 0 of #waypoints-1
        // Debug.Break();
    }
#endregion
}

