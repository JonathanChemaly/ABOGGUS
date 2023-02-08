using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateControl : MonoBehaviour
{
    private InputActions playerInputActions;
    private InputAction rotateAction;

    public GameObject targetObject;
    public float speed = 50.0f;
    Quaternion rotateCamera;
    Quaternion rotateTarget;
    private Vector3 rotateDirection;
    private float rotX;
    private float rotY;
    private bool startRotation;

    public void Initialize(InputAction rotateAction)
    {
        playerInputActions = new InputActions();
        this.rotateAction = rotateAction;
        rotateAction.Enable();
        rotX = 0.0f;
        rotY = 0.0f;
        startRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startRotation) StartCoroutine(RotationDelay());
        else
        {
            rotateDirection = (Vector3)rotateAction.ReadValue<Vector2>() * Time.deltaTime * speed;
            rotX += rotateDirection.x;
            rotY += rotateDirection.y;

            if (rotY < -90.0f) rotY = -90.0f;
            if (rotY > 90.0f) rotY = 90.0f;
            rotateCamera = Quaternion.Euler(-rotY, rotX, 0.0f);
            rotateTarget = Quaternion.Euler(0.0f, rotX, 0.0f);

            targetObject.transform.localRotation = Quaternion.RotateTowards(targetObject.transform.localRotation, rotateTarget, speed);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rotateCamera, speed);
        }
    }

    IEnumerator RotationDelay()
    {
        yield return new WaitForSeconds(0.1f);
        startRotation = true;
    }
}
