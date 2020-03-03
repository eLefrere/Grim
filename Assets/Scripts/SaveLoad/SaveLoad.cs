using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// @Author : Veli-Matti Vuoti
/// 
/// This class does Saving and loading using JSON format
/// </summary>
public class SaveLoad : MonoBehaviour
{

    public static string SavePath;
    public string file = "save_";
    public string extension = ".json";
    public int saveId;
    DirectoryInfo folder;

    PuzzlepartSaver[] puzzlePartSavers;
    InteractableSaver[] interactableSavers;
    InventoryItemSaver[] inventoryItemSavers;
    EventSaver[] eventSavers;

    public bool load = false;
    public bool continueG = false;
    public bool save = false;
    public bool newG = false;

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

    /// <summary>
    /// On Awake Sets the SavePath to persistent datapath "works with multiple platforms" + folder "Saves"
    /// Checks if directory is already there, if not Creates it and saves the folderinfo into folder variable
    /// Finds all savers if not already done
    /// </summary>
    private void Awake()
    {
        SavePath = Application.persistentDataPath + "/Saves/";

        if (!Directory.Exists(SavePath))
        {
            folder = Directory.CreateDirectory(SavePath);
        }

        InitSaverArrays();
        
    }

    public void InitSaverArrays()
    {
        if (puzzlePartSavers.IsEmpty())
            puzzlePartSavers = FindObjectsOfType<PuzzlepartSaver>();
        if (interactableSavers.IsEmpty())
            interactableSavers = FindObjectsOfType<InteractableSaver>();
        if (inventoryItemSavers.IsEmpty())
            inventoryItemSavers = FindObjectsOfType<InventoryItemSaver>();
        if (eventSavers.IsEmpty())
            eventSavers = FindObjectsOfType<EventSaver>();
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

    /// <summary>
    /// When starting new game makes new save file with incremented number save_(number)
    /// </summary>
    /// <param name="toState">Not used in this function</param>
    public void TimeToNewSave(GameState toState)
    {
        if (!File.Exists(SavePath + GetSaveFile(saveId)))
            return;

        while (File.Exists(SavePath + GetSaveFile(saveId)))
        {
            saveId++;
        }

        //if too many saves it overrides first
        if(saveId > 5)
        {
            saveId = 0;
        }

    }

    /// <summary>
    /// Saves automatically when puzzle is completed
    /// </summary>
    /// <param name="eventCode"></param>
    public void TimeToSave(string eventCode)
    {
       
        Save();
    }

    /// <summary>
    /// Loads automatically when continuing game
    /// </summary>
    /// <param name="toState">Not used in this function</param>
    public void TimeToLoad(GameState toState)
    {
        
        Load();
    }

    /// <summary>
    /// This Function Saves the data:
    /// 
    /// 1. Invokes OnSave Event to update all save data objects
    /// 2. Creates new saveobject
    /// 3. Finds all savers if not done it before
    /// 4. Adds the saveable data into the save object specific object data lists
    /// 5. Converts / Serializes the saveobject into JSON format using newtonsoft libs
    /// 6. Writes all the data into a save file into Saves folder path for windows : C:\Users\"username"\AppData\LocalLow\GrimVR\Grim\Saves\
    /// </summary>
    public void Save()
    {
        if (DebugTable.SaveDebug)
        {
            Debug.Log("Saving...");
        }

        EventManager.OnSave();

        SaveObject save = new SaveObject(Guid.NewGuid().ToString(), GetSaveName(saveId), DateTime.Now.ToString());

        InitSaverArrays();
        FillSaveObject(save);

        for (int i = 0; i < save.saveablePuzzleParts.Count; i++)
        {
            if (DebugTable.SaveDebug)
                Debug.Log(save.saveablePuzzleParts[i]);
        }

        string json = JsonConvert.SerializeObject(save); // with newtonsoft
        //string json = JsonUtility.ToJson(save);

        if (DebugTable.SaveDebug)
            Debug.Log(GetPath(GetSaveFile(saveId)));

        File.WriteAllText(GetPath(GetSaveFile(saveId)), json);
    }

    private void FillSaveObject(SaveObject save)
    {
        for (int i = 0; i < puzzlePartSavers.Length; i++)
        {
            save.saveablePuzzleParts.Add(puzzlePartSavers[i].saveData);
        }

        for (int i = 0; i < interactableSavers.Length; i++)
        {
            save.saveableInteractables.Add(interactableSavers[i].saveData);
        }

        for (int i = 0; i < inventoryItemSavers.Length; i++)
        {
            save.saveableInventoryItems.Add(inventoryItemSavers[i].saveData);
        }

        for (int i = 0; i < eventSavers.Length; i++)
        {
            save.saveableEventTriggers.Add(eventSavers[i].saveData);
        }
    }

    /// <summary>
    /// This function loads the data
    /// 
    /// 1. Reads the file from the correct file path into a string variable
    /// 2. Deserializes the JSON presentated data from string variable into new SaveObject using newtonsoft lib
    /// 3. Finds all saver classes if not already done before
    /// 4. Updates the saver classes with data from saveobject
    /// 5. Sets player pos 0,0,0
    /// 6. Invokes OnLoad Event to update all objects with their loaded data
    /// </summary>
    public void Load()
    {
        if (DebugTable.SaveDebug)
            Debug.Log("Loading...");

        if (!File.Exists(SavePath + GetSaveFile(saveId)))
        {
            Debug.LogError("THERE ARE NO SAVE FILE IN " + SavePath + GetSaveFile(saveId));
            Debug.LogError("SAVE BEFORE TRYING TO LOAD");
            return;
        }

        string saveString = File.ReadAllText(GetPath(GetSaveFile(saveId)));
        SaveObject loadedSave = JsonConvert.DeserializeObject<SaveObject>(saveString);
        //SaveObject loadedSave = JsonUtility.FromJson<SaveObject>(saveString);

        InitSaverArrays();

        if (DebugTable.SaveDebug)
            Debug.Log("LOAD SAVENAME : " + loadedSave.saveName);

        UnloadLists(loadedSave);

        //Reset Player pos to 0.0.0
        Valve.VR.InteractionSystem.Player.instance.transform.position = Vector3.zero;

        EventManager.OnLoad();
    }

    private void UnloadLists(SaveObject loadedSave)
    {
        for (int i = 0; i < loadedSave.saveablePuzzleParts.Count; i++)
        {
            if (puzzlePartSavers[i].saveData != null)
            {
                puzzlePartSavers[i].saveData = loadedSave.saveablePuzzleParts[i];
            }
        }

        for (int i = 0; i < interactableSavers.Length; i++)
        {
            if (interactableSavers[i].saveData != null)
            {
                interactableSavers[i].saveData = loadedSave.saveableInteractables[i];
            }
        }

        for (int i = 0; i < inventoryItemSavers.Length; i++)
        {
            if (inventoryItemSavers[i].saveData != null)
            {
                inventoryItemSavers[i].saveData = loadedSave.saveableInventoryItems[i];
            }
        }

        for (int i = 0; i < eventSavers.Length; i++)
        {
            if (eventSavers[i].saveData != null)
            {
                eventSavers[i].saveData = loadedSave.saveableEventTriggers[i];
            }
        }
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

            EventManager.OnGameContinue(GameState.GameOn);
        }

        if(newG)
        {
            newG = false;

            EventManager.OnNewGameStart(GameState.GameOn);
        }

        if(save)
        {
            save = false;

            Save();
        }
    }
}
