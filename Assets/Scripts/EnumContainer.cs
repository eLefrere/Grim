using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Type of Puzzle
/// </summary>
public enum PuzzleType
{
    ButtonPuzzle,
    BookPuzzle,
    ItemPuzzle
}

/// <summary>
/// State of game
/// </summary>
public enum GameState
{
    GameOn,
    GamePause
}

/// <summary>
/// 
/// </summary>
public enum SaveDataType
{
    PuzzlepartSaveData,
    InteractableSaveData,
    InventorySaveData,

}

public enum AIType
{
    Teleporting,
    Walking,
}

public enum MovementType
{
    Teleporting,
    Walking,
}

//public enum ControllerMode
//{
//    VRController,
//    FPSController,
//}