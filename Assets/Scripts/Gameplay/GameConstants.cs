using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngineInternal;

namespace ABOGGUS.Gameplay
{
    public static class GameConstants
    {
        public enum GameState
        {
            StartMenu,
            InGame,
            Paused,
            EndGame
        }

        public const string SCENE_BOSS = "Boss";
        public const string SCENE_CREDITS = "Credits";
        public const string SCENE_ELEVATOR = "Elevator";
        public const string SCENE_LOADING = "LoadingScreen";
        public const string SCENE_MAINLOBBY = "LobbyScene";
        public const string SCENE_MAINMENU = "Menu";
        public const string SCENE_MINIGAME = "MiniGame";
        public const string SCENE_PLAYERDEMO = "PlayerDemo";
        public const string SCENE_DUNGEONTEST = "DungeonTest";
        public const string SCENE_AUTUMNROOM = "AutumnRoom";
        public const string SCENE_SUMMERROOM = "SummerRoom";
        public const string SCENE_WINTERROOM = "WinterRoom";
        public const string SCENE_SPRINGROOM = "SpringRoom";
        public const string SCENE_DUNGEON1 = "DungeonLayer1";
        public const string SCENE_DUNGEON2 = "DungeonLayer2";
        public const string SCENE_DUNGEON3 = "DungeonLayer3";
        public const string SCENE_TEST = "TestPuzzle";

        public const string NAME_PLAYERGAMEOBJECT = "Player";

        public static List<string> SCENES_INGAME = new List<string>() { SCENE_BOSS, SCENE_DUNGEON1, SCENE_DUNGEON2, SCENE_DUNGEON3, SCENE_ELEVATOR, SCENE_MAINLOBBY, SCENE_DUNGEONTEST, SCENE_AUTUMNROOM, SCENE_SPRINGROOM, SCENE_WINTERROOM, SCENE_SUMMERROOM, SCENE_TEST };
        public static Dictionary<string, bool> puzzleStatus= new Dictionary<string, bool>() { 
            { "FirePuzzle", false }, 
            { "WindPushPuzzle", false }, 
            { "TileSlidePuzzle", false }, 
            { "TractorPuzzle", false }, 
            { "MazePuzzle", false }, 
            { "MeltIcePuzzle", false }, 
            { "FallingIcePuzzle", false },
            { "RunePuzzle", false },
            { "TreeGrowPuzzle", false }
        };
        /*foreach (KeyValuePair<string, bool> entry in puzzles)
        {
            if (entry.Value)
            {
                Debug.Log(entry.Key + ": Complete");
            }
            else
            {
                Debug.Log(entry.Key + ": Incomplete");
            }
        }*/
    
    }
}
