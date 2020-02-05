using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Observes the puzzles completion events
/// </summary>
public class PuzzleObserver : MonoBehaviour
{

    public List<Puzzle> puzzles = new List<Puzzle>();
    public bool[] puzzlesCompleted;
    public float completionPercent = 0;

    public bool isPercent25Reached;
    public bool isPercent50Reached;
    public bool isPercent75Reached;
    public bool allCompleted;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        EventManager.onPuzzleComplete += PuzzleComplete;
        EventManager.onPuzzleReset += PuzzleReset;

        puzzles = FindObjectsOfType<Puzzle>().ToList();
        puzzlesCompleted = new bool[puzzles.Count];

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

    private void OnDestroy()
    {
        EventManager.onPuzzleComplete -= PuzzleComplete;
        EventManager.onPuzzleReset += PuzzleReset;
    }

    public void PuzzleComplete(string eventCode)
    {
        UpdateCompletedArray();
        
    }

    public void PuzzleReset(string eventCode)
    {
        UpdateCompletedArray();
      
    }

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

        CheckPuzzleCompletionStatus();
    }

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

        if (count <= 0)
        {
            completionPercent = count / puzzlesCompleted.Length * 100;
        }
        else
        {
            completionPercent = 100f;
        }

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
