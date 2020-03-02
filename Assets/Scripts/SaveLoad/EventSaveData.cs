using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event trigger savedata
/// </summary>
[System.Serializable]
public class EventSaveData : SaveData
{
    public bool isOneShot;
    public bool triggered;
}
