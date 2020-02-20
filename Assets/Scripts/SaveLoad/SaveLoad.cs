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
    public int saveId;
    DirectoryInfo folder;

    PuzzlepartSaver[] puzzlePartSavers;

    public bool load = false;
    public bool continueG = false;
    public bool save = false;

    /// <summary>
    /// Get save name without extension
    /// </summary>
    /// <returns></returns>
    string GetSaveName(int saveNumber)
    {
        return file + saveNumber.ToString();
    }

    /// <summary>
    /// Get Save file name with extension
    /// </summary>
    /// <returns></returns>
    string GetSaveFile(int saveNumber)
    {
        return file + saveNumber.ToString() + extension;
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

        if (puzzlePartSavers.IsEmpty())
            puzzlePartSavers = FindObjectsOfType<PuzzlepartSaver>();

    }

    private void Start()
    {
        EventManager.onPuzzleComplete += TimeToSave;
        EventManager.onGameContinue += TimeToLoad;
        EventManager.onNewGame += TimeToNewSave;
    }

    private void OnDestroy()
    {
        EventManager.onPuzzleComplete -= TimeToSave;
        EventManager.onGameContinue -= TimeToLoad;
        EventManager.onNewGame -= TimeToNewSave;
    }

    public void TimeToNewSave(GameState toState)
    {
        if (!File.Exists(SavePath + GetSaveFile(saveId)))
            return;

        while (File.Exists(SavePath + GetSaveFile(saveId)))
        {
            saveId++;
        }

    }

    public void TimeToSave(string eventCode)
    {
       
        Save();
    }

    public void TimeToLoad(GameState toState)
    {
        
        Load();
    }

    public void Save()
    {
        if (DebugTable.SaveDebug)
        {
            Debug.Log("Saving...");
        }

        EventManager.OnSave();

        SaveObject save = new SaveObject(Guid.NewGuid().ToString(), GetSaveName(saveId), DateTime.Now.ToString());

        if (puzzlePartSavers.IsEmpty())
            puzzlePartSavers = FindObjectsOfType<PuzzlepartSaver>();

        for (int i = 0; i < puzzlePartSavers.Length; i++)
        {
            save.saveablePuzzleParts.Add(puzzlePartSavers[i].mySaveData);
        }

        for (int i = 0; i < save.saveablePuzzleParts.Count; i++)
        {
            if (DebugTable.SaveDebug)
                Debug.Log(save.saveablePuzzleParts[i]);
        }

        string json = JsonConvert.SerializeObject(save); // with newtonsoft
        //string json = JsonUtility.ToJson(save);
        Debug.Log(GetPath(GetSaveFile(saveId)));
        File.WriteAllText(GetPath(GetSaveFile(saveId)), json);
    }

    public void Load()
    {
        if (DebugTable.SaveDebug)
            Debug.Log("Loading...");

        string saveString = File.ReadAllText(GetPath(GetSaveFile(saveId)));
        SaveObject loadedSave = JsonConvert.DeserializeObject<SaveObject>(saveString);
        //SaveObject loadedSave = JsonUtility.FromJson<SaveObject>(saveString);

        if (puzzlePartSavers.IsEmpty())
            puzzlePartSavers = FindObjectsOfType<PuzzlepartSaver>();

        if (DebugTable.SaveDebug)
            Debug.Log("LOAD SAVENAME : " + loadedSave.saveName);

        for (int i = 0; i < loadedSave.saveablePuzzleParts.Count; i++)
        {
            if (puzzlePartSavers[i].mySaveData != null)
            {
                puzzlePartSavers[i].mySaveData = loadedSave.saveablePuzzleParts[i];
            }
        }

        //Reset Player pos to 0.0.0
        Valve.VR.InteractionSystem.Player.instance.transform.position = Vector3.zero;

        EventManager.OnLoad();
    }

    private void Update()
    {
        if (load)
        {
            load = false;

            Load();

        }

        if(continueG)
        {
            continueG = false;

            EventManager.OnNewGameStart(GameState.GameOn);
        }

        if(save)
        {
            save = false;

            Save();
        }
    }
}
