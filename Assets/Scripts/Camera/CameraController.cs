using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ABOGGUS
{
    public class CameraController : MonoBehaviour
    {

        public GameObject player;
        private float camSpeed = 0.0032f;
        private float rotateSpeed = 0.04f;
        private float playerHeight = 2.2f;
        private float fov = 90.0f;

        private Vector3 tpOffset;
        public Quaternion fpRotation;
        public Quaternion tpRotation;
        public static bool thirdPerson = true;
        private InputAction cameraSwitch;

        // Start is called before the first frame update
        void Start()
        {
            tpOffset = transform.position - player.transform.position;
            tpRotation = transform.rotation;
            fpRotation = Quaternion.Euler(1, 0, 0);
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (thirdPerson)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position + tpOffset, camSpeed);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, tpRotation, rotateSpeed);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(0.0f, playerHeight, 1f), camSpeed);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, fpRotation, rotateSpeed);
            }
            Camera.main.fieldOfView = fov;


        }

        private void FixedUpdate()
        {
            if (thirdPerson && fov < 90.0f)
            {
                camSpeed = 0.0032f;
                fov += Time.deltaTime * 12.0f;
            }
            else if (!thirdPerson && fov > 70.0f)
            {
                camSpeed = 0.0032f;
                fov -= Time.deltaTime * 12.0f;
            }
            else if (fov <= 70.0f || fov >= 90.0f)
            {
                camSpeed = 0.1f;
            }
        }
        public void Initialize(InputAction cameraSwitch)
        {
            this.cameraSwitch = cameraSwitch;

            this.cameraSwitch.performed += Trigger;
            this.cameraSwitch.Enable();

        }
        public void Trigger(InputAction.CallbackContext obj) 
        {
            thirdPerson = !thirdPerson;
        }
    }
}
