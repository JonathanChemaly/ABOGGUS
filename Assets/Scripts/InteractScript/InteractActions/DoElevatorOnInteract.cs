using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact
{
    public class DoElevatorOnInteract : MonoBehaviour
    {
        private Vector3 topDoorRP;
        private Vector3 topDoorLP;
        private Vector3 topDoorRPO;
        private Vector3 topDoorLPO;

        private float doorSpeed = 0.02f;
        private float timer = 2.0f;

        public GameObject topDoorR;
        public GameObject topDoorL;

        public bool openTopDoor = false;

        [SerializeField]
        [Tooltip("interact to watch for success interact the other")]
        private Interactable interactToWatch;

        [SerializeField]
        [Tooltip("whether to flip direction or not")]
        private bool flipOrientation = false;

        [SerializeField]
        [Tooltip("If we are moving bottom or top door")]
        private bool isTopDoor = true;

        void Start()
        {
            topDoorRP = topDoorR.transform.position;
            topDoorLP = topDoorL.transform.position;

            if(!isTopDoor) //bottom door
            {
                if (flipOrientation)
                {
                    topDoorRPO = topDoorRP - new Vector3(1.7f, 0.0f, 0.0f);
                    topDoorLPO = topDoorLP + new Vector3(1.7f, 0.0f, 0.0f);

                    //topDoorRPO = topDoorRP - new Vector3(10.4f, 0.0f, 0.0f);
                    //topDoorLPO = topDoorLP + new Vector3(10.4f, 0.0f, 0.0f);
                }
                else
                {
                    topDoorRPO = topDoorRP + new Vector3(1.7f, 0.0f, 0.0f);
                    topDoorLPO = topDoorLP - new Vector3(1.7f, 0.0f, 0.0f);

                    //topDoorRPO = topDoorRP + new Vector3(10.4f, 0.0f, 0.0f);
                    //topDoorLPO = topDoorLP - new Vector3(10.4f, 0.0f, 0.0f);
                }
            } 
            else
            {
                topDoorRPO = topDoorRP + new Vector3(1.7f, 0.0f, 0.0f);
                topDoorLPO = topDoorLP - new Vector3(1.7f, 0.0f, 0.0f);
            }

            

            interactToWatch.InteractAction += OpenDoor;
        }

        private void OpenDoor()
        {
            timer = 2.0f;
            openTopDoor = true;
            //Debug.Log("Opening Elevator door");
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (openTopDoor)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    //Debug.Log("elevator moving");
                    topDoorR.transform.position = Vector3.MoveTowards(topDoorR.transform.position, topDoorRPO, doorSpeed);
                    topDoorL.transform.position = Vector3.MoveTowards(topDoorL.transform.position, topDoorLPO, doorSpeed);

                    if (topDoorL.transform.position == topDoorLPO)
                    {
                        StartCoroutine(CloseDoorAfterTime());
                    }
                }
            }
            else
            {
                topDoorR.transform.position = Vector3.MoveTowards(topDoorR.transform.position, topDoorRP, doorSpeed);
                topDoorL.transform.position = Vector3.MoveTowards(topDoorL.transform.position, topDoorLP, doorSpeed);
            }
        }
        
        IEnumerator CloseDoorAfterTime(){
            yield return new WaitForSeconds(2.0f);
            openTopDoor = false;
        }
    }
}

