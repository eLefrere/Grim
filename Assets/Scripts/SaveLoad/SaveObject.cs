using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SaveObject
{

    public string saveID;
    public string saveName;
    public string timestamp;

    public List<PuzzlepartSaveData> saveablePuzzleParts = new List<PuzzlepartSaveData>();

    public SaveObject(string id, string saveName, string timeStamp)
    {
        saveID = id;
        this.saveName = saveName;
        timestamp = timeStamp;
    }

}
