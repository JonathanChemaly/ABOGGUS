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

        public const float MAX_HEALTH = 1000;
        public const float SPEED_DEFAULT = 0.1f;
        public const float SPRINT_MULTIPLIER_DEFAULT = 2;
        public const float JUMP_HEIGHT_DEFAULT = 3.0f;
        public const float JUMP_TIME_DEFAULT = 0.3f;
        public const float DODGE_LENGTH_DEFAULT = 0.1f;
        public const float DODGE_TIME_DEFAULT = 0.6f;
        public const float JUMP_TIME_CUMULATIVE_DEFAULT = 0;
        public const float DODGE_TIME_CUMULATIVE_DEFAULT = 0;
    }
}