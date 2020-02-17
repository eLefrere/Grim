using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

/// <summary>
/// Saves and loads the gamestate Json format
/// </summary>
public class SaveLoad : MonoBehaviour
{
    
    public static readonly string SavePath = Application.persistentDataPath + "/Saves/";
    public string file = "save_";
    public string extension = ".txt";

    string GetSaveName()
    {
        return file;
    }

    string GetSaveFile()
    {
        return GetSaveName() + extension;
    }

    private void Start()
    {
        EventManager.onPuzzleComplete += TimeToSave;
    }

    private void OnDestroy()
    {
        EventManager.onPuzzleComplete -= TimeToSave;
    }

    void TimeToSave(string eventCode)
    {
        if (DebugTable.SaveDebug)
        {
            Debug.Log("Saving...");
        }

     
        Save();

    }

    public void Save()
    {

        SaveObject save = new SaveObject(Guid.NewGuid(), GetSaveName(), DateTime.Now );

      

        string json = JsonUtility.ToJson(save);
        WriteToFile(GetSaveFile(), json);
    }

    public void Load()
    {
        SaveObject loadedSave = JsonUtility.FromJson<SaveObject>(ReadFromFile(GetSaveFile()));

        ISaveable[] saveables = loadedSave.saveables.ToArray();

       
        //Reset Player pos to 0.0.0
        Valve.VR.InteractionSystem.Player.instance.transform.position = Vector3.zero;
    }

    void WriteToFile(string fileName, string json)
    {
        string path = SavePath + fileName;
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    string ReadFromFile(string fileName)
    {
        string path = SavePath + fileName;

        if(File.Exists(path))
        {
            using(StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else
        {
            Debug.LogWarning("File not found from path : " + path);
        }

        return null;
    }

  
}
