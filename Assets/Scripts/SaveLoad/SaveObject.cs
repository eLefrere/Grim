using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @Author : Veli-Matti Vuoti
/// 
/// This Class is representing the save file
/// All the data from game are loaded in lists of this object when created
/// save file has id, savename, and timestamp.
/// </summary>
[Serializable]
public class SaveObject
{

    public string saveID;
    public string saveName;
    public string timestamp;

    public List<PuzzlepartSaveData> saveablePuzzleParts = new List<PuzzlepartSaveData>();

    /// <summary>
    /// Constructor for save
    /// </summary>
    /// <param name="id">save id</param>
    /// <param name="saveName">save name</param>
    /// <param name="timeStamp">time when saved</param>
    public SaveObject(string id, string saveName, string timeStamp)
    {
        saveID = id;
        this.saveName = saveName;
        timestamp = timeStamp;
    }

}
