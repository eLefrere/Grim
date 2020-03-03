using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Author : Veli-Matti Vuoti
/// 
/// Abstract Base class for puzzles, inherit from this to make custom puzzle logic! 
/// Observer finds list of Puzzle classes to observe!
/// </summary>
public abstract class Puzzle : MonoBehaviour
{
    [Header("Eventcodes for puzzle events just in case!")]
    public string completionEventCode;
    public string resetEventCode;

    [Header("Time between puzzle status checks!")]
    public float timeStep = 3.0f;
    private IEnumerator coroutine;

    [Header("Drag and drop puzzleparts that are involved in completing this puzzle here!")]
    public List<Puzzlepart> puzzleParts = new List<Puzzlepart>();

    [Header("Puzzle completion status")]
    public bool finished = false;


    private void Start()
    {
        coroutine = Tick(timeStep);
        StartCoroutine(coroutine);
    }

    /// <summary>
    /// Coroutine that keeps checking puzzle status every timestep instead on update
    /// </summary>
    /// <param name="time">time it waits before running CheckStatus function</param>
    /// <returns></returns>
    public IEnumerator Tick(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);

            if (DebugTable.PuzzleDebug)
                Debug.Log("Check Puzzle Status " + Time.time);
          
            CheckStatus();
        }
    }

    /// <summary>
    /// Resets the puzzle to false state and all parts aswell
    /// </summary>
    public void ResetPuzzle()
    {
        for (int i = 0; i < puzzleParts.Count; i++)
        {
            if (puzzleParts[i] != null)
                puzzleParts[i].ResetPart();
        }

        finished = false;
        EventManager.OnPuzzleResetEvent(resetEventCode);
    }

    /// <summary>
    /// Checks if puzzleparts are complete if they are runs CompletePuzzle function
    /// </summary>
    public void CheckStatus()
    {
      
        if (puzzleParts.Any() == false)
        {
            if (DebugTable.PuzzleDebug)
                Debug.LogError("EMPTY PUZZLE! " + this.name);

            return;
        }

        for (int i = 0; i < puzzleParts.Count; i++)
        {
            if (puzzleParts[i] == null)
            {
                if (DebugTable.PuzzleDebug)
                    Debug.LogError("Puzzle Part Index Null in : " + this.gameObject.name + " gameobject ! index : " + i);

                return;
            }

            if (!puzzleParts[i].completed)
            {
                return;
            }
        }

        if (finished)
            return;

        CompletePuzzle();

    }

    /// <summary>
    /// Sets puzzle completed and sends event about it
    /// </summary>
    public void CompletePuzzle()
    {
        finished = true;

        EventManager.OnPuzzleCompleteEvent(completionEventCode);
    }



}
