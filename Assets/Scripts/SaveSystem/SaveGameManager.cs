using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using ABOGGUS.PlayerObjects;
using ABOGGUS.Interact.Puzzles;
using ABOGGUS.Gameplay;

namespace ABOGGUS.SaveSystem
{
    public static class SaveGameManager
    {
        public static SaveData currentSaveData = new SaveData();    // save data currently used in game

        public const string saveFolder = "/Saves/";
        public const string defaultFileName = "DefaultSave.sav";  // currently only one save file at a time

        public static bool finishedLoadingPlayer = false;

        private static bool debug = false;
        
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
                //Debug.Log("Save file does not exist.");
            }

            // assign loaded data
            currentSaveData = temp;
        }

        public static void StartNewData()
        {
            UpgradeStats.ResetPlayerStats();
            WeaponDamageStats.ResetDamageStats();
            currentSaveData = new SaveData();
            finishedLoadingPlayer = true;
        }

        public static void SaveProgressToFile(string fileName, Player player, string sceneName)
        {
            SaveScene(sceneName);
            SavePlayerProgress(player);
            SaveGameConstants();
            SaveUpgradeStats();
            SaveDataToFile(fileName);
        }

        public static void LoadProgressFromFile(string fileName)
        {
            LoadDataFromFile(fileName);
            LoadGameConstants();
            LoadUpgradeStats();
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
            currentSaveData.playerItems = player.inventory.GetItemsString();
            //Debug.Log("Saved player with health: " + currentSaveData.playerHealth);
        }

        public static void LoadPlayerProgress(Player player)
        {
            if (!finishedLoadingPlayer)
            {
                player.inventory.health = currentSaveData.playerHealth;
                player.inventory.key = currentSaveData.playerHasKey;
                player.inventory.mana = currentSaveData.playerMana;
                player.inventory.SetItemsString(currentSaveData.playerItems);
                //Debug.Log("Loaded player with health: " + currentSaveData.playerHealth);
                finishedLoadingPlayer = true;
            }
        }

        // debug

        public static void SaveDebug()
        {
            if (debug)
            {
                SaveDataToFile(null);
            }
        }
        public static void LoadDebug()
        {
            if (debug)
            {
                LoadDataFromFile(null);
                while (GameController.player is null) ;
                LoadPlayerProgress(GameController.player);
            }
        }

        // Puzzle rooms

        public static void SaveAutumnPuzzleStatus(int tractorState)
        {
            currentSaveData.tractorState = tractorState;
        }
        public static void LoadAutumnPuzzleStatus(out int tractorState)
        {
            tractorState = currentSaveData.tractorState;
        }

        public static void SaveSummerPuzzleStatus(int tileSlideDifficulty)
        {
            currentSaveData.tileSlideDifficulty = tileSlideDifficulty;
        }
        public static void LoadSummerPuzzleStatus(out int tileSlideDifficulty)
        {
            tileSlideDifficulty = currentSaveData.tileSlideDifficulty;
        }

        public static void SaveLobbyPuzzleStatus(Player player)
        {
            SavePlayerProgress(player);
            SaveGameConstants();
        }

        public static void LoadLobbyPuzzleStatus(Player player, out bool grimAquired, out bool lockUnlocked)
        {
            //LoadPlayerProgress(player);
            grimAquired = currentSaveData.playerItems.Contains("Grimoire");
            lockUnlocked = currentSaveData.introPuzzleComplete || GameConstants.puzzleStatus["IntroPuzzle"];
        }

        public static void SaveWinterPuzzleStatus()
        {
            // do we need these?
        }
        public static void LoadWinterPuzzleStatus()
        {
            // do we need these?
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

        // game stats and upgrades and misc stuff
        public static void SaveGameConstants()
        {
            currentSaveData.firePuzzleComplete = GameConstants.puzzleStatus["FirePuzzle"];
            currentSaveData.windPushPuzzleComplete = GameConstants.puzzleStatus["WindPushPuzzle"];
            currentSaveData.tileSlidePuzzleComplete = GameConstants.puzzleStatus["TileSlidePuzzle"];
            currentSaveData.tractorPuzzleComplete = GameConstants.puzzleStatus["TractorPuzzle"];
            currentSaveData.mazePuzzleComplete = GameConstants.puzzleStatus["MazePuzzle"];
            currentSaveData.meltIcePuzzleComplete = GameConstants.puzzleStatus["MeltIcePuzzle"];
            currentSaveData.fallingIcePuzzleComplete = GameConstants.puzzleStatus["FallingIcePuzzle"];
            currentSaveData.runePuzzleComplete = GameConstants.puzzleStatus["RunePuzzle"];
            currentSaveData.treeGrowPuzzleComplete = GameConstants.puzzleStatus["TreeGrowPuzzle"];
            currentSaveData.introPuzzleComplete = GameConstants.puzzleStatus["IntroPuzzle"];

            currentSaveData.windAOEUnlocked = GameConstants.windAOEUnlocked;
            currentSaveData.natureAOEUnlocked = GameConstants.natureAOEUnlocked;
            currentSaveData.waterAOEUnlocked = GameConstants.waterAOEUnlocked;
            currentSaveData.fireAOEUnlocked = GameConstants.fireAOEUnlocked;
            currentSaveData.windUnlocked = GameConstants.windUnlocked;
            currentSaveData.natureUnlocked = GameConstants.natureUnlocked;
            currentSaveData.waterUnlocked = GameConstants.waterUnlocked;
            currentSaveData.fireUnlocked = GameConstants.fireUnlocked;
        }
        public static void LoadGameConstants()
        {
            GameConstants.puzzleStatus["FirePuzzle"] = currentSaveData.firePuzzleComplete;
            GameConstants.puzzleStatus["WindPushPuzzle"] = currentSaveData.windPushPuzzleComplete;
            GameConstants.puzzleStatus["TileSlidePuzzle"] = currentSaveData.tileSlidePuzzleComplete;
            GameConstants.puzzleStatus["TractorPuzzle"] = currentSaveData.tractorPuzzleComplete;
            GameConstants.puzzleStatus["MazePuzzle"] = currentSaveData.mazePuzzleComplete;
            GameConstants.puzzleStatus["MeltIcePuzzle"] = currentSaveData.meltIcePuzzleComplete;
            GameConstants.puzzleStatus["FallingIcePuzzle"] = currentSaveData.fallingIcePuzzleComplete;
            GameConstants.puzzleStatus["RunePuzzle"] = currentSaveData.runePuzzleComplete;
            GameConstants.puzzleStatus["TreeGrowPuzzle"] = currentSaveData.treeGrowPuzzleComplete;
            GameConstants.puzzleStatus["IntroPuzzle"] = currentSaveData.introPuzzleComplete;

            GameConstants.windAOEUnlocked = currentSaveData.windAOEUnlocked;
            GameConstants.natureAOEUnlocked = currentSaveData.natureAOEUnlocked;
            GameConstants.waterAOEUnlocked = currentSaveData.waterAOEUnlocked;
            GameConstants.fireAOEUnlocked = currentSaveData.fireAOEUnlocked;
            GameConstants.windUnlocked = currentSaveData.windUnlocked;
            GameConstants.natureUnlocked = currentSaveData.natureUnlocked;
            GameConstants.waterUnlocked = currentSaveData.waterUnlocked;
            GameConstants.fireUnlocked = currentSaveData.fireUnlocked;
        }
        public static void SaveUpgradeStats()
        {
            currentSaveData.runs = UpgradeStats.runs;
            currentSaveData.healthUpgradeCount = UpgradeStats.healthUpgradeCount;
            currentSaveData.swordUpgradeCount = UpgradeStats.swordUpgradeCount;
            currentSaveData.healthBarSize = UpgradeStats.healthBarSize;
            currentSaveData.mana = UpgradeStats.mana;
            currentSaveData.totalMana = UpgradeStats.totalMana;
            currentSaveData.manaEfficiency = UpgradeStats.manaEfficiency;
            currentSaveData.oSDUpgradeCount = UpgradeStats.oSDUpgradeCount;
            currentSaveData.overallSpellDamBonus = UpgradeStats.overallSpellDamBonus;
            currentSaveData.hFMUpgradeCount = UpgradeStats.hFMUpgradeCount;
            currentSaveData.canHealFromMana = UpgradeStats.canHealFromMana;
            currentSaveData.healFromManaVal = UpgradeStats.healFromManaVal;
            currentSaveData.bonusDamMultiplier = UpgradeStats.bonusDamMultiplier;
            currentSaveData.bonusDamUpgradeCount = UpgradeStats.bonusDamUpgradeCount;
            currentSaveData.canDealBonusDamAtMaxHealth = UpgradeStats.canDealBonusDamAtMaxHealth;
        }
        public static void LoadUpgradeStats()
        {
            UpgradeStats.runs = currentSaveData.runs;
            UpgradeStats.healthUpgradeCount = currentSaveData.healthUpgradeCount;
            UpgradeStats.swordUpgradeCount = currentSaveData.swordUpgradeCount;
            UpgradeStats.healthBarSize = currentSaveData.healthBarSize;
            UpgradeStats.mana = currentSaveData.mana;
            UpgradeStats.totalMana = currentSaveData.totalMana;
            UpgradeStats.manaEfficiency = currentSaveData.manaEfficiency;
            UpgradeStats.oSDUpgradeCount = currentSaveData.oSDUpgradeCount;
            UpgradeStats.overallSpellDamBonus = currentSaveData.overallSpellDamBonus;
            UpgradeStats.hFMUpgradeCount = currentSaveData.hFMUpgradeCount;
            UpgradeStats.canHealFromMana = currentSaveData.canHealFromMana;
            UpgradeStats.healFromManaVal = currentSaveData.healFromManaVal;
            UpgradeStats.bonusDamMultiplier = currentSaveData.bonusDamMultiplier;
            UpgradeStats.bonusDamUpgradeCount = currentSaveData.bonusDamUpgradeCount;
            UpgradeStats.canDealBonusDamAtMaxHealth = currentSaveData.canDealBonusDamAtMaxHealth;
        }

        

    }
}
