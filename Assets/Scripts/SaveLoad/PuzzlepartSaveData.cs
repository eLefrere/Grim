using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @Author : Veli-Matti Vuoti
/// 
/// This class is inherited from Saveable, represeting the puzzlepart savedata with only boolean value for state of part
/// </summary>
[System.Serializable]
public class PuzzlepartSaveData : SaveData
{
    public bool partState;
}
