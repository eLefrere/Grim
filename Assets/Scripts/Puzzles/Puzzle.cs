using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Base class for puzzles
/// </summary>
public abstract class Puzzle : MonoBehaviour
{
    public string completionEventCode;
    public string resetEventCode;

    public float timeStep = 3.0f;
    private IEnumerator coroutine;
    public List<PuzzlePart> puzzleParts = new List<PuzzlePart>();
    public bool finished = false;


    private void Start()
    {
        coroutine = Tick(timeStep);
        StartCoroutine(coroutine);
    }

    public IEnumerator Tick(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(timeStep);

            if (DebugTable.PuzzleDebug)
                Debug.Log("Check Puzzle Status " + Time.time);
          
            CheckStatus();
        }
    }

    public void ResetPuzzle()
    {
        for (int i = 0; i < puzzleParts.Count; i++)
        {
            if (puzzleParts[i] != null)
                puzzleParts[i].ResetPart();
        }

        finished = false;
    }

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

        finished = true;
        EventManager.OnPuzzleCompleteEvent(completionEventCode);

    }



}
