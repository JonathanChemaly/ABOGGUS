using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Gameplay;

namespace ABOGGUS.SaveSystem
{
    [System.Serializable]
    public class SaveData
    {
        // test data
        public int index = 1;
        public float randFloat = -1f;
        [SerializeField] private float myFloat = 5.8f;
        public bool ourBool = true;
        public Vector3 ourVector = new Vector3(0, 2, 5);

        // real data
        public string lastScene = GameConstants.SCENE_MAINLOBBY;
        public Vector3 lastPosition = Vector3.zero;
    }
}