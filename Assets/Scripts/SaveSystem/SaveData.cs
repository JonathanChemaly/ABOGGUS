using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ABOGGUS.Gameplay;
using ABOGGUS.PlayerObjects;
using ABOGGUS.PlayerObjects.Items;
using ABOGGUS.Interact.Puzzles;

namespace ABOGGUS.SaveSystem
{
    [System.Serializable]
    public class SaveData
    {
        // scene data
        public string lastScene = GameConstants.SCENE_MAINLOBBY;

        // player inventory data
        public float playerHealth = PlayerConstants.MAX_HEALTH;
        public bool playerHasKey = false;
        public int playerMana = 0;
        public List<IItem> playerItems = new();

        // spring puzzle data
        public TreePuzzle.Status treeStatus = TreePuzzle.Status.DIRT;
        public int treeRunNumber = 0;
        public List<bool> activeRunesList = new();

        // game constants
        public bool firePuzzleComplete = false;
        public bool windPushPuzzleComplete = false;
        public bool tileSlidePuzzleComplete = false;
        public bool tractorPuzzleComplete = false;
        public bool mazePuzzleComplete = false;
        public bool meltIcePuzzleComplete = false;
        public bool fallingIcePuzzleComplete = false;
        public bool runePuzzleComplete = false;
        public bool treeGrowPuzzleComplete = false;
        public bool windAOEUnlocked = false;
        public bool natureAOEUnlocked = false;
        public bool waterAOEUnlocked = false;
        public bool fireAOEUnlocked = false;
        public bool windUnlocked = true;
        public bool natureUnlocked = false;
        public bool waterUnlocked = false;
        public bool fireUnlocked = false;

        // upgrade stats
        public int runs = 0;
        public int healthUpgradeCount = 0;
        public int swordUpgradeCount = 0;
        public float healthBarSize = UpgradeStats.defaultHealthBarSize;
        public int mana = UpgradeStats.defaultMana;
        public int totalMana = UpgradeStats.defaultMana;
        public float manaEfficiency = 1.0f;
        public int oSDUpgradeCount = 0;
        public int overallSpellDamBonus = 0;
        public int hFMUpgradeCount = 0;
        public bool canHealFromMana = false;
        public float healFromManaVal = 0.0f;
        public float bonusDamMultiplier = 1.0f;
        public int bonusDamUpgradeCount = 0;
        public bool canDealBonusDamAtMaxHealth = false;
    }
}