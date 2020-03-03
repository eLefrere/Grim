using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @Author : Veli-Matti Vuoti
/// 
/// Interface for saver classes
/// </summary>
public interface ISaver
{

    /// <summary>
    /// Function to update save data object before saving
    /// </summary>
    void UpdateSaveData();

    /// <summary>
    /// Function to load save data into object after loading
    /// </summary>
    void UpdateObjectData();

}
