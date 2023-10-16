using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
#region Serialized fields
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed = 1;
#endregion

#region Private fields
    private int next = 0;
#endregion

#region Init
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
    void Update()
    {
        float move = speed * Time.deltaTime;
        Vector3 dir = waypoints[next].position - transform.position;

        if (move > dir.magnitude)
        {
            // don't overshoot the waypoint
            transform.position = waypoints[next].position;
            NextWaypoint();
        }
        else
        {
            transform.Translate(move * dir.normalized);
        }
    }
    
    private void NextWaypoint()
    {
        next = (next + 1) % waypoints.Length;
    }
#endregion
}

