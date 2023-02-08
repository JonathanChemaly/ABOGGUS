using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementControl : MonoBehaviour
{
    private InputAction moveAction;
    public float speed = 2.0f;
    private float fixedSpeed;
    [SerializeField] private Camera targetCamera;
    public void Initialize(InputAction moveAction)
    {
        this.moveAction = moveAction;
        this.moveAction.Enable();
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = (Vector3)moveAction.ReadValue<Vector2>();
        moveDirection.z = moveDirection.y;
        moveDirection.y = 0;
        fixedSpeed = speed * Time.deltaTime;
        transform.localPosition += transform.TransformVector(moveDirection * fixedSpeed);
        targetCamera.transform.localPosition = Vector3.MoveTowards(targetCamera.transform.localPosition, targetCamera.transform.localPosition, fixedSpeed);
    }
}
