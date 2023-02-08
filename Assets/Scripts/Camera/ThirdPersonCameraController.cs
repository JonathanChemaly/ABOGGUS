using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCameraController : MonoBehaviour
{
    public GameObject player;
    private float camSpeed = 5f;
    private float rotateSpeed = 1f;
    float x, y;

    private Vector3 tpOffset;
    private float offset = -2f;
    private InputAction look;
    private bool lookAround = false;

    // Start is called before the first frame update
    void Start()
    {
        tpOffset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        if (lookAround)
        {
            Vector2 lookVector = look.ReadValue<Vector2>();
            float lRotateSpeed = rotateSpeed;
            if (lookVector.x < 0)
            {
                lRotateSpeed = -lRotateSpeed;
            }
            transform.RotateAround(player.transform.position, new Vector3(0, 1, 0), lRotateSpeed);
            tpOffset = new Vector3(offset * Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180), 4, offset * Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180));
            Rotator.cameraYRot = transform.eulerAngles.y;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + tpOffset, camSpeed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + tpOffset, camSpeed);
        }
    }
    public void Initialize(InputAction look)
    {
        this.look = look;
        this.look.performed += LookAround;
        this.look.canceled += StopLookAround;
        this.look.Enable();
    }

    public void LookAround(InputAction.CallbackContext obj)
    {
        lookAround = true;
    }

    public void StopLookAround(InputAction.CallbackContext obj)
    {
        lookAround = false;
    }
}
