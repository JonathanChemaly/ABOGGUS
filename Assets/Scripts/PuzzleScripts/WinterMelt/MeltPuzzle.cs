using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltPuzzle : MonoBehaviour
{
    [SerializeField] GameObject meltpuzzle;
    [SerializeField] GameObject player;

    public void Restart()
    {
        Debug.Log("Restart");
        GameObject toDestroy = this.transform.GetChild(2).gameObject;

        Vector3 puzzlePos = toDestroy.transform.position;
        Quaternion rot = toDestroy.transform.rotation;
        Destroy(toDestroy);

        Vector3 playerPos = new Vector3();
        playerPos.x = -199.44f;
        playerPos.y = 32f;
        playerPos.z = -60.63f;

        player.transform.SetPositionAndRotation(playerPos, player.transform.rotation);
        GameObject newPuzzle = Instantiate(meltpuzzle, puzzlePos, rot);
        newPuzzle.transform.parent = gameObject.transform;
    }
}
