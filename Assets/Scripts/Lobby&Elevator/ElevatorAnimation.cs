using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using ABOGGUS.Gameplay;
using ABOGGUS.PlayerObjects;
using UnityEngine.SceneManagement;

public class ElevatorAnimation : MonoBehaviour
{
    private Player player;
    private Transform playerTransform;
    private bool animationState = false;
    public float timer = 16f;
    private float rotation = 180f;
    private float rotationSpeed = 1f;

    private AsyncOperation sceneLoadingOperation;

    // Start is called before the first frame update
    void Start()
    {
        player = GameController.player;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
 
    void FixedUpdate()
    {
        if (animationState)
        {
            timer -= Time.deltaTime;
        }
        if (animationState && timer >= 15f)
        {
            ThirdPersonCameraController.animationState = true;
            ThirdPersonCameraController.adjustCam = true;
            playerTransform.position += new Vector3(0, 0, 0.05f);
        }
        else if (animationState && timer <= 12f && timer >= 8.5f)
        {
            ThirdPersonCameraController.adjustCam = false;
            ThirdPersonCameraController.moveCam = true;
            playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, Quaternion.Euler(0, rotation, 0), rotationSpeed);
        }
        else if (animationState && timer <= 8.5f && timer >= 0f)
        {
            ThirdPersonCameraController.moveCam = false;
        }
        if (animationState && timer <= 0f)
        {
            player.GetGameObject().transform.position += new Vector3(0, 0, -0.06f);
        }
        //ThirdPersonCameraController.cameraYRot = ThirdPersonCameraController.transform.eulerAngles.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.gameState = GameConstants.GameState.Paused;
            GameController.player.debug = false;
            animationState = true;
        }
    }
}
