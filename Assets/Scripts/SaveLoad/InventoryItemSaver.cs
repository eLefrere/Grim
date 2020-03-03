using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 
/// </summary>
public class InventoryItemSaver : MonoBehaviour, ISaver
{
    public SaveDataType dataType;
    public InventoryItemSaveData saveData;
    //public InventoryItem objectData;

    private void Awake()
    {
       /* if (objectData == null)
            objectData = GetComponent<InventoryItem>();
            */
        if (saveData == null)
            saveData = GetComponent<InventoryItemSaveData>();
    }

    private void OnEnable()
    {
        EventManager.onSave += UpdateSaveData;
        EventManager.onLoad += UpdateObjectData;
    }

    public void UpdateObjectData()
    {
        //TODO : Load Data from Save Data
    }

    public void UpdateSaveData()
    {
        //TODO : Update Save Data with Object Data
    }
}
