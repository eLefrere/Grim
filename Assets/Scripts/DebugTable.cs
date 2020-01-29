using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Booleans to activate and deactivate specific debugging
/// </summary>
public class DebugTable : MonoBehaviour
{
    [Header("Different Debug areas ON/OFF, automatically changes static member to match bool")]
    [Space]
    public bool eventDebug;
    public static bool EventDebug;

    public bool puzzleDebug;
    public static bool PuzzleDebug;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnValidate()
    {
        if (eventDebug != EventDebug)
            EventDebug = eventDebug;

        if (puzzleDebug != PuzzleDebug)
            PuzzleDebug = puzzleDebug;
    }

}
