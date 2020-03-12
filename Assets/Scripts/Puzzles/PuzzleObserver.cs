using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// Author : Veli-Matti Vuoti
/// 
/// Observes the puzzles completion events
/// </summary>
public class PuzzleObserver : MonoBehaviour
{

    [Header("List of puzzles, finds all Puzzle classes when start")]
    public List<Puzzle> puzzles = new List<Puzzle>();

    [Header("Booleans for completed puzzles, could use the list too though")]
    public bool[] puzzlesCompleted;

    [Header("Current puzzle completion percent")]
    public float completionPercent = 0;

    [Header("Total completion status")]
    public bool isPercent25Reached;
    public bool isPercent50Reached;
    public bool isPercent75Reached;
    public bool allCompleted;

    public TextMeshProUGUI[] textMeshes;

    private void OnEnable()
    {
        EventManager.onPuzzleComplete += PuzzleComplete;
        EventManager.onPuzzleReset += PuzzleReset;
    }

    private void Start()
    {
        
        //Initialize lists and bool array
        puzzles = FindObjectsOfType<Puzzle>().ToList();

      
        puzzlesCompleted = new bool[puzzles.Count];

        for (int i = 0; i < puzzles.Count; i++)
        {
            textMeshes[i].text = puzzles[i].name + " initialized";
        }

        for (int i = 0; i < puzzles.Count; i++)
        {
            textMeshes[i + puzzles.Count].text = puzzlesCompleted[i].ToString();
        }

        //debug stuff
        if (DebugTable.PuzzleDebug)
        {
            for (int i = 0; i < puzzles.Count; i++)
            {
                Debug.Log(this.name + " observing puzzles : " + puzzles[i].name);
            }

            for (int i = 0; i < puzzlesCompleted.Length; i++)
            {
                Debug.Log("PuzzleCompletion state : " + puzzlesCompleted[i]);
            }
        }

        UpdateCompletedArray();     
    }

    private void OnDisable()
    {
        EventManager.onPuzzleComplete -= PuzzleComplete;
        EventManager.onPuzzleReset -= PuzzleReset;
    }

    /// <summary>
    /// When puzzle somewhere is completed
    /// </summary>
    /// <param name="eventCode"></param>
    public void PuzzleComplete(string eventCode)
    {
        UpdateCompletedArray();
        
    }

    /// <summary>
    /// When puzzle somewhere is resetted
    /// </summary>
    /// <param name="eventCode"></param>
    public void PuzzleReset(string eventCode)
    {
        UpdateCompletedArray();
      
    }

    /// <summary>
    /// Update the bool array to match puzzles
    /// </summary>
    public void UpdateCompletedArray()
    {

        if(allCompleted)
        {
            return;
        }

        for (int i = 0; i < puzzles.Count; i++)
        {
            if (puzzles[i].finished != puzzlesCompleted[i])
            {
                puzzlesCompleted[i] = puzzles[i].finished;

                if (DebugTable.PuzzleDebug)
                {
                    Debug.Log(puzzles[i].name + " completion state changed!");
                    Debug.Log("Changed Puzzle Completed index : " + i + " to " + puzzlesCompleted[i]);
                }
            }
        }

        for (int i = 0; i < puzzles.Count; i++)
        {
            textMeshes[i + puzzles.Count].text = puzzlesCompleted[i].ToString();
        }

        CheckPuzzleCompletionStatus();
    }

    /// <summary>
    /// Check puzzles completion
    /// </summary>
    public void CheckPuzzleCompletionStatus()
    {
        if (DebugTable.PuzzleDebug)
        {
            Debug.Log("Checking puzzle completion status...");
        }

        int count = 0;
     

        for (int i = 0; i < puzzlesCompleted.Length; i++)
        {
            if (!puzzlesCompleted[i])
            {
                if (DebugTable.PuzzleDebug)
                {
                    Debug.Log(" Puzzle index : " + i + " not completed!");
                }
            }
            else
            {
                if (DebugTable.PuzzleDebug)
                {
                    Debug.Log(" Puzzle index : " + i + " is completed!");
                    count++;
                }
            }
        }

        if (DebugTable.PuzzleDebug)
        {
            Debug.Log(count + " puzzles of " + puzzlesCompleted.Length + " completed");
        }

        
        completionPercent = count / (float)puzzlesCompleted.Length * 100f;
        

        if (DebugTable.PuzzleDebug)
        {
            Debug.Log("Completion percent is : " + completionPercent);
        }

        if(completionPercent >= 25f && completionPercent <= 30f && !isPercent25Reached)
        {
            EventManager.OnPuzzles25CompletedEvent(this.name);
            isPercent25Reached = true;
        }

        if(completionPercent >= 50f && completionPercent <= 60f && !isPercent50Reached)
        {
            EventManager.OnPuzzles50CompletedEvent(this.name);
            isPercent50Reached = true;
        }

        if(completionPercent >= 75f && completionPercent <= 80f && !isPercent75Reached)
        {
            EventManager.OnPuzzles75CompletedEvent(this.name);
            isPercent75Reached = true;
        }

        if(count == puzzlesCompleted.Length && count > 0)
        {
            EventManager.OnPuzzlesCompleteEvent(this.name);
            allCompleted = true;
        }

     }
}
