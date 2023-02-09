using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCameraController : MonoBehaviour
{
    public GameObject player;
    private float yOffset = 20f;
    private float camSpeed = 5f;
    private float rotateSpeed = 1f;
    float x, y;

    private Vector3 tpOffset;
    private Vector3 camOffset;
    private float offset = -15f;
    private InputAction look;
    private bool lookAround = false;

    private float transitionSpeed = 0.04f;
    public Quaternion fpRotation;
    public Quaternion tpRotation;
    public static bool thirdPerson = true;
    private InputAction cameraSwitch;
    private float fov = 90.0f;

    public static bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        tpOffset = transform.position - player.transform.position;
        tpRotation = transform.rotation;
        fpRotation = Quaternion.Euler(1, 0, 0);
        camOffset = tpOffset;
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
            camOffset = new Vector3(offset * Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180), yOffset, offset * Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180));
            Rotator.cameraYRot = transform.eulerAngles.y;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + camOffset, camSpeed);
        } else {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + camOffset, camSpeed);
        }
 
        
        Camera.main.fieldOfView = fov;
    }


    public void Initialize(InputAction look, InputAction cameraSwitch)
    {
        this.look = look;
        this.look.performed += LookAround;
        this.look.canceled += StopLookAround;
        this.look.Enable();

        this.cameraSwitch = cameraSwitch;
        this.cameraSwitch.performed += Trigger;
        this.cameraSwitch.Enable();
    }

    public void LookAround(InputAction.CallbackContext obj)
    {
        lookAround = true;
    }

    public void StopLookAround(InputAction.CallbackContext obj)
    {
        lookAround = false;
    }


    public void Trigger(InputAction.CallbackContext obj) 
    {
        thirdPerson = !thirdPerson;
        if (thirdPerson)
        {
            offset = -15f;
            yOffset = 20f;
            camOffset = new Vector3(offset * Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180), yOffset, offset * Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180));
        }
        else
        {
            offset = 2f;
            yOffset = 14f;
            camOffset = new Vector3(offset * Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180), yOffset, offset * Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180));
            transform.rotation = fpRotation;
        }
    }
    /*
    private void FixedUpdate()
    {
        if (thirdPerson && fov < 90.0f)
        {
            camSpeed = 0.0032f;
            fov += Time.deltaTime * 12.0f;
            isPaused = false;
        }
        else if (!thirdPerson && fov > 70.0f)
        {
            camSpeed = 0.0032f;
            fov -= Time.deltaTime * 12.0f;
            isPaused = false;
        }
        else if (fov <= 70.0f || fov >= 90.0f)
        {
            camSpeed = 0.1f;
            isPaused = true;
        }
    }*/
}