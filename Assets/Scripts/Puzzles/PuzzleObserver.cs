using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Observes the puzzles completion events
/// </summary>
public class PuzzleObserver : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        EventManager.onPuzzleComplete += PuzzleComplete;
        EventManager.onPuzzleReset += PuzzleReset;
    }

    private void OnDestroy()
    {
        EventManager.onPuzzleComplete -= PuzzleComplete;
        EventManager.onPuzzleReset += PuzzleReset;
    }

    public void PuzzleComplete(string eventCode)
    {
        
    }

    public void PuzzleReset(string eventCode)
    {

    }

}
