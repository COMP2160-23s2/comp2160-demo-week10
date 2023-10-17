using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformMove : MonoBehaviour
{
#region Serialized fields
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed = 1;
#endregion

#region Private fields
    private int next = 0;
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
        float move = speed * Time.deltaTime;
        Vector2 dir = (Vector2)waypoints[next].position - rigidbody.position;

        if (move > dir.magnitude)
        {
            // don't overshoot the waypoint
            rigidbody.MovePosition(waypoints[next].position);            
            NextWaypoint();
        }
        else
        {            
            rigidbody.MovePosition(rigidbody.position + move * dir.normalized);
        }

        Debug.Log($"velocity = {rigidbody.velocity}");
    }
    
    private void NextWaypoint()
    {
        next = (next + 1) % waypoints.Length;  // wrap to between 0 of #waypoints-1
    }
#endregion
}

