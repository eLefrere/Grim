using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlepartSaver : MonoBehaviour, ISaver
{
    public PuzzlepartSaveData mySaveData;
    public Puzzlepart objectToSave;

    private void Awake()
    {
        if (objectToSave == null)
            objectToSave = GetComponent<Puzzlepart>();

        if (mySaveData == null)
            mySaveData = GetComponent<PuzzlepartSaveData>();
    }

    private void OnEnable()
    {
        EventManager.onSave += UpdateMySaveable;
        EventManager.onLoad += LoadFromMySaveable;
    }

    private void OnDisable()
    {
        EventManager.onSave -= UpdateMySaveable;
        EventManager.onLoad -= LoadFromMySaveable;
    }

    public void UpdateMySaveable()
    {
        if (mySaveData == null)
        {
            if (DebugTable.SaveDebug)
                Debug.Log(this.gameObject.name + " Has no saveable object, this is object is not saved ! ");

            return;
        }

        mySaveData.id = objectToSave.id;

        mySaveData.partState = objectToSave.completed;

        mySaveData.position = transform.position;

        mySaveData.rotation = transform.rotation.eulerAngles;

        mySaveData.scale = transform.localScale;

    }

    public void LoadFromMySaveable()
    {

        if (mySaveData == null)
        {
            if (DebugTable.SaveDebug)
                Debug.Log(this.gameObject.name + " Has no saveable object, this is object is not saved ! ");

            return;
        }

        objectToSave.completed = mySaveData.partState;

        transform.position = mySaveData.position;

        transform.rotation = Quaternion.Euler(mySaveData.rotation);

        transform.localScale = mySaveData.scale;

    }
}
