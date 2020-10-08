using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 
/// @Author : Veli-Matti Vuoti
/// 
/// This class sets graphical text overlay for puzzle completion states, usefull for debugging puzzlestates.
/// 
/// Listens the Eventmanager puzzle status changes!
/// </summary>
public class PuzzleCompletionState : MonoBehaviour
{
    public TextMeshProUGUI [] textMesh;
    public TextMeshProUGUI completionText;
    public TextMeshProUGUI puzzleNotify;
      
    private void OnEnable()
    {
        EventManager.onPuzzlepartComplete += CheckCompletedPart;
        EventManager.onAllPuzzlesComplete += AllPuzzlesCompleted;
        EventManager.onPuzzleComplete += PuzzleNotify;
     }

    private void OnDisable()
    {
        EventManager.onPuzzlepartComplete -= CheckCompletedPart;
        EventManager.onAllPuzzlesComplete -= AllPuzzlesCompleted;
        EventManager.onPuzzleComplete -= PuzzleNotify;
    }
    
    /// <summary>
    /// Starts the coroutine on puzzle complete event
    /// </summary>
    /// <param name="eventCode"></param>
    public void PuzzleNotify(string eventCode)
    {
        StartCoroutine(PuzzleNotified(eventCode));
    }

    /// <summary>
    /// This coroutine shows the puzzle 
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public IEnumerator PuzzleNotified(string code)
    {
        puzzleNotify.gameObject.SetActive(true);
        puzzleNotify.text = code + " Complete";
        yield return new WaitForSeconds(2);
        puzzleNotify.gameObject.SetActive(false);
    }

    /// <summary>
    /// Activates completionTExt when all puzzles event called.
    /// </summary>
    /// <param name="eventCode"></param>
    public void AllPuzzlesCompleted(string eventCode)
    {
        completionText.gameObject.SetActive(true);
    }

    /// <summary>
    /// Depending on event code changes the textmesh texts in puzzle completion state array.
    /// </summary>
    /// <param name="eventCode"></param>
    public void CheckCompletedPart(string eventCode)
    {
        if( eventCode == "Button1Complete")
        {
            textMesh[0].text = "Button 1 = true";
        }
        if(eventCode == "Button2Complete")
        {
            textMesh[1].text = "Button 2 = true";
        }
        if(eventCode == "Wheel1Complete")
        {
            textMesh[2].text = "Wheel 1 = true";
        }
        if (eventCode == "Wheel2Complete")
        {
            textMesh[3].text = "Wheel 2 = true";
        }
        if (eventCode == "Wheel3Complete")
        {
            textMesh[4].text = "Wheel 3 = true";
        }
        if (eventCode == "Wheel4Complete")
        {
            textMesh[5].text = "Wheel 4 = true";
        }
    }
}
