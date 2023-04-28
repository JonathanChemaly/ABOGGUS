using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Gameplay;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public class SokobanWatcher : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Generators for elevator")]
        private GenerateSokoban dungeonLevel, summerLevel, winterLevel, springLevel;

        private void Awake()
        {
            int totalMana = UpgradeStats.totalMana;
            if (totalMana < 150)
            {
                summerLevel.enabled = false;
            }
            if (totalMana < 350)
            {
                winterLevel.enabled = false;
            }
            if (totalMana < 500)
            {
                springLevel.enabled = false;
            }
        }

        public SokobanCell[,] GetLevel(string level)
        {
            if (level.Equals("WinterLevel") && winterLevel.enabled)
            {
                return winterLevel.sokoban;
            } 
            else if (level.Equals("SummerLevel") && summerLevel.enabled)
            {
                return summerLevel.sokoban;
            } 
            else if (level.Equals("SpringLevel") && springLevel.enabled)
            {
                return springLevel.sokoban;
            }
            
            return dungeonLevel.sokoban;
        }

    }

}

