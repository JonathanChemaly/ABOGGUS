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
        public const string SCENE_AUTUMNROOM = "Autumn Room";
        public const string SCENE_DUNGEON = "Dungeon";

        public const string NAME_PLAYERGAMEOBJECT = "Player";

        public static List<string> SCENES_INGAME = new List<string>() { SCENE_BOSS, SCENE_ELEVATOR, SCENE_MAINLOBBY, SCENE_DUNGEONTEST };
    }
}
