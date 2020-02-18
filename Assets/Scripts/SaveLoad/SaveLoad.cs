using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// Saves and loads the gamestate Json format
/// </summary>
public class SaveLoad : MonoBehaviour
{
    
    public static string SavePath;
    public string file = "save_";
    public string extension = ".json";
    DirectoryInfo folder;

    Puzzlepart[] puzzleparts;

    public bool load = false;

    /// <summary>
    /// Get save name without extension
    /// </summary>
    /// <returns></returns>
    string GetSaveName()
    {
        return file;
    }

    /// <summary>
    /// Get Save file name with extension
    /// </summary>
    /// <returns></returns>
    string GetSaveFile()
    {
        return file + extension;
    }

    /// <summary>
    /// Get Full save path
    /// </summary>
    /// <param name="fileName">name of file</param>
    /// <returns></returns>
    string GetPath(string fileName)
    {
        return Path.Combine(SavePath, fileName);
    }

    private void Awake()
    {
        SavePath = Application.persistentDataPath + "/Saves/";

        if (!Directory.Exists(SavePath))
        {
            folder = Directory.CreateDirectory(SavePath);       
        }
     
    }

    private void Start()
    {
        EventManager.onPuzzleComplete += TimeToSave;
    }

    private void OnDestroy()
    {
        EventManager.onPuzzleComplete -= TimeToSave;
    }

    public void TimeToSave(string eventCode)
    {
        if (DebugTable.SaveDebug)
        {
            Debug.Log("Saving...");
        }
        Save();
    }

    public void Save()
    {

        EventManager.OnSave();

        SaveObject save = new SaveObject(Guid.NewGuid().ToString(), GetSaveName(), DateTime.Now.ToString() );
        puzzleparts = FindObjectsOfType<Puzzlepart>();

        for (int i = 0; i < puzzleparts.Length; i++)
        {
            save.saveablesPuzzleParts.Add(puzzleparts[i].mySaveable);
        }
        
        for (int i = 0; i < save.saveablesPuzzleParts.Count; i++)
        {
            Debug.Log(save.saveablesPuzzleParts[i]);
        }

        string json = JsonConvert.SerializeObject(save); // with newtonsoft
        //string json = JsonUtility.ToJson(save);
        Debug.Log(GetPath(GetSaveFile()));
        File.WriteAllText(GetPath(GetSaveFile()), json);
    }

    public void Load()
    {
        EventManager.OnLoad();

        string saveString = File.ReadAllText(GetPath(GetSaveFile()));
        SaveObject loadedSave = JsonConvert.DeserializeObject<SaveObject>(saveString);
        //SaveObject loadedSave = JsonUtility.FromJson<SaveObject>(saveString);

        for (int i = 0; i < loadedSave.saveablesPuzzleParts.Count; i++)
        {
            Debug.Log(loadedSave.saveablesPuzzleParts[i]);
        }

        if (DebugTable.SaveDebug)
            Debug.Log("LOAD SAVENAME : " + loadedSave.saveName);
      
        //Reset Player pos to 0.0.0
        Valve.VR.InteractionSystem.Player.instance.transform.position = Vector3.zero;
    }

    private void Update()
    {
        if(load)
        {
            load = false;

            Load();

        }
    }
}
