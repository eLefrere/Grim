using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Booleans to activate and deactivate specific debugging works with Scriptable Objects OR just static member functions use what you like!
/// </summary>
public class DebugTable : MonoBehaviour
{

    [Header("Different Debug areas ON/OFF, automatically changes static member to match bool")]
    public bool debugging;
    public static bool Debugging;
    [Tooltip("List of Debug Field Scriptable Objects optionally used to check debugging status")]
    public DebugField[] debugFields;

    [Header("Dev-vellu debugging")]
    public bool eventDebug;
    public static bool EventDebug;

    public bool puzzleDebug;
    public static bool PuzzleDebug;

    [Header("Dev-emppu debugging")]
    public bool playerMovementDebug;
    public static bool PlayerMovementDebug;

    public bool playerInteractionDebug;
    public static bool PlayerInteractionDebug;

    public bool soundDebug;
    public static bool SoundDebug;

    [Header("Dev-Tohvu debugging")]
    public bool aiPlayerDebug;
    public static bool AiPlayerDebug;

    [Header("Dev-Leo debugging")]
    public bool monsterDebug;
    public static bool MonsterDebug;

    public bool shaderDebug;
    public static bool ShaderDebug;

    private static DebugTable debugTableInstance = null;
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

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

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
            if(Debugging)
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
            if(Debugging)
                Debug.Log(debugFields[i].name);

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

        if (debugging != Debugging)
            Debugging = debugging;

        if (eventDebug != EventDebug)
            EventDebug = eventDebug;

        if (puzzleDebug != PuzzleDebug)
            PuzzleDebug = puzzleDebug;

        if (playerMovementDebug != PlayerMovementDebug)
            PlayerMovementDebug = playerMovementDebug;

        if (playerInteractionDebug != PlayerInteractionDebug)
            PlayerInteractionDebug = playerInteractionDebug;

        if (soundDebug != SoundDebug)
            SoundDebug = soundDebug;

        if (aiPlayerDebug != AiPlayerDebug)
            AiPlayerDebug = aiPlayerDebug;

        if (monsterDebug != MonsterDebug)
            MonsterDebug = monsterDebug;

        if (shaderDebug != ShaderDebug)
            ShaderDebug = shaderDebug;

    }

}
