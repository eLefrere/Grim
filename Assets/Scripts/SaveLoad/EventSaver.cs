using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @Author : Veli-Matti Vuoti
/// 
/// Saves EventTrigger state.
/// </summary>
public class EventSaver : MonoBehaviour, ISaver
{

    public SaveDataType dataType;
    public EventSaveData saveData;
    public EventTrigger objectData;

    private void Awake()
    {
        if (objectData == null)
            objectData = GetComponent<EventTrigger>();

        if (saveData == null)
            saveData = GetComponent<EventSaveData>();
    }

    private void OnEnable()
    {
        EventManager.onSave += UpdateSaveData;
        EventManager.onLoad += UpdateObjectData;
    }

    private void OnDisable()
    {
        EventManager.onSave -= UpdateSaveData;
        EventManager.onLoad -= UpdateObjectData;
    }

    public void UpdateObjectData()
    {
        if (!HasSaveData())
            return;

        objectData.triggered = saveData.triggered;
        objectData.happenOnce = saveData.isOneShot;

    }

    public void UpdateSaveData()
    {
        if (!HasSaveData())
            return;

        saveData.triggered = objectData.triggered;
        saveData.isOneShot = objectData.happenOnce;

    }

    public bool HasSaveData()
    {

        if (saveData == null)
        {
            if (DebugTable.SaveDebug)
                Debug.Log(this.gameObject.name + " Has no saveable object, this is object is not saved ! ");

            return false;
        }

        return true;
    }

}
