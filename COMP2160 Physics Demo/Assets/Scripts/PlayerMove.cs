using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
#region Serialized fields
    [SerializeField] private float speed = 2;
#endregion

#region Private fields
    private Actions actions;
    private InputAction moveAction;
    private Rigidbody2D rigidbody;
    private float movement;
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
        Vector3 v = rigidbody.velocity;
        v.x = movement; // (m, y)
        rigidbody.velocity = v;
    }
#endregion
}
