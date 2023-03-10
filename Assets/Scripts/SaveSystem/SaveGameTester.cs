using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.SaveSystem
{
    public class SaveGameTester : MonoBehaviour
    {
        public void TestSave()
        {
            float rand = Random.Range(0.0f, 100.0f);
            SaveGameManager.currentSaveData.randFloat = rand;
            SaveGameManager.SaveDataToFile(null);
            Debug.Log("Saved game with randFloat value " + rand);
        }

        public void TestLoad()
        {
            SaveGameManager.currentSaveData.randFloat = -10f;   // should not work
            SaveGameManager.LoadDataFromFile(null);
            Debug.Log("Loaded game with randFloat value " + SaveGameManager.currentSaveData.randFloat);
        }

        public void Start()
        {
            TestLoad();
            TestSave();
        }
    }
}
