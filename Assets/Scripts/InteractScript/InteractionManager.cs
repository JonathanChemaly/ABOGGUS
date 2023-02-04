using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("What camera to see what you are interacting with")]
    Camera FPScam;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RayCastCheck();
    }

    private void RayCastCheck()
    {
        Ray lookAtRay = FPScam.ViewportPointToRay(Vector3.one/2f);
        Debug.DrawRay(lookAtRay.origin, lookAtRay.direction * 3f);
    }
}
