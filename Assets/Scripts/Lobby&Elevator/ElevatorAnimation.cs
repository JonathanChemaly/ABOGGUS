using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using ABOGGUS.Gameplay;
using ABOGGUS.PlayerObjects;
using UnityEngine.SceneManagement;

public class ElevatorAnimation : MonoBehaviour
{
    private Player player;
    private bool animationState = false;
    public float timer = 16.5f;
    public float rotation = 180f;
    public float rotationSpeed = 0.6f;

    private AsyncOperation sceneLoadingOperation;

    // Start is called before the first frame update
    void Start()
    {
        player = GameController.player;
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
            player.GetGameObject().transform.position += new Vector3(0, 0, 0.06f);
        }
        else if (animationState && timer <= 12f && timer >= 8.5f)
        {
            ThirdPersonCameraController.adjustCam = false;
            ThirdPersonCameraController.moveCam = true;
            player.GetGameObject().transform.rotation = Quaternion.RotateTowards(player.transform.rotation, Quaternion.Euler(0, rotation, 0), rotationSpeed);
        }
        else if (animationState && timer <= 8.5f && timer >= 0f)
        {
            ThirdPersonCameraController.moveCam = false;
        }
        if (animationState && timer <= 0f)
        {
            player.GetGameObject().transform.position += new Vector3(0, 0, -0.06f);
        }
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
