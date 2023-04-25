using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using ABOGGUS.PlayerObjects;
using ABOGGUS.Interact.Puzzles;

namespace ABOGGUS.SaveSystem
{
    public static class SaveGameManager
    {
        public static SaveData currentSaveData = new SaveData();    // save data currently used in game

        public const string saveFolder = "/Saves/";
        public const string defaultFileName = "DefaultSave.sav";  // currently only one save file at a time
        
        public static bool SaveDataToFile(string fileName)
        {
            //var dir = Application.persistentDataPath + saveFolder;  // find default directory to store our save

            var dir = Application.dataPath + saveFolder;

            // create "Saves" directory if it doesn't exist yet
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            // use default savefile if none given
            if (fileName is null) fileName = defaultFileName;

            // convert SaveData object to JSON, then write to save file
            string json = JsonUtility.ToJson(currentSaveData, true);
            File.WriteAllText(dir + fileName, json);

            //GUIUtility.systemCopyBuffer = dir;  // copies path to clipboard

            return true;

        }

        public static void LoadDataFromFile(string fileName)
        {
            //string path = Application.persistentDataPath + saveFolder + fileName;

            // use default savefile if none given
            if (fileName is null) fileName = defaultFileName;

            string pathToFile = Application.dataPath + saveFolder + fileName;

            // if save path exists, read and convert all JSON data to SaveData object
            SaveData temp = new SaveData();
            if (File.Exists(pathToFile))
            {
                string json = File.ReadAllText(pathToFile);
                temp = JsonUtility.FromJson<SaveData>(json);
            }
            else
            {
                Debug.Log("Save file does not exist.");
            }

            // assign loaded data
            currentSaveData = temp;
        }

        public static void StartNewData()
        {
            UpgradeStats.resetPlayerStats();
            WeaponDamageStats.resetDamageStats();
            currentSaveData = new SaveData();
        }

        public static void SaveProgressToFile(string fileName, Player player, string sceneName)
        {
            SaveScene(sceneName);
            SavePlayerProgress(player);
            SaveDataToFile(fileName);
        }

        public static void LoadProgressFromFile(string fileName)
        {
            LoadDataFromFile(fileName);
        }

        public static void SaveScene(string sceneName)
        {
            currentSaveData.lastScene = sceneName;
        }

        public static void SavePlayerProgress(Player player)
        {
            currentSaveData.playerHealth = player.inventory.health;
            currentSaveData.playerHasKey = player.inventory.key;
            currentSaveData.playerMana = player.inventory.mana;
            Debug.Log("Saved player with health: " + currentSaveData.playerHealth);
        }

        public static void LoadPlayerProgress(Player player)
        {
            player.inventory.health = currentSaveData.playerHealth;
            player.inventory.key = currentSaveData.playerHasKey;
            player.inventory.mana = currentSaveData.playerMana;
            Debug.Log("Loaded player with health: " + currentSaveData.playerHealth);
        }

        public static void SaveSpringPuzzleStatus(List<bool> activeRunesList, TreePuzzle.Status tStatus, int runNum)
        {
            currentSaveData.treeStatus = tStatus;
            currentSaveData.treeRunNumber = runNum;

            currentSaveData.activeRunesList = activeRunesList;
        }

        public static void LoadSpringPuzzleStatus(out List<bool> activeRunesList, out TreePuzzle.Status tStatus, out int runNum)
        {
            tStatus = currentSaveData.treeStatus;
            runNum = currentSaveData.treeRunNumber;

            activeRunesList = currentSaveData.activeRunesList;
        }
    }
}
