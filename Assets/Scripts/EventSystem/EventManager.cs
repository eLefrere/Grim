using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages events of the game
/// </summary>
public class EventManager : MonoBehaviour
{
    /// <summary>
    /// Delegate for events
    /// </summary>
    /// <param name="eventCode">Pass eventcode as parameter to point subscribed event for specific objects</param>
    public delegate void EventTriggerDelegate(string eventCode, Vector3 pos);   
    public static event EventTriggerDelegate onTriggerEnter;
    public static event EventTriggerDelegate onTriggerExit;

    public delegate void PuzzleEventDelegate(string eventCode);
    public static event PuzzleEventDelegate onPuzzleComplete;
    public static event PuzzleEventDelegate onPuzzleReset;
    public static event PuzzleEventDelegate onPuzzlepartComplete;
    public static event PuzzleEventDelegate onPuzzlepartReset;

    public static event PuzzleEventDelegate onAllPuzzlesComplete;
    public static event PuzzleEventDelegate on25PercentCompletion;
    public static event PuzzleEventDelegate on50PercentCompletion;
    public static event PuzzleEventDelegate on75PercentCompletion;

    public delegate void NormalEventDelegate();
    public static event NormalEventDelegate onNormalEvent;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    #region EventCall Functions

    public static void OnNormalEvent()
    {
        if(DebugTable.instance.IsDebugging("EventDebug"))
        {
            Debug.Log("Invoking normal event!");
        }

        onNormalEvent?.Invoke();
    }

    #region Trigger Event Calls
    /// <summary>
    /// To Invoke the onTriggerEnter
    /// </summary>
    /// <param name="eventCode">code to specify event</param>
    /// <param name="pos">position of event</param>
    public static void OnTriggerEnterEvent(string eventCode, Vector3 pos)
    {
        if(DebugTable.instance.IsDebugging("EventDebug"))
            Debug.Log("On Trigger Enter Event CODE: " + eventCode + " POSITION: " + pos);

        onTriggerEnter?.Invoke(eventCode, pos);
    }

    /// <summary>
    /// To Invoke the onTriggerExit
    /// </summary>
    /// <param name="eventCode">code to specify event</param>
    /// <param name="pos">position of event</param>
    public static void OnTriggerExitEvent(string eventCode, Vector3 pos)
    {
        if(DebugTable.instance.IsDebugging("EventDebug"))
            Debug.Log("On Trigger Exit Event CODE: " + eventCode + " POSITION: " + pos);

        onTriggerExit?.Invoke(eventCode, pos);
    }

    #endregion Trigger Event Calls

    #region Puzzle Events Calls
    public static void OnPuzzleCompleteEvent(string eventCode)
    {
        if (DebugTable.instance.IsDebugging("EventDebug"))
            Debug.Log("On Puzzle Complete Event CODE: " + eventCode);

        onPuzzleComplete?.Invoke(eventCode);
    }

    public static void OnPuzzleResetEvent(string eventCode)
    {
        if (DebugTable.instance.IsDebugging("EventDebug"))
            Debug.Log("On Puzzle Reset Event CODE: " + eventCode);

        onPuzzleReset?.Invoke(eventCode);
    }

    public static void OnPuzzlepartCompleteEvent(string eventCode)
    {
        if (DebugTable.instance.IsDebugging("EventDebug"))
            Debug.Log("On Puzzlepart Complete Event CODE: " + eventCode);

        onPuzzlepartComplete?.Invoke(eventCode);
    }

    public static void OnPuzzlepartResetEvent(string eventCode)
    {
        if (DebugTable.instance.IsDebugging("EventDebug"))
            Debug.Log("On Puzzlepart Reset Event CODE: " + eventCode);

        onPuzzlepartReset?.Invoke(eventCode);
    }

    public static void OnPuzzlesCompleteEvent(string eventCode)
    {
        if (DebugTable.instance.IsDebugging("EventDebug"))
            Debug.Log("Invoking all puzzles compled event : " + eventCode);

        onAllPuzzlesComplete?.Invoke(eventCode);
    }

    public static void OnPuzzles25CompletedEvent(string eventCode)
    {
        if (DebugTable.instance.IsDebugging("EventDebug"))
            Debug.Log("Invoking 25% puzzles compled event : " + eventCode);

        on25PercentCompletion?.Invoke(eventCode);
    }

    public static void OnPuzzles50CompletedEvent(string eventCode)
    {
        if (DebugTable.instance.IsDebugging("EventDebug"))
            Debug.Log("Invoking 50% puzzles compled event : " + eventCode);

        on50PercentCompletion?.Invoke(eventCode);
    }

    public static void OnPuzzles75CompletedEvent(string eventCode)
    {
        if (DebugTable.instance.IsDebugging("EventDebug"))
            Debug.Log("Invoking 75% puzzles compled event : " + eventCode);

        on75PercentCompletion?.Invoke(eventCode);
    }

    #endregion Puzzle Events Calls

    #endregion EventCall Functions for <EventTriggerDelegate>

}
