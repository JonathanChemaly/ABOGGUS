using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Gameplay;

public class BotElevatorDoorSokobanTrigger : MonoBehaviour
{
    public TMPro.TextMeshProUGUI NotEnoughMana;
    public TMPro.TextMeshProUGUI GoToBoss;

    public bool openBottomDoor = false;
    public bool flipOrientation = false;

    [SerializeField] private bool transition = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (transform.parent.name != "BossElevator" && transform.parent.name != "SpawnElevator" && UpgradeStats.runs == 0)
        {
            GoToBoss.enabled = true;
        }
        else
        {
            if (transition && other.CompareTag("Player"))
            {
                openBottomDoor = true;
                if (GameController.scene == GameConstants.SCENE_DUNGEON1 && transform.parent.name == "DungeonElevatorToLayer2")
                {
                    GameController.ChangeScene("Elevator to Dungeon Layer 2", GameConstants.SCENE_DUNGEON2, false);
                }
                if (GameController.scene == GameConstants.SCENE_DUNGEON2 && transform.parent.name == "DungeonElevatorToLayer3")
                {
                    GameController.ChangeScene("Elevator to Dungeon Layer 3", GameConstants.SCENE_DUNGEON3, false);
                }
                if (GameController.scene == GameConstants.SCENE_DUNGEON3 && transform.parent.name == "DungeonElevatorToBoss")
                {
                    GameController.ChangeScene("Elevator to Boss", GameConstants.SCENE_BOSS, false);
                }
                if (GameController.scene == GameConstants.SCENE_MAINLOBBY)
                {
                    if (UpgradeStats.runs == 0) GameController.ChangeScene("Elevator to Boss", GameConstants.SCENE_BOSS, false);
                    else GameController.ChangeScene("Elevator to Dungeon Layer 1", GameConstants.SCENE_DUNGEON1, false);
                    UpgradeStats.runs++;
                }
                if (GameController.scene == GameConstants.SCENE_AUTUMNROOM)
                {
                    GameController.ChangeScene("Elevator to Autumn Room", GameConstants.SCENE_MAINLOBBY, false);
                }
                if (GameController.scene == GameConstants.SCENE_SUMMERROOM)
                {
                    GameController.ChangeScene("Elevator to Summer Room", GameConstants.SCENE_MAINLOBBY, false);
                }
                if (GameController.scene == GameConstants.SCENE_WINTERROOM)
                {
                    GameController.ChangeScene("Elevator to Winter Room", GameConstants.SCENE_MAINLOBBY, false);
                }
                if (GameController.scene == GameConstants.SCENE_SPRINGROOM)
                {
                    GameController.ChangeScene("Elevator to Spring Room", GameConstants.SCENE_MAINLOBBY, false);
                }
            }
            else if (other.CompareTag("Player"))
            {
                openBottomDoor = true;
                //Debug.Log("botOpen");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            openBottomDoor = false;
            if (NotEnoughMana != null) NotEnoughMana.enabled = false;
            if (GoToBoss != null) GoToBoss.enabled = false;
        }
    }

}
