using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Gameplay;

public class BotElevatorDoorTrigger : MonoBehaviour
{
    private Vector3 botDoorRP;
    private Vector3 botDoorLP;
    private Vector3 botDoorRPO;
    private Vector3 botDoorLPO;

    private float doorSpeed = 0.02f;

    public GameObject botDoorR;
    public GameObject botDoorL;

    public TMPro.TextMeshProUGUI NotEnoughMana;
    public TMPro.TextMeshProUGUI GoToBoss;

    public bool openBottomDoor = false;
    public bool flipOrientation = false;

    [SerializeField] private bool transition = false;
    void Start()
    {
        botDoorRP = botDoorR.transform.position;
        botDoorLP = botDoorL.transform.position;
        if (flipOrientation)
        {
            botDoorRPO = botDoorRP -new Vector3(10.4f, 0.0f, 0.0f);
            botDoorLPO = botDoorLP + new Vector3(10.4f, 0.0f, 0.0f);
        }
        else
        {
            botDoorRPO = botDoorRP + new Vector3(10.4f, 0.0f, 0.0f);
            botDoorLPO = botDoorLP - new Vector3(10.4f, 0.0f, 0.0f);
        }
        NotEnoughMana.enabled = false;
        GoToBoss.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (openBottomDoor)
        {
            botDoorR.transform.position = Vector3.MoveTowards(botDoorR.transform.position, botDoorRPO, doorSpeed);
            botDoorL.transform.position = Vector3.MoveTowards(botDoorL.transform.position, botDoorLPO, doorSpeed);
        }
        else
        {
            botDoorR.transform.position = Vector3.MoveTowards(botDoorR.transform.position, botDoorRP, doorSpeed);
            botDoorL.transform.position = Vector3.MoveTowards(botDoorL.transform.position, botDoorLP, doorSpeed);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (transform.parent.name != "BossElevator" && UpgradeStats.runs == 0)
        {
            GoToBoss.enabled = true;
        }
        else if (transform.parent.name == "SummerElevator" && UpgradeStats.totalMana < 150) {
            NotEnoughMana.enabled = true;
        }
        else if (transform.parent.name == "SpringElevator" && UpgradeStats.totalMana < 200) {
            NotEnoughMana.enabled = true;
        }
        else if (transform.parent.name == "WinterElevator" && UpgradeStats.totalMana < 250) {
            NotEnoughMana.enabled = true;
        }
        else
        {
            if (transition && other.CompareTag("Player"))
            {
                openBottomDoor = true;
                if (GameController.scene == GameConstants.SCENE_DUNGEON)
                {
                    GameController.ChangeScene("Elevator to Boss", GameConstants.SCENE_BOSS, false);
                }
                if (GameController.scene == GameConstants.SCENE_MAINLOBBY)
                {
                    if (UpgradeStats.runs == 0) GameController.ChangeScene("Elevator to Boss", GameConstants.SCENE_BOSS, false);
                    else GameController.ChangeScene("Elevator to Boss", GameConstants.SCENE_DUNGEON, false);
                    BuffManager.Instance.StoreStatsBeforeRun();
                    UpgradeStats.runs++;
                }
                if (GameController.scene == GameConstants.SCENE_AUTUMNROOM)
                {
                    GameController.ChangeScene("Elevator to Boss", GameConstants.SCENE_MAINLOBBY, false);
                }
                if (GameController.scene == GameConstants.SCENE_SUMMERROOM)
                {
                    GameController.ChangeScene("Elevator to Boss", GameConstants.SCENE_MAINLOBBY, false);
                }
                if (GameController.scene == GameConstants.SCENE_WINTERROOM)
                {
                    GameController.ChangeScene("Elevator to Boss", GameConstants.SCENE_MAINLOBBY, false);
                }
            }
            else if (other.CompareTag("Player"))
            {
                openBottomDoor = true;
                Debug.Log("botOpen");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            openBottomDoor = false;
            NotEnoughMana.enabled = false;
            GoToBoss.enabled = false;
        }
    }

}
