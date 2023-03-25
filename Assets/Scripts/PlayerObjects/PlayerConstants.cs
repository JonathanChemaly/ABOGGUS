using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
        public enum Magic { Wind, Fire, Water, Nature, Lightning };
        public enum Weapon { Sword, Grimoire, Spear, Unarmed };

        public const float INVULNERABILITY_FRAMES = 2 * FIXED_UPDATES_PER_SECOND;

        public const string UNARMED_IDLE = "Unarmed-Idle";
        public const string ARMED_IDLE = "Armed-Idle";
        public const string UNARMED_RUN_F = "Unarmed-Run-Forward";
        public const string ARMED_RUN_F = "Armed-Run-Forward";
        public const string SPRINT_ANIMATION = "Unarmed-Sprint";
        public const string UNARMED_CROUCH = "Unarmed-Idle-Crouch";
        public const string ARMED_CROUCH = "Armed-Idle-Crouch";
        public const string UNARMED_JUMP = "Unarmed-Jump";
        public const string ARMED_JUMP = "Armed-Jump";
        public const string UNARMED_DODGE = "Unarmed-DiveRoll-Forward1";
        public const string ARMED_DODGE = "Armed-DiveRoll-Forward1";
        public const string MAGIC_ATTACK = "Staff-Cast-Attack3_start";
        public const string MAGIC_AOE_ATTACK = "Staff-Cast-L-Summon1_start";
        public const string EQUIP_GRIMOIRE = "Armed-WeaponSwitch-R-Hips";
        public const string DEQUIP_GRIMOIRE = "Armed-Sheath-R-Hips-Unarmed";
        public const string EQUIP_SWORD = "Armed-WeaponSwitch-L-Back";
        public const string DEQUIP_SWORD = "Armed-Sheath-L-Back-Unarmed";
        public const string EQUIP_SPEAR = "Armed-WeaponSwitch-R-Back";
        public const string DEQUIP_SPEAR = "Armed-Sheath-R-Back-Unarmed";
        public static string[] SWORD_ATTACKS = { "Sword-Attack-L1", "Sword-Attack-L2", "Sword-Attack-L3", "Sword-Attack-L4", "Sword-Attack-L5", "Sword-Attack-L6" };
        public static string[] SPEAR_ATTACKS = { "Spear-Attack-R1", "Spear-Attack-R2", "Spear-Attack-R3", "Spear-Attack-R4", "Spear-Attack-R5", "Spear-Attack-R6" };
        public const string JUMP_SWORD_ATTACK = "Armed-Air-Attack-L1";
        public const string DASH_SWORD_ATTACK = "Armed-Run-Attack-L1";
        public const string JUMP_SPEAR_ATTACK = "Armed-Air-Attack-R1";
        public const string DASH_SPEAR_ATTACK = "Armed-Run-Attack-R1";

        public const float MAGIC_ATTACK_DELAY = 0.35f;
        public const float MAGIC_AOE_ATTACK_DELAY = 0.6f;
        public const float TRANSITION_DELAY = 0.8f;
        public const float ATTACK_DELAY = 0.8f;
        public const float JUMP_ATTACK_DELAY = 0.7f;
    }
}