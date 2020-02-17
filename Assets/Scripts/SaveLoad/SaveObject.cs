using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SaveObject
{

    public Guid saveID;
    public string saveName;
    public DateTime timestamp;

    public List<ISaveable> saveables = new List<ISaveable>();

    public SaveObject(Guid id, string saveName, DateTime timeStamp)
    {
        saveID = id;
        this.saveName = saveName;
        timestamp = timeStamp;
    }

}
