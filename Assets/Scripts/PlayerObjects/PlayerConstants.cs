using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABOGGUS.PlayerObjects
{
    public static class PlayerConstants
    {
        public const string GAMEOBJECT_PLAYERNAME = "Player";

        public const int FIXED_UPDATES_PER_SECOND = 50;

        public const float MAX_HEALTH = 100;
        public const float SPEED_DEFAULT = 0.1f;
        public const float SPRINT_MULTIPLIER_DEFAULT = 2;
        public const float JUMP_HEIGHT_DEFAULT = 0.8f;
        public const float JUMP_TIME_DEFAULT = 0.8f;
        public const float DODGE_LENGTH_DEFAULT = 1f;
        public const float DODGE_TIME_DEFAULT = 1.2f;
        public const float JUMP_TIME_CUMULATIVE_DEFAULT = 0;
        public const float DODGE_TIME_CUMULATIVE_DEFAULT = 0;
        public const float WIND_AOE_ATTACK_MAXRANGE = 10f;
        public enum Magic { Wind, Fire, Water, Nature };
        public enum Weapon { Sword, Grimoire, Unarmed };

        public const float INVULNERABILITY_FRAMES = 2 * FIXED_UPDATES_PER_SECOND;
    }
}