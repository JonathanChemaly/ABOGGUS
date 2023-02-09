using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveGameManager
{
    public static SaveData currentSaveData = new SaveData();    // save data currently used in game

    public const string saveFolder = "/Saves/";
    public const string fileName = "DefaultSave.sav";  // currently only one save file at a time

    public static bool Save()
    {
        var dir = Application.persistentDataPath + saveFolder;  // find default directory to store our save

        // create "Saves" directory if it doesn't exist yet
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        // convert SaveData object to JSON, then write to save file
        string json = JsonUtility.ToJson(currentSaveData, true);
        File.WriteAllText(dir + fileName, json);

        GUIUtility.systemCopyBuffer = dir;  // copies path to clipboard

        return true;

    }

    public static void Load()
    {
        string path = Application.persistentDataPath + saveFolder + fileName;

        // if save path exists, read and convert all JSON data to SaveData object
        SaveData temp = new SaveData();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            temp = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            Debug.LogError("Save file does not exist.");
        }

        // assign loaded data
        currentSaveData = temp;
    }
}
