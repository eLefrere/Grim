using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Puzzles have puzzleparts, when puzzlepart is completed puzzle will update also raises event
/// </summary>
public abstract class PuzzlePart : MonoBehaviour
{

    public bool completed = false;
    public string eventCodeComplete;
    public string eventCodeReset;


    public void SetFinished()
    {
        if (!completed)
        {
            completed = true;

            if (DebugTable.PuzzleDebug)
                Debug.Log("Puzzle part finished! " + gameObject.name);

            EventManager.OnPuzzleCompleteEvent(eventCodeComplete);
        }
    }

    public void ResetPart()
    {

        completed = false;

        if (DebugTable.PuzzleDebug)
            Debug.Log("puzzle part reseted! " + gameObject.name);

        EventManager.OnPuzzlepartResetEvent(eventCodeReset);

    }

}
