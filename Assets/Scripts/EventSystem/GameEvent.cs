using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author : Veli-Matti Vuoti
/// 
/// This Class is Abstract base for game events
/// </summary>
public abstract class GameEvent : MonoBehaviour
{
    
    public string eventCode;

    /// <summary>
    /// Happens when the listener gets the matching eventCode for this Game Event, Start your logic from this function!
    /// </summary>
    /// <param name="pos"></param>
    public abstract void Raise(Vector3 pos);
}
