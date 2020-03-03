using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 
/// Interactable Saver just like any saver
/// </summary>
public class InteractableSaver : MonoBehaviour, ISaver
{

    public SaveDataType dataType;
    public InteractableSaveData saveData;
    public Valve.VR.InteractionSystem.Interactable objectData;

    private void Awake()
    {
        if (objectData == null)
            objectData = GetComponent<Valve.VR.InteractionSystem.Interactable>();

        if (saveData == null)
            saveData = GetComponent<InteractableSaveData>();
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
        if (saveData == null)
        {
            if (DebugTable.SaveDebug)
                Debug.Log(this.gameObject.name + " Has no saveable object, this is object is not saved ! ");

            return;
        }

        if (!saveData.active)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

        transform.position = saveData.position;

        transform.rotation = Quaternion.Euler(saveData.rotation);

        transform.localScale = saveData.scale;

    }

    public void UpdateSaveData()
    {
        if (saveData == null)
        {
            if (DebugTable.SaveDebug)
                Debug.Log(this.gameObject.name + " Has no saveable object, this is object is not saved ! ");

            return;
        }

        saveData.active = gameObject.activeSelf;

        saveData.position = transform.position;

        saveData.rotation = transform.rotation.eulerAngles;

        saveData.scale = transform.localScale;
    }

}
