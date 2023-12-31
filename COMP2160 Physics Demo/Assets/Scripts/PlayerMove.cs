using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
#region Serialized fields
    [SerializeField] private float speed = 2;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;
#endregion

#region Private fields
    private Actions actions;
    private InputAction moveAction;
    private Rigidbody2D rigidbody;
    private float movement;
    private Rigidbody2D ground = null;
#endregion

#region Init
    void Awake()
    {
        actions = new Actions();
        moveAction = actions.movement.move;

        rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        actions.movement.Enable();
    }

    void OnDisable()
    {
        actions.movement.Disable();
    }

    void Start()
    {
        
    }
#endregion

#region Update
    void Update()
    {
        movement = moveAction.ReadValue<float>() * speed;        
    }

    void FixedUpdate()
    {
        CheckOnGround();

        Vector3 v = rigidbody.velocity;
        v.x = movement; // (m, y)

        if (ground != null)
        {   
            // we're standing on a moving platform
            // add its x velocity to our own
            v.x += ground.velocity.x;
        }
        rigidbody.velocity = v;
    }

    private void CheckOnGround()
    {
        // cast a ray downwards a short distance to see whether it hits the ground
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position, Vector2.down, groundCheckDistance, groundLayer);

        if (hit.collider != null)
        {
            // we hit something, record its rigidbody (if any)
            ground = hit.rigidbody;
        }
    }

#endregion

#region Gizmos
    private void OnDrawGizmos()
    {
        // draw the ray for the CheckOnGround raycast
        // color it red if it hit something with a rigidbody, or green otherwise
        Gizmos.color = (ground == null ? Color.green : Color.red);
        Gizmos.DrawRay(transform.position, Vector2.down * groundCheckDistance);
    }
#endregion

}
