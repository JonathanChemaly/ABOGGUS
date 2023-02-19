using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ABOGGUS.Menus;

public class ThirdPersonCameraController : MonoBehaviour
{
    public GameObject player;
    private float yOffset = 2.33f;
    private float camSpeed = 5f;
    private float rotateSpeed = 1f;
    float x, y;

    private Vector3 tpOffset;
    private Vector3 camOffset;
    private float offset = 1f;
    private InputAction look;
    private bool lookAround = false;

    private float transitionSpeed = 0.04f;
    public Quaternion fpRotation;
    public Quaternion tpRotation;
    public bool thirdPerson = false;
    public static bool animationState = false;
    public static bool adjustCam = false;
    public static bool moveCam = false;
    public static bool preAnimCam = false;

    private InputAction cameraSwitch;
    private float fov = 90.0f;

    public static bool isPaused = false;
    [SerializeField] private float rotationSpeed = 20f;
    private Vector3 rotateDirection;
    private float rotX;
    private float rotY;
    Quaternion rotateCamera;
    Quaternion rotateTarget;
    // Start is called before the first frame update
    void Start()
    {
        tpRotation = Quaternion.Euler(33.5f, 0, 0);
        fpRotation = Quaternion.Euler(1f, 0, 0);
        camOffset = new Vector3(offset * Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180), yOffset, offset * Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180));
    }

    private void LateUpdate()
    {
        if (adjustCam)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, tpRotation, 0.03f);
            camSpeed = 0.01f;
        }
        else if (preAnimCam)
        {
            offset = 1f;
            yOffset = 2.33f;
            camOffset = new Vector3(offset * Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180), yOffset, offset * Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, fpRotation, 0.1f);
        }
        else if (moveCam)
        {
            camSpeed = 5f;
            transform.RotateAround(player.transform.position, Vector3.up, 0.1f);
        }

        if (animationState)
        {
            offset = -2f;
            yOffset = 3.5f;
            camOffset = new Vector3(offset * Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180), yOffset, offset * Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180));
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + camOffset, camSpeed);
        }
        else if (lookAround && !PauseMenu.isPaused && !InventoryMenu.isPaused && thirdPerson)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Vector2 lookVector = look.ReadValue<Vector2>();
            float lRotateSpeed = lookVector.x * Time.deltaTime * rotationSpeed;
            float yRotateSpeed = -lookVector.y * Time.deltaTime * rotationSpeed;

            transform.RotateAround(player.transform.position, new Vector3(0, 1, 0), lRotateSpeed);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);


            camOffset = new Vector3(offset * Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180), yOffset, offset * Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180));
            Rotator.cameraYRot = transform.eulerAngles.y;

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + camOffset, camSpeed);
        }
        else if (lookAround && !PauseMenu.isPaused && !InventoryMenu.isPaused && !thirdPerson)
        {
            Cursor.lockState = CursorLockMode.Locked;
            rotateDirection = (Vector3)look.ReadValue<Vector2>() * Time.deltaTime * rotationSpeed;
            rotX += rotateDirection.x;
            rotY += rotateDirection.y;

            if (rotY < -90.0f) rotY = -90.0f;
            if (rotY > 90.0f) rotY = 90.0f;
            rotateCamera = Quaternion.Euler(-rotY, rotX, 0.0f);
            rotateTarget = Quaternion.Euler(0.0f, rotX, 0.0f);

            player.transform.localRotation = Quaternion.RotateTowards(player.transform.localRotation, rotateTarget, rotationSpeed);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rotateCamera, rotationSpeed);
            camOffset = new Vector3(offset * Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180), yOffset, offset * Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180));
            Rotator.cameraYRot = transform.eulerAngles.y;

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + camOffset, camSpeed);
        }
        else
        {
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

    public void startAnimation()
    {
        animationState = !animationState;
    }
    public static void globalCameraSwitch()
    {
        
    }
    public void CameraPosition()
    {
        if (thirdPerson)
        {
            offset = -2f;
            yOffset = 3.5f;
            camOffset = new Vector3(offset * Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180), yOffset, offset * Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180));
            transform.rotation = tpRotation;
        }
        else
        {
            offset = 1f;
            yOffset = 2.33f;
            camOffset = new Vector3(offset * Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180), yOffset, offset * Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180));
            transform.rotation = fpRotation;
        }
    }


    public void Trigger(InputAction.CallbackContext obj) 
    {
        thirdPerson = !thirdPerson;
        CameraPosition();
    }
}