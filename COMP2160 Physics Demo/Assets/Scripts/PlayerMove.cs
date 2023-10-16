using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
#region Serialized fields
    [SerializeField] private float speed = 2;
#endregion

#region Private fields
    private Actions actions;
    private InputAction moveAction;
#endregion

#region Init
    void Awake()
    {
        actions = new Actions();
        moveAction = actions.movement.move;
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
        float movement = moveAction.ReadValue<float>() * speed * Time.deltaTime;
        transform.Translate(movement * Vector3.right);
    }
#endregion
}
