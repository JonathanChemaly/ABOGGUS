using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FreeLook : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform follow;
    private Transform lookAt;
    void Start()
    {
        follow = GameObject.Find("Player").transform;
        lookAt = GameObject.Find("LookAt").transform;
        GetComponent<CinemachineFreeLook>().Follow = follow;
        GetComponent<CinemachineFreeLook>().LookAt = lookAt;
    }
}
