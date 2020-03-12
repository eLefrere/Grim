using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author : Veli-Matti Vuoti
/// 
/// Booleans to activate and deactivate specific debugging works with Scriptable Objects to avoid merge issues OR just static members, use what you like!
/// </summary>
public class DebugTable : MonoBehaviour
{

    [Tooltip("List of Debug Field Scriptable Objects optionally used to check debugging status")]
    public DebugField[] debugFields;
   
    [Header("Dev-vellu debugging")]
    public bool eventDebug;
    public static bool EventDebug;

    public bool puzzleDebug;
    public static bool PuzzleDebug;

    public bool saveDebug;
    public static bool SaveDebug;

    [Header("Dev-emppu debugging")]
    public bool playerMovementDebug;
    public static bool PlayerMovementDebug;

    public bool playerInteractionDebug;
    public static bool PlayerInteractionDebug;

    public bool soundDebug;
    public static bool SoundDebug;

    [Header("Dev-Tohvu debugging")]
    public bool inventoryDebug;
    public static bool InventoryDebug;

    [Header("Dev-Leo debugging")]
    public bool monsterDebug;
    public static bool MonsterDebug;

    public bool shaderDebug;
    public static bool ShaderDebug;

    #region singleton
    private static DebugTable debugTableInstance = null;
    /// <summary>
    /// DebugTable singleton to check custom scriptable object debugfields!
    /// </summary>
    public static DebugTable instance
    {
        get
        {
            if (debugTableInstance == null)
            {
                debugTableInstance = new DebugTable();
            }

            return debugTableInstance;
        }
    }
    #endregion singleton

    private void Awake()
    {
     
        debugTableInstance = this;

    }

    /// <summary>
    /// Check debugging status for ScriptableObject if you have one in stored in class
    /// Use singleton to call this
    /// </summary>
    /// <param name="field">DebugField scriptable object contains bool debug ON/OFF</param>
    /// <returns></returns>
    public bool IsDebugging(DebugField field)
    {
        for (int i = 0; i < debugFields.Length; i++)
        {
          
            Debug.Log(debugFields[i].name);

            if (field == debugFields[i])
            {
                return debugFields[i].enableDebug;
            }
        }

        return false;
    }

    /// <summary>
    /// Check debugging status for ScriptableObject by name
    /// Use singleton to call this
    /// </summary>
    /// <param name="fieldName">DebugFields Name</param>
    /// <returns></returns>
    public bool IsDebugging(string fieldName)
    {
        for (int i = 0; i < debugFields.Length; i++)
        {
          
            //Debug.Log(debugFields[i].name);

            if (fieldName == debugFields[i].name)
            {
                return debugFields[i].enableDebug;
            }
        }

        return false;
    }

    /// <summary>
    /// Sets the static members to match the public bool if changed
    /// </summary>
    private void OnValidate()
    {

        if (eventDebug != EventDebug)
            EventDebug = eventDebug;
        
        if (puzzleDebug != PuzzleDebug)
            PuzzleDebug = puzzleDebug;

        if (saveDebug != SaveDebug)
            SaveDebug = saveDebug;
     
        if (playerMovementDebug != PlayerMovementDebug)
            PlayerMovementDebug = playerMovementDebug;

        if (playerInteractionDebug != PlayerInteractionDebug)
            PlayerInteractionDebug = playerInteractionDebug;

        if (soundDebug != SoundDebug)
            SoundDebug = soundDebug;

        if (inventoryDebug != InventoryDebug)
            InventoryDebug = inventoryDebug;

        if (monsterDebug != MonsterDebug)
            MonsterDebug = monsterDebug;

        if (shaderDebug != ShaderDebug)
            ShaderDebug = shaderDebug;

    }

}
