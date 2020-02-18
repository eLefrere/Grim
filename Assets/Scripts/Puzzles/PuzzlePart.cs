using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author : Veli-Matti Vuoti
/// 
/// Puzzles have puzzleparts, when puzzlepart is completed puzzle will update also raises event
/// </summary>
public abstract class Puzzlepart : MonoBehaviour
{

    public SaveablePuzzlePart mySaveable;

    public bool completed = false;
    public string eventCodeComplete;
    public string eventCodeReset;

    public bool isResetedOverTime;
    public float triggerTimer;
    public float resettingTime;

    public void Update()
    {
        //Adds time to triggertime when puzzlepart is set complete state
        if(completed)
            triggerTimer += Time.deltaTime;

        //if puzzlepart is resetting over time
        if(isResetedOverTime)
        {
            //resets when triggerTimer is over resettingTime
            if(triggerTimer > resettingTime)
            {
                ResetPart();
            }
        }
    }

    /// <summary>
    /// Sets puzzlepart finished state
    /// </summary>
    public void SetFinished()
    {
        if (!completed)
        {
            completed = true;
            triggerTimer = 0;

            if (DebugTable.PuzzleDebug)
                Debug.Log("Puzzle part finished! " + gameObject.name);

            EventManager.OnPuzzleCompleteEvent(eventCodeComplete);
        }
    }

    /// <summary>
    /// Resets puzzlepart back to non finished state
    /// </summary>
    public void ResetPart()
    {

        completed = false;
        triggerTimer = 0;

        if (DebugTable.PuzzleDebug)
            Debug.Log("puzzle part reseted! " + gameObject.name);

        EventManager.OnPuzzlepartResetEvent(eventCodeReset);

        //TODO : RESET LOCATIONS AND ROTATIONS FOR MOVING PUZZLEPARTS ASWELL OR COLORS OR WHATEVER

    }

    public float GetTimerPercent()
    {
        if(triggerTimer == 0)
        {
            return 0;
        }

        return triggerTimer/resettingTime * 100f;
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

        mySaveable.partState = completed;


        mySaveable.position = transform.position;


        mySaveable.rotation = transform.rotation.eulerAngles;


        mySaveable.scale = transform.localScale;

    }

    public void LoadFromMySaveable()
    {

        completed = mySaveable.partState;


        transform.position = mySaveable.position;


        transform.rotation = Quaternion.Euler(mySaveable.rotation);


        transform.localScale = mySaveable.scale;

    }

}
