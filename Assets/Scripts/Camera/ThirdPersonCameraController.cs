using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using ABOGGUS.Menus;
using ABOGGUS.Gameplay;
using ABOGGUS.Interact;
using ABOGGUS.PlayerObjects;

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
    public bool thirdPerson = true;
    private bool oldTP = true;
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
    CinemachineFreeLook freeLookCam;

    private float freeLookCamSpeed;

    public static float cameraYRot;

    private static bool exists = false;
    private void Awake()
    {
        if (!exists)
        {
            DontDestroyOnLoad(gameObject);
        }
        exists = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        tpRotation = Quaternion.Euler(33.5f, 0, 0);
        fpRotation = Quaternion.Euler(1f, 0, 0);
        camOffset = new Vector3(offset * Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180), yOffset, offset * Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180));
        freeLookCam = GameObject.FindGameObjectWithTag("FreeLook").GetComponent<CinemachineFreeLook>();
        freeLookCamSpeed = freeLookCam.m_XAxis.m_MaxSpeed;
    }

    private void LateUpdate()
    {
        Debug.Log("is paused: " + GameController.gameState);
        bool cameraChecks = GameController.gameState != GameConstants.GameState.Paused && !PauseMenu.isPaused 
                && !InventoryMenu.isPaused && !GameOverMenu.isPaused && !OpenShopMenu.shopOpen && !OpenInteractMenu.interactOpen && !OpenSokobanOnInteract.SokobanOpen;
        if (cameraChecks) Cursor.lockState = CursorLockMode.Locked;

        if (GameController.scene == GameConstants.SCENE_DUNGEON1 || GameController.scene == GameConstants.SCENE_DUNGEON2 || GameController.scene == GameConstants.SCENE_DUNGEON3 || GameController.scene == GameConstants.SCENE_BOSS)
        {
            thirdPerson = true;
        } else
        {
            thirdPerson = false;
            Debug.Log("IN LOBBY");
        }
        if (thirdPerson != oldTP)
        {
            CameraPosition();
            oldTP = thirdPerson;
            //Debug.Log("CameraSwitch");
        }
        if (GameController.player != null)
        {
            player = GameController.player.GetGameObject();
        }
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
        else if (lookAround && thirdPerson && cameraChecks)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Vector2 lookVector = look.ReadValue<Vector2>();
            float lRotateSpeed = lookVector.x * Time.deltaTime * rotationSpeed;
            float yRotateSpeed = -lookVector.y * Time.deltaTime * rotationSpeed;

            transform.RotateAround(player.transform.position, new Vector3(0, 1, 0), lRotateSpeed);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);


            camOffset = new Vector3(offset * Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180), yOffset, offset * Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180));
            //Debug.Log("tpc: " + cameraYRot);

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + camOffset, camSpeed);
        }
        else if (lookAround && !thirdPerson && cameraChecks)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            rotateDirection = (Vector3)look.ReadValue<Vector2>() * Time.deltaTime * rotationSpeed;
            rotX += rotateDirection.x;
            rotY += rotateDirection.y;

            if (rotY < -90.0f) rotY = -90.0f;
            if (rotY > 90.0f) rotY = 90.0f;
            rotateCamera = Quaternion.Euler(-rotY, rotX, 0.0f);
            rotateTarget = Quaternion.Euler(0.0f, rotX, 0.0f);
            if (GameController.player.GetController().GetFacingDirection() == PlayerController.FacingDirection.Idle)
                player.transform.localRotation = Quaternion.RotateTowards(player.transform.localRotation, rotateTarget, rotationSpeed);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rotateCamera, rotationSpeed);
            camOffset = new Vector3(offset * Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180), yOffset, offset * Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180));
            cameraYRot = transform.eulerAngles.y;

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + camOffset, camSpeed);
        }
        else
        {
            if (player != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position + camOffset, camSpeed);
            }
        }
        cameraYRot = transform.eulerAngles.y;
        if (PauseMenu.isPaused || InventoryMenu.isPaused || GameOverMenu.isPaused) freeLookCam.m_XAxis.m_MaxSpeed = 0.0f;
        else freeLookCam.m_XAxis.m_MaxSpeed = freeLookCamSpeed;
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
            GetComponent<CinemachineBrain>().enabled = true;
            offset = -2f;
            yOffset = 3.5f;
            camOffset = new Vector3(offset * Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180), yOffset, offset * Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180));
            transform.rotation = tpRotation;
        }
        else
        {
            GetComponent<CinemachineBrain>().enabled = false;
            offset = 1.2f;
            yOffset = 2f;
            camOffset = new Vector3(offset * Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180), yOffset, offset * Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180));
            transform.rotation = fpRotation;
        }
    }

    public void Trigger(InputAction.CallbackContext obj)
    {
       /* thirdPerson = !thirdPerson;
        CameraPosition();*/
    }
}