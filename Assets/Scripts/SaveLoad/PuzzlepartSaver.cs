using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @Author : Veli-Matti Vuoti
/// 
/// This class is used to save puzzlepart data without causing merge issues with the puzzlepart class itself, implements ISaver interface
/// Has reference to puzzlepart and the savedata
/// Listens OnSave and OnLoad events for updating data
/// Add this component for puzzleparts that needs saving
/// </summary>
public class PuzzlepartSaver : MonoBehaviour, ISaver
{
    public SaveDataType dataType;
    public PuzzlepartSaveData saveData;
    public Puzzlepart objectData;

    private void Awake()
    {
        if (objectData == null)
            objectData = GetComponent<Puzzlepart>();

        if (saveData == null)
            saveData = GetComponent<PuzzlepartSaveData>();
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

    /// <summary>
    /// On Save updates the save data with object data
    /// </summary>
    public void UpdateSaveData()
    {
        if (saveData == null)
        {
            if (DebugTable.SaveDebug)
                Debug.Log(this.gameObject.name + " Has no saveable object, this is object is not saved ! ");

            return;
        }

        saveData.id = objectData.id;

        saveData.active = gameObject.activeSelf;

        saveData.partState = objectData.completed;

        saveData.position = transform.position;

        saveData.rotation = transform.rotation.eulerAngles;

        saveData.scale = transform.localScale;

    }

    /// <summary>
    /// On Load updates the object data with save data
    /// </summary>
    public void UpdateObjectData()
    {

        if (saveData == null)
        {
            if (DebugTable.SaveDebug)
                Debug.Log(this.gameObject.name + " Has no saveable object, this is object is not saved ! ");

            return;
        }

        if(!saveData.active)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

        objectData.completed = saveData.partState;

        transform.position = saveData.position;

        transform.rotation = Quaternion.Euler(saveData.rotation);

        transform.localScale = saveData.scale;

    }
}
